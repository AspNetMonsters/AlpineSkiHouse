using AlpineSkiHouse.Data;
using AlpineSkiHouse.Events;
using AlpineSkiHouse.Handlers;
using AlpineSkiHouse.Services;
using AlpineSkiHouse.Web.Command;
using AlpineSkiHouse.Web.Tests.Data;
using MediatR;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace AlpineSkiHouse.Web.Tests.Handlers
{
    public class CreateScanHandlerTests
    {
        public class When_handling_create_scan_command
        {
            CreateScan createScan = new CreateScan
            {
                CardId = 115,
                LocationId = 451
            };

            private PassContext GetContext()
            {
                return new PassContext(InMemoryDbContextOptionsFactory.Create<PassContext>());
            }

            [Fact]
            public void A_new_scan_is_saved_to_the_database()
            {
                using (PassContext context = GetContext())
                {
                    var dateService = new Mock<IDateService>();
                    var currentDate = DateTime.UtcNow;
                    dateService.Setup(x => x.Now()).Returns(currentDate);
                    Mock<IMediator> mediatorMock = new Mock<IMediator>();

                    var sut = new CreateScanHandler(context, dateService.Object, mediatorMock.Object);
                    sut.Handle(createScan);

                    Assert.Equal(1, context.Scans.Count());
                    var scanThatWasAdded = context.Scans.Single();
                    Assert.Equal(createScan.CardId, scanThatWasAdded.CardId);
                    Assert.Equal(createScan.LocationId, scanThatWasAdded.LocationId);
                    Assert.Equal(currentDate, scanThatWasAdded.DateTime);
                }
            }

            [Fact]
            public void The_card_scanned_event_is_raised()
            {
                using (PassContext context = GetContext())
                {
                    var dateService = new Mock<IDateService>();
                    var currentDate = DateTime.UtcNow;
                    dateService.Setup(x => x.Now()).Returns(currentDate);
                    Mock<IMediator> mediatorMock = new Mock<IMediator>();

                    var sut = new CreateScanHandler(context, dateService.Object, mediatorMock.Object);
                    var scanId = sut.Handle(createScan);

                    mediatorMock.Verify(m => m.Publish(It.Is<CardScanned>(c => c.ScanId == scanId)));
                }
            }
        }
    }
}
