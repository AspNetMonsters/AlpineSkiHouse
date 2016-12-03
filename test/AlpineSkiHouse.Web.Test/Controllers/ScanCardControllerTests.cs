using AlpineSkiHouse.Data;
using AlpineSkiHouse.Models;
using AlpineSkiHouse.Models.SkiCardViewModels;
using AlpineSkiHouse.Security;
using AlpineSkiHouse.Services;
using AlpineSkiHouse.Web.Command;
using AlpineSkiHouse.Web.Controllers;
using AlpineSkiHouse.Web.Queries;
using AlpineSkiHouse.Web.Tests.Data;
using MediatR;
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
    public class ScanCardControllerTests
    {

        public class WhenCardIsScanned
        {
            [Fact]
            public void CreateScanCommandShouldBeInvoked()
            {
                using (PassContext context =
                        new PassContext(InMemoryDbContextOptionsFactory.Create<PassContext>()))
                {
                    var dateServiceMock = new Mock<IDateService>();
                    dateServiceMock.Setup(d => d.Now()).Returns(DateTime.Now);

                    var mediatorMock = new Mock<IMediator>();
                    var controller = new ScanCardController(context, mediatorMock.Object, dateServiceMock.Object);

                    var result = controller.Get(124, 432);

                    mediatorMock.Verify(m => m.Send(It.Is<CreateScan>(c => c.CardId == 124 && c.LocationId == 432)));
                }
            }
        }

        public class WhenCardIsScannedWithNoValidPass
        {
            [Fact]
            public void ResultShouldBeFalse()
            {
                using (PassContext context =
                        new PassContext(InMemoryDbContextOptionsFactory.Create<PassContext>()))
                {

                    var mediatorMock = new Mock<IMediator>();
                    mediatorMock.Setup(m => m.Send(It.Is<ResolvePass>(r => r.CardId == 124 && r.LocationId == 432))).Returns(default(Pass));

                    var dateServiceMock = new Mock<IDateService>();
                    dateServiceMock.Setup(d => d.Now()).Returns(DateTime.Now);

                    var controller = new ScanCardController(context, mediatorMock.Object, dateServiceMock.Object);

                    var result = controller.Get(124, 432);

                    Assert.IsType<OkObjectResult>(result);
                    OkObjectResult okObjectResult = (OkObjectResult)result;
                    Assert.Equal(false, okObjectResult.Value);
                }
            }
        }


        public class WhenCardIsScannedWithValidPassWithNoPreviousActivation
        {
            [Fact]
            public void ResultShouldBeTrue()
            {
                using (PassContext context =
                        new PassContext(InMemoryDbContextOptionsFactory.Create<PassContext>()))
                {
                    var pass = new Pass { CardId = 124, CreatedOn = DateTime.Today.AddDays(-4)};
                    context.Passes.Add(pass);
                    context.SaveChanges();

                    var mediatorMock = new Mock<IMediator>();
                    mediatorMock.Setup(m => m.Send(It.Is<ResolvePass>(r => r.CardId == 124 && r.LocationId == 432))).Returns(pass);

                    var dateServiceMock = new Mock<IDateService>();
                    dateServiceMock.Setup(d => d.Now()).Returns(DateTime.Now);

                    var controller = new ScanCardController(context, mediatorMock.Object, dateServiceMock.Object);

                    var result = controller.Get(124, 432);

                    Assert.IsType<OkObjectResult>(result);
                    OkObjectResult okObjectResult = (OkObjectResult)result;
                    Assert.Equal(true, okObjectResult.Value);
                }
            }

            [Fact]
            public void ThePassShouldBeActivated()
            {
                using (PassContext context =
                        new PassContext(InMemoryDbContextOptionsFactory.Create<PassContext>()))
                {
                    var pass = new Pass { CardId = 124, CreatedOn = DateTime.Today.AddDays(-4) };
                    context.Passes.Add(pass);
                    context.SaveChanges();

                    var mediatorMock = new Mock<IMediator>();
                    mediatorMock.Setup(m => m.Send(It.Is<CreateScan>(c => c.CardId == 124 && c.LocationId == 432))).Returns(555);
                    mediatorMock.Setup(m => m.Send(It.Is<ResolvePass>(r => r.CardId == 124 && r.LocationId == 432))).Returns(pass);

                    var dateServiceMock = new Mock<IDateService>();
                    dateServiceMock.Setup(d => d.Now()).Returns(DateTime.Now);

                    var controller = new ScanCardController(context, mediatorMock.Object, dateServiceMock.Object);

                    var result = controller.Get(124, 432);

                    mediatorMock.Verify(m => m.Send(It.Is<ActivatePass>(p => p.PassId == pass.Id && p.ScanId == 555)));
                }
            }

            //Pass should be activated
        }

        public class WhenCardIsScannedWithValidPassWithPreviousActivation
        {
            [Fact]
            public void ResultShouldBeTrue()
            {
                using (PassContext context =
                        new PassContext(InMemoryDbContextOptionsFactory.Create<PassContext>()))
                {
                    var pass = new Pass { CardId = 124, CreatedOn = DateTime.Today.AddDays(-4) };
                    context.Passes.Add(pass);
                    context.SaveChanges();

                    var passActivation = new PassActivation { PassId = pass.Id, ScanId = 555 };
                    context.PassActivations.Add(passActivation);
                    context.SaveChanges();

                    var mediatorMock = new Mock<IMediator>();
                    mediatorMock.Setup(m => m.Send(It.Is<ResolvePass>(r => r.CardId == 124 && r.LocationId == 432))).Returns(pass);

                    var dateServiceMock = new Mock<IDateService>();
                    dateServiceMock.Setup(d => d.Now()).Returns(DateTime.Now);

                    var controller = new ScanCardController(context, mediatorMock.Object, dateServiceMock.Object);

                    var result = controller.Get(124, 432);

                    Assert.IsType<OkObjectResult>(result);
                    OkObjectResult okObjectResult = (OkObjectResult)result;
                    Assert.Equal(true, okObjectResult.Value);
                }
            }

            [Fact]
            public void ThePassShouldNotBeActivatedAgain()
            {
                using (PassContext context =
                        new PassContext(InMemoryDbContextOptionsFactory.Create<PassContext>()))
                {
                    var pass = new Pass { CardId = 124, CreatedOn = DateTime.Today.AddDays(-4) };
                    context.Passes.Add(pass);
                    context.SaveChanges();

                    var passActivation = new PassActivation { PassId = pass.Id, ScanId = 555 };
                    context.PassActivations.Add(passActivation);
                    context.SaveChanges();

                    var mediatorMock = new Mock<IMediator>();
                    mediatorMock.Setup(m => m.Send(It.Is<CreateScan>(c => c.CardId == 124 && c.LocationId == 432))).Returns(555);
                    mediatorMock.Setup(m => m.Send(It.Is<ResolvePass>(r => r.CardId == 124 && r.LocationId == 432))).Returns(pass);

                    var dateServiceMock = new Mock<IDateService>();
                    dateServiceMock.Setup(d => d.Now()).Returns(DateTime.Now);

                    var controller = new ScanCardController(context, mediatorMock.Object, dateServiceMock.Object);

                    var result = controller.Get(124, 432);

                    mediatorMock.Verify(m => m.Send(It.Is<ActivatePass>(p => p.PassId == pass.Id && p.ScanId == 555)),Times.Never);
                }
            }
        }
    }
}

