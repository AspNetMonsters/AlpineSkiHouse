using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AlpineSkiHouse.Data;
using AlpineSkiHouse.Models.SkiCardViewModels;
using AlpineSkiHouse.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace AlpineSkiHouse.Web.Controllers
{
    [Authorize]
    public class SkiCardController : Controller
    {
        private SkiCardContext _skiCardContext;
        private UserManager<ApplicationUser> _userManager;

        public SkiCardController(SkiCardContext skiCardContext, UserManager<ApplicationUser> userManager)
        {
            _skiCardContext = skiCardContext;
            _userManager = userManager;
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

            var hasExistingSkiCards = _skiCardContext.SkiCards.Any(s => s.ApplicationUserId == userId);
            if (!hasExistingSkiCards)
            {
                viewModel.CardHolderFirstName = currentUser.FirstName;
                viewModel.CardHolderLastName = currentUser.LastName;
            }

            return View(viewModel);
        }

        // POST: SkiCard/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateSkiCardViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);

                SkiCard skiCard = new SkiCard
                {
                    ApplicationUserId = userId,
                    CreatedOn = DateTime.UtcNow,
                    CardHolderFirstName = viewModel.CardHolderFirstName,
                    CardHolderLastName = viewModel.CardHolderLastName,
                    CardHolderBirthDate = viewModel.CardHolderBirthDate.Value.Date,
                    CardHolderPhoneNumber = viewModel.CardHolderPhoneNumber
                };

                _skiCardContext.SkiCards.Add(skiCard);
                await _skiCardContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: SkiCard/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var userId = _userManager.GetUserId(User);

            var skiCardViewModel = await _skiCardContext.SkiCards
                .Where(s => s.ApplicationUserId == userId && s.Id == id)
                .Select(s => new EditSkiCardViewModel
                {
                    Id = s.Id,
                    CardHolderFirstName = s.CardHolderFirstName,
                    CardHolderLastName = s.CardHolderLastName,
                    CardHolderBirthDate = s.CardHolderBirthDate,
                    CardHolderPhoneNumber = s.CardHolderPhoneNumber
                }).SingleOrDefaultAsync();

            if (skiCardViewModel == null)
            {
                return NotFound();
            }
            
            return View(skiCardViewModel);
        }

        // POST: SkiCard/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditSkiCardViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);

                var skiCard = await _skiCardContext.SkiCards
                            .SingleOrDefaultAsync(s => s.ApplicationUserId == userId && s.Id == viewModel.Id);              

                if (skiCard == null)
                {
                    return NotFound();
                }

                skiCard.CardHolderFirstName = viewModel.CardHolderFirstName;
                skiCard.CardHolderLastName = viewModel.CardHolderLastName;
                skiCard.CardHolderPhoneNumber = viewModel.CardHolderPhoneNumber;
                skiCard.CardHolderBirthDate = viewModel.CardHolderBirthDate.Value.Date;

                await _skiCardContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }            
            return View(viewModel);
        }

        // GET: SkiCard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SkiCard/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}