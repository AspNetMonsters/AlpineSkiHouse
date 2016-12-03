using AlpineSkiHouse.Data;
using AlpineSkiHouse.Models;
using AlpineSkiHouse.Services;
using AlpineSkiHouse.Web.Handlers;
using AlpineSkiHouse.Web.Services;
using AlpineSkiHouse.Web.Tests.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AlpineSkiHouse.Web.Tests.Services
{
    public class PassResolverTests
    {
        private Mock<IDateService> _mockDateService;
        private PassContext _passContext;
        private PassTypeContext _passTypeContext;

        public PassResolverTests()
        {
            _mockDateService = new Mock<IDateService>();
            _passContext = new PassContext(InMemoryDbContextOptionsFactory.Create<PassContext>());
            _passTypeContext = new PassTypeContext(InMemoryDbContextOptionsFactory.Create<PassTypeContext>());
        }

        [Fact]
        public void Should_check_context_using_provided_card_id()
        {
            var context = new PassContext(InMemoryDbContextOptionsFactory.Create<PassContext>());
            var cardId = 1337;
            var verifyingPassId = 7331;
            context.Passes.Add(new Pass { CardId = cardId, Id = 7330 });
            context.Passes.Add(new Pass { CardId = cardId, Id = verifyingPassId });
            context.Passes.Add(new Pass { CardId = cardId, Id = 7332 });
            context.SaveChanges();

            var validator = new Mock<IPassValidityChecker>();
            validator.Setup(c => c.IsValid(It.IsAny<int>()))
                .Returns<int>(i => context.Passes.Any(p=>p.CardId == i));

            var handler = new ResolvePassHandler(context, validator.Object);

            handler.Handle(new Queries.ResolvePass { CardId = cardId });

            validator.Verify(v => v.IsValid(It.Is<int>(i => i == verifyingPassId)), Times.Once);
            validator.Verify(v => v.IsValid(It.IsAny<int>()), Times.Exactly(3));

        }

    }
}
