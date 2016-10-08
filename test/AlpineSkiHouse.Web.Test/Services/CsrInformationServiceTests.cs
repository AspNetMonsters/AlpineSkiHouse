using AlpineSkiHouse.Configuration;
using AlpineSkiHouse.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Xunit;
using Moq;

namespace AlpineSkiHouse.Web.Tests.Services
{
public class CsrInformationServiceTests
{
    public class GivenThereIsAtLeastOneRepresentativeOnline
    {
        public class GivenThereAreNoRepresentativesOnline
        {
            private IOptions<CsrInformationOptions> options;

            public GivenThereAreNoRepresentativesOnline()
            {
                options = Options.Create(new CsrInformationOptions());
                options.Value.OnlineRepresentatives = 0;
            }

            [Fact]
            public void CallCenterOnlineShouldBeFalse()
            {
                var service = new CsrInformationService(options, new Mock<ILogger<CsrInformationService>>().Object);
                Assert.False(service.CallCenterOnline);
            }
        }

        public static readonly List<object[]> options = new List<object[]>
        {
            new object[] {Options.Create(new CsrInformationOptions { OnlineRepresentatives = 1} )},
            new object[] {Options.Create(new CsrInformationOptions { OnlineRepresentatives = 2} )},
            new object[] {Options.Create(new CsrInformationOptions { OnlineRepresentatives = 3} )},
            new object[] {Options.Create(new CsrInformationOptions { OnlineRepresentatives = 1000} )},
            new object[] {Options.Create(new CsrInformationOptions { OnlineRepresentatives = 100000} )}
        };

        [Theory]
        [MemberData(nameof(options))]
        public void CallCenterOnlineShouldBeTrue(IOptions<CsrInformationOptions> options)
        {
            var service = new CsrInformationService(options, new Mock<ILogger<CsrInformationService>>().Object);
            Assert.True(service.CallCenterOnline);
        }

        [Theory]
        [MemberData(nameof(options))]
        public void OnlineRepresentativesShouldMatchOptionsSource(IOptions<CsrInformationOptions> options)
        {
            var service = new CsrInformationService(options, new Mock<ILogger<CsrInformationService>>().Object);
            Assert.Equal(options.Value.OnlineRepresentatives, service.OnlineRepresentatives);
        }
    }        
}
}
