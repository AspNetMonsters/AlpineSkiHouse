using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AlpineSkiHouse.Data;
using AlpineSkiHouse.Models.SkiCardViewModels;
using AlpineSkiHouse.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AlpineSkiHouse.Security;
using Microsoft.Extensions.Logging;
using AlpineSkiHouse.Services;
using Microsoft.AspNetCore.Http;

namespace AlpineSkiHouse.Web.Controllers
{
    [Authorize]
    public class SkiCardController : Controller
    {
        private readonly SkiCardContext _skiCardContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBlobFileUploadService _uploadservice;
        private readonly IAuthorizationService _authorizationService;
        private ILogger<SkiCardController> _logger;

        public SkiCardController(SkiCardContext skiCardContext,
                                    UserManager<ApplicationUser> userManager,
                                    IAuthorizationService authorizationService,
                                    IBlobFileUploadService uploadservice,
                                    ILogger<SkiCardController> logger)
        {
            _skiCardContext = skiCardContext;
            _userManager = userManager;
            _uploadservice = uploadservice;
            _authorizationService = authorizationService;
            _logger = logger;
        }

        // GET: SkiCard
        public async Task<ActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var skiCardsViewModels = await _skiCardContext.SkiCards
                .Where(s => s.ApplicationUserId == userId)
                .Select(s => new SkiCardListViewModel
                {
                    Id = s.Id,
                    CardHolderName = s.CardHolderFirstName + " " + s.CardHolderLastName
                })
                .ToListAsync();

            return View(skiCardsViewModels);
        }

        // GET: SkiCard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SkiCard/Create
        public async Task<ActionResult> Create()
        {
            var userId = _userManager.GetUserId(User);
            var currentUser = await _userManager.FindByIdAsync(userId);
            var viewModel = new CreateSkiCardViewModel
            {
                CardHolderPhoneNumber = currentUser.PhoneNumber
            };

            //If this is the user's first card, auto-populate the name properties since this card is 
            //most likely for that user. Otherwise assume the card is for a family member and leave
            //the name properties blank.
            var hasExistingSkiCards = _skiCardContext.SkiCards.Any(s => s.ApplicationUserId == userId);
            if (!hasExistingSkiCards)
            {
                viewModel.CardHolderFirstName = currentUser.FirstName;
                viewModel.CardHolderLastName = currentUser.LastName;
            }

            return View(viewModel);
        }

        private async Task<Guid> UploadImage(CreateSkiCardViewModel viewModel, string userId)
        {
            Guid imageId;
            _logger.LogInformation("Uploading ski card image for " + userId);
            imageId = Guid.NewGuid();
            await _uploadservice.UploadFileFromStream("cardimages", $"{imageId}.jpg", viewModel.CardImage.OpenReadStream());
            return imageId;
        }
        private bool HasCardImage(CreateSkiCardViewModel viewModel)
        {
            return viewModel.CardImage != null;
        }
        private async Task CreateAndSaveCard(CreateSkiCardViewModel viewModel)
        {
            var userId = _userManager.GetUserId(User);
            _logger.LogDebug($"Creating ski card for {userId}");

            using (_logger.BeginScope($"CreateSkiCard: {userId}"))
            {
                Guid? imageId = null;
                if (HasCardImage(viewModel))
                {
                    imageId = await UploadImage(viewModel, userId);
                }

                _logger.LogInformation($"Saving ski card to DB for {userId}");
                var skiCard = new SkiCard
                {
                    ApplicationUserId = userId,
                    CreatedOn = DateTime.UtcNow,
                    CardHolderFirstName = viewModel.CardHolderFirstName,
                    CardHolderLastName = viewModel.CardHolderLastName,
                    CardHolderBirthDate = viewModel.CardHolderBirthDate.Value.Date,
                    CardHolderPhoneNumber = viewModel.CardHolderPhoneNumber,
                    CardImageId = imageId
                };
                _skiCardContext.SkiCards.Add(skiCard);
                await _skiCardContext.SaveChangesAsync();

                _logger.LogInformation("Ski card created for " + userId);
            }
        }
        // POST: SkiCard/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateSkiCardViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            await CreateAndSaveCard(viewModel);

            _logger.LogDebug($"Ski card for {_userManager.GetUserId(User)} created successfully, redirecting to Index...");
            return RedirectToAction(nameof(Index));
        }

        // GET: SkiCard/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var userId = _userManager.GetUserId(User);

            var skiCard = await _skiCardContext.SkiCards
                .Where(s => s.Id == id)
                .SingleOrDefaultAsync();

            if (skiCard == null)
            {
                return NotFound();
            }

            var skiCardViewModel = new EditSkiCardViewModel
            {
                Id = skiCard.Id,
                CardHolderFirstName = skiCard.CardHolderFirstName,
                CardHolderLastName = skiCard.CardHolderLastName,
                CardHolderBirthDate = skiCard.CardHolderBirthDate,
                CardHolderPhoneNumber = skiCard.CardHolderPhoneNumber
            };

            if (await _authorizationService.AuthorizeAsync(User, skiCard, new EditSkiCardAuthorizationRequirement()))
            {
                return View(skiCardViewModel);
            }
            else
            {
                return new ChallengeResult();
            }
        }

        // POST: SkiCard/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditSkiCardViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var skiCard = await _skiCardContext.SkiCards
                            .SingleOrDefaultAsync(s => s.Id == viewModel.Id);

                if (skiCard == null)
                {
                    return NotFound();
                }
                else if (await _authorizationService.AuthorizeAsync(User, skiCard, new EditSkiCardAuthorizationRequirement()))
                {
                    skiCard.CardHolderFirstName = viewModel.CardHolderFirstName;
                    skiCard.CardHolderLastName = viewModel.CardHolderLastName;
                    skiCard.CardHolderPhoneNumber = viewModel.CardHolderPhoneNumber;
                    skiCard.CardHolderBirthDate = viewModel.CardHolderBirthDate.Value.Date;

                    await _skiCardContext.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return new ChallengeResult();
                }
            }
            return View(viewModel);
        }
    }
}