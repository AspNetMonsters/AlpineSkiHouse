using AlpineSkiHouse.Data;
using AlpineSkiHouse.Events;
using AlpineSkiHouse.Handlers;
using AlpineSkiHouse.Services;
using AlpineSkiHouse.Web.Tests.Data;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace AlpineSkiHouse.Web.Tests.Handlers
{
    public class LogCardScannedTests
    {
        public class When_handling_card_scanned_event
        {
            CardScanned cardScanned = new CardScanned
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

                    var sut = new LogCardScanned(context, dateService.Object);
                    sut.Handle(cardScanned);

                    Assert.Equal(1, context.Scans.Count());
                    var scanThatWasAdded = context.Scans.Single();
                    Assert.Equal(cardScanned.CardId, scanThatWasAdded.CardId);
                    Assert.Equal(cardScanned.LocationId, scanThatWasAdded.LocationId);
                    Assert.Equal(currentDate, scanThatWasAdded.DateTime);
                }
            }
        }
    }
}
