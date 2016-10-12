using AlpineSkiHouse.Data;
using AlpineSkiHouse.Events;
using AlpineSkiHouse.Handlers;
using AlpineSkiHouse.Services;
using AlpineSkiHouse.Web.Tests.Data;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AlpineSkiHouse.Web.Tests.Handlers
{
    public class AddSkiPassOnPurchaseCompletedTests
    {
        public class When_handling_purchase_completed
        {
            PassPurchased passPurchased = new PassPurchased
            {
                CardId = 1,
                PassTypeId = 2,
                DiscountCode = "2016springpromotion",
                PricePaid = 200m
            };

            private static PassContext GetContext()
            {
                return new PassContext(InMemoryDbContextOptionsFactory.Create<PassContext>());
            }
            [Fact]
            public void Pass_is_saved_to_the_database_for_each_pass()
            {
                using (PassContext context = GetContext())
                {
                    var mediator = new Mock<IMediator>();
                    var dateService = new Mock<IDateService>();
                    var currentDate = DateTime.UtcNow;
                    dateService.Setup(x => x.Now()).Returns(currentDate);
                    var sut = new AddSkiPassOnPurchaseCompleted(context, mediator.Object, dateService.Object);
                    sut.Handle(new Events.PurchaseCompleted
                    {
                        Passes = new List<PassPurchased>
                        {
                            passPurchased
                        }
                    });

                    Assert.Equal(1, context.Passes.Count());
                    Assert.Equal(passPurchased.CardId, context.Passes.Single().CardId);
                    Assert.Equal(passPurchased.PassTypeId, context.Passes.Single().PassTypeId);
                    Assert.Equal(currentDate, context.Passes.Single().CreatedOn);
                }
            }

            [Fact]
            public void PassesAddedEvents_is_published_for_each_pass()
            {
                using (PassContext context =
                        GetContext())
                {
                    var mediator = new Mock<IMediator>();
                    var dateService = new Mock<IDateService>();
                    var currentDate = DateTime.UtcNow;
                    dateService.Setup(x => x.Now()).Returns(currentDate);
                    var sut = new AddSkiPassOnPurchaseCompleted(context, mediator.Object, dateService.Object);

                    sut.Handle(new Events.PurchaseCompleted
                    {
                        Passes = new List<PassPurchased>
            {
                passPurchased
            }
                    });
                    var dbPass = context.Passes.Single();
                    mediator.Verify(x => x.Publish(It.Is<PassAdded>(y => y.CardId == passPurchased.CardId &&
                                                                            y.CreatedOn == currentDate &&
                                                                            y.PassId == dbPass.Id &&
                                                                            y.PassTypeId == passPurchased.PassTypeId)));
                }
            }

            [Fact]
            public void Empty_passes_collection_saves_nothing_to_the_database()
            {
                using (PassContext context = GetContext())
                {
                    var mediator = new Mock<IMediator>();
                    var dateService = new Mock<IDateService>();
                    var currentDate = DateTime.UtcNow;
                    dateService.Setup(x => x.Now()).Returns(currentDate);
                    var sut = new AddSkiPassOnPurchaseCompleted(context, mediator.Object, dateService.Object);
                    sut.Handle(new Events.PurchaseCompleted { Passes = new List<PassPurchased>() });

                    Assert.Equal(0, context.Passes.Count());
                }
            }

            [Fact]
            public void Empty_passes_collection_publishes_no_messages()
            {
                using (PassContext context = GetContext())
                {
                    var mediator = new Mock<IMediator>();
                    var dateService = new Mock<IDateService>();
                    var currentDate = DateTime.UtcNow;
                    dateService.Setup(x => x.Now()).Returns(currentDate);
                    var sut = new AddSkiPassOnPurchaseCompleted(context, mediator.Object, dateService.Object);

                    sut.Handle(new Events.PurchaseCompleted { Passes = new List<PassPurchased>() });

                    mediator.Verify(x => x.Publish(It.IsAny<PassAdded>()), Times.Never);
                }
            }
        }
    }
}
