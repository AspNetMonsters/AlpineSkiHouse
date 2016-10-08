using AlpineSkiHouse.Data;
using AlpineSkiHouse.Models;
using AlpineSkiHouse.Models.SkiCardViewModels;
using AlpineSkiHouse.Security;
using AlpineSkiHouse.Services;
using AlpineSkiHouse.Web.Controllers;
using AlpineSkiHouse.Web.Tests.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace AlpineSkiHouse.Web.Tests.Controllers
{
    public class SkiCardControllerTests
    {

        public class WhenEditingASkiCardThatDoesNotExistInTheContext
        {
            [Fact]
            public async Task EditActionShouldReturnNotFound()
            {
                using (SkiCardContext context =
                        new SkiCardContext(InMemoryDbContextOptionsFactory.Create<SkiCardContext>()))
                {
                    var logger = new Mock<ILogger<SkiCardController>>();
                    var uploader = new Mock<IBlobFileUploadService>();
                    var controller = new SkiCardController(context, null, null, uploader.Object, logger.Object);
                    var result = await controller.Edit(new EditSkiCardViewModel
                    {
                        Id = 2,
                        CardHolderFirstName = "Dave",
                        CardHolderLastName = "Paquette",
                        CardHolderBirthDate = DateTime.Now.AddYears(-99),
                        CardHolderPhoneNumber = "555-123-1234"
                    });
                    Assert.IsType<NotFoundResult>(result);
                }
            }
        }

        public class GivenAHackerTriesToEditSomeoneElsesSkiCard : IDisposable
        {
            SkiCardContext _skiCardContext;
            SkiCard _skiCard;
            ControllerContext _controllerContext;
            ClaimsPrincipal _badGuyPrincipal;
            Mock<IAuthorizationService> _mockAuthorizationService;

            public GivenAHackerTriesToEditSomeoneElsesSkiCard()
            {
                _skiCardContext =
                    new SkiCardContext(InMemoryDbContextOptionsFactory.Create<SkiCardContext>());
                _skiCard = new SkiCard
                {
                    ApplicationUserId = Guid.NewGuid().ToString(),
                    Id = 5,
                    CardHolderFirstName = "James",
                    CardHolderLastName = "Chambers",
                    CardHolderBirthDate = DateTime.Now.AddYears(-150),
                    CardHolderPhoneNumber = "555-555-5555",
                    CreatedOn = DateTime.UtcNow
                };

                _skiCardContext.SkiCards.Add(_skiCard);
                _skiCardContext.SaveChanges();


                _badGuyPrincipal = new ClaimsPrincipal();
                _controllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = _badGuyPrincipal
                    }
                };

                _mockAuthorizationService = new Mock<IAuthorizationService>();
            }

            [Fact]
            public async void EditActionShouldReturnChallengeResult()
            {
                var logger = new Mock<ILogger<SkiCardController>>();
                var uploader = new Mock<IBlobFileUploadService>();
                var controller = new SkiCardController(_skiCardContext, null, _mockAuthorizationService.Object, uploader.Object, logger.Object)
                {
                    ControllerContext = _controllerContext
                };

                _mockAuthorizationService.Setup(
                    a => a.AuthorizeAsync(
                              _badGuyPrincipal,
                              _skiCard,
                              It.Is<IEnumerable<IAuthorizationRequirement>>(
                                  r => r.Count() == 1 && r.First() is EditSkiCardAuthorizationRequirement)))
                    .Returns(Task.FromResult(false));

                var result = await controller.Edit(new EditSkiCardViewModel
                {
                    Id = _skiCard.Id,
                    CardHolderFirstName = "BadGuy",
                    CardHolderLastName = "McHacker",
                    CardHolderBirthDate = DateTime.Now.AddYears(-25),
                    CardHolderPhoneNumber = "555-555-5555"
                });

                Assert.IsType<ChallengeResult>(result);
                _mockAuthorizationService.VerifyAll();
            }

            //TODO: Test that the ski card did not change

            public void Dispose()
            {
                _skiCardContext.Dispose();
            }
        }

    }
}

