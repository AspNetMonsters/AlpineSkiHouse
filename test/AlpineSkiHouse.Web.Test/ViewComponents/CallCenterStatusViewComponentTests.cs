using Xunit;
using AlpineSkiHouse.ViewComponents;
using Moq;
using AlpineSkiHouse.Services;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace AlpineSkiHouse.Web.Tests.ViewComponents
{
    public class CallCenterStatusViewComponentTests
    {
        public class GivenTheCallCenterIsClosed
        {
            [Fact]
            public void TheClosedViewShouldBeReturned()
            {
                var _csrInfoServiceMock = new Mock<ICsrInformationService>();
                _csrInfoServiceMock.Setup(c => c.CallCenterOnline).Returns(false);

                var viewComponent = new CallCenterStatusViewComponent(_csrInfoServiceMock.Object);

                var result = viewComponent.Invoke();

                Assert.IsType<ViewViewComponentResult>(result);
                var viewResult = result as ViewViewComponentResult;
                Assert.Equal("Closed", viewResult.ViewName);
            }
        }

        public class GivenTheCallCenterIsOpen
        {
            [Fact]
            public void TheDefaultViewShouldBeReturned()
            {
                var _csrInfoServiceMock = new Mock<ICsrInformationService>();
                _csrInfoServiceMock.Setup(c => c.CallCenterOnline).Returns(true);

                var viewComponent = new CallCenterStatusViewComponent(_csrInfoServiceMock.Object);

                var result = viewComponent.Invoke();

                Assert.IsType<ViewViewComponentResult>(result);
                var viewResult = result as ViewViewComponentResult;
                Assert.Null(viewResult.ViewName);
            }
        }
    }
}
