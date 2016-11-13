using AlpineSkiHouse.Data;
using AlpineSkiHouse.Models;
using AlpineSkiHouse.Services;
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
    public class PassValidityCheckerTests
    {
        private Mock<IDateService> _mockDateService;
        private PassContext _passContext;
        private PassTypeContext _passTypeContext;

        public PassValidityCheckerTests()
        {
            _mockDateService = new Mock<IDateService>();
            _passContext = new PassContext(InMemoryDbContextOptionsFactory.Create<PassContext>());
            _passTypeContext = new PassTypeContext(InMemoryDbContextOptionsFactory.Create<PassTypeContext>());
        }

        private PassValidityChecker GetInstance()
        {
            return new PassValidityChecker(_mockDateService.Object, _passContext, _passTypeContext);
        }

        [Fact]
        public void Should_be_invalid_if_current_time_is_after_valid_date()
        {
            _mockDateService.Setup(x => x.Now()).Returns(new DateTime(2012, 01, 01));
            var passType = new PassType
            {
                ValidFrom = new DateTime(2010, 01, 01),
                ValidTo = new DateTime(2011, 01, 01)
            };
            _passTypeContext.PassTypes.Add(passType);
            _passTypeContext.SaveChanges();
            var pass = new Pass
            {
                CreatedOn = DateTime.UtcNow,
                PassTypeId = passType.Id
            };
            _passContext.Add(pass);
            _passContext.SaveChanges();
            var checker = GetInstance();
            Assert.False(checker.IsValid(pass.Id));
        }

        [Fact]
        public void Should_be_invalid_if_current_time_is_before_valid_date()
        {
            _mockDateService.Setup(x => x.Now()).Returns(new DateTime(2001, 01, 01));
            var passType = new PassType
            {
                ValidFrom = new DateTime(2010, 01, 01),
                ValidTo = new DateTime(2011, 01, 01)
            };
            _passTypeContext.PassTypes.Add(passType);
            _passTypeContext.SaveChanges();
            var pass = new Pass
            {
                CreatedOn = DateTime.UtcNow,
                PassTypeId = passType.Id
            };
            _passContext.Add(pass);
            _passContext.SaveChanges();
            var checker = GetInstance();
            Assert.False(checker.IsValid(pass.Id));
        }

        [Fact]
        public void Should_be_invalid_if_current_time_is_in_window_and_activations_equal_to_allowed()
        {
            _mockDateService.Setup(x => x.Now()).Returns(new DateTime(2010, 06, 01));
            var passType = new PassType
            {
                ValidFrom = new DateTime(2010, 01, 01),
                ValidTo = new DateTime(2011, 01, 01),
                MaxActivations = 1
            };
            _passTypeContext.PassTypes.Add(passType);
            _passTypeContext.SaveChanges();
            var pass = new Pass
            {
                CreatedOn = DateTime.UtcNow,
                PassTypeId = passType.Id,
                Activations = new List<PassActivation>
                {
                    new PassActivation { Scan = new Scan() }
                }
            };
            _passContext.Add(pass);
            _passContext.SaveChanges();
            var checker = GetInstance();
            Assert.False(checker.IsValid(pass.Id));
        }

        [Fact]
        public void Should_be_invalid_if_current_time_is_in_window_and_activations_greater_than_allowed()
        {
            _mockDateService.Setup(x => x.Now()).Returns(new DateTime(2010, 06, 01));
            var passType = new PassType
            {
                ValidFrom = new DateTime(2010, 01, 01),
                ValidTo = new DateTime(2011, 01, 01),
                MaxActivations = 1
            };
            _passTypeContext.PassTypes.Add(passType);
            _passTypeContext.SaveChanges();
            var pass = new Pass
            {
                CreatedOn = DateTime.UtcNow,
                PassTypeId = passType.Id,
                Activations = new List<PassActivation>
                {
                    new PassActivation(),
                    new PassActivation()
                }
            };
            _passContext.Add(pass);
            _passContext.SaveChanges();
            var checker = GetInstance();
            Assert.False(checker.IsValid(pass.Id));
        }

        [Fact]
        public void Should_be_invalid_if_pass_does_not_exist()
        {
            var checker = GetInstance();
            Assert.False(checker.IsValid(1));
        }

        [Fact]
        public void Should_be_valid_if_current_time_is_in_window_and_activations_equal_to_allowed_and_last_activation_today()
        {
            var now = new DateTime(2010, 06, 01);
            _mockDateService.Setup(x => x.Now()).Returns(now);
            var passType = new PassType
            {
                ValidFrom = new DateTime(2010, 01, 01),
                ValidTo = new DateTime(2011, 01, 01),
                MaxActivations = 1
            };
            _passTypeContext.PassTypes.Add(passType);
            _passTypeContext.SaveChanges();
            var pass = new Pass
            {
                CreatedOn = DateTime.UtcNow,
                PassTypeId = passType.Id,
                Activations = new List<PassActivation>
                {
                    new PassActivation { Scan = new Scan
                        {
                            DateTime = now
                        }
                    }
                }
            };
            _passContext.Add(pass);
            _passContext.SaveChanges();
            var checker = GetInstance();
            Assert.True(checker.IsValid(pass.Id));
        }
        [Fact]
        public void Should_be_valid_if_current_time_is_in_window_and_activations_less_than_allowed()
        {
            _mockDateService.Setup(x => x.Now()).Returns(new DateTime(2010, 06, 01));
            var passType = new PassType
            {
                ValidFrom = new DateTime(2010, 01, 01),
                ValidTo = new DateTime(2011, 01, 01),
                MaxActivations = 10
            };
            _passTypeContext.PassTypes.Add(passType);
            _passTypeContext.SaveChanges();
            var pass = new Pass
            {
                CreatedOn = DateTime.UtcNow,
                PassTypeId = passType.Id,
                Activations = new List<PassActivation>()
            };
            _passContext.Add(pass);
            _passContext.SaveChanges();
            var checker = GetInstance();
            Assert.True(checker.IsValid(pass.Id));
        }
    }
}
