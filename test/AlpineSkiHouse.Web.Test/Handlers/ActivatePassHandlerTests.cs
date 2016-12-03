using AlpineSkiHouse.Data;
using AlpineSkiHouse.Events;
using AlpineSkiHouse.Web.Handlers;
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
    public class ActivatePassHandlerTests
    {
        public class When_handling_activate_pass_command
        {
            ActivatePass activatePass = new ActivatePass
            {
                PassId = 15,
                ScanId = 45
            };

            private PassContext GetContext()
            {
                return new PassContext(InMemoryDbContextOptionsFactory.Create<PassContext>());
            }

            [Fact]
            public void A_new_pass_activation_is_saved_to_the_database()
            {
                using (PassContext context = GetContext())
                {
                    var currentDate = DateTime.UtcNow;
                    Mock<IMediator> mediatorMock = new Mock<IMediator>();

                    var sut = new ActivatePassHandler(context, mediatorMock.Object);
                    sut.Handle(activatePass);

                    Assert.Equal(1, context.PassActivations.Count());
                    var passActivateThatWasAdded = context.PassActivations.Single();
                    Assert.Equal(activatePass.PassId, passActivateThatWasAdded.PassId);
                    Assert.Equal(activatePass.ScanId, passActivateThatWasAdded.ScanId);
                }
            }

            [Fact]
            public void The_pass_activated_event_is_raised()
            {
                using (PassContext context = GetContext())
                {
                    Mock<IMediator> mediatorMock = new Mock<IMediator>();

                    var sut = new ActivatePassHandler(context, mediatorMock.Object);
                    var activationId = sut.Handle(activatePass);

                    mediatorMock.Verify(m => m.Publish(It.Is<PassActivated>(c => c.PassActivationId == activationId)));
                }
            }
        }
    }
}
