using AlpineSkiHouse.Web.Controllers;
using Microsoft.AspNetCore.Builder;
using System.Globalization;
using Xunit;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using Moq;
using Microsoft.AspNetCore.Http;

namespace AlpineSkiHouse.Web.Tests.Controllers
{
    public class SelectLanguageControllerTests
    {
        public class WhenViewingSelectLanguagePage
        {
            [Fact]
            public void ShouldDisplaySupportedUICultures()
            {
                var options = new RequestLocalizationOptions
                {
                    SupportedUICultures = new[] { new CultureInfo("en-CA"), new CultureInfo("fr-CA") }
                };
                var optionsAccessor = Options.Create(options);
                var controller = new SelectLanguageController(optionsAccessor);

                var result = controller.Index();

                Assert.NotNull(result);
                Assert.IsType<ViewResult>(result);
                ViewResult viewResult = (ViewResult) result;
                Assert.Equal(options.SupportedUICultures, viewResult.Model);
            }
        }

        public class WhenSelectingASupportedLanguage
        {
            [Fact]
            public void ShouldAddCultureCookieToResponse()
            {
                var options = new RequestLocalizationOptions
                {
                    SupportedUICultures = new[] { new CultureInfo("en-CA"), new CultureInfo("fr-CA") }
                };
                var optionsAccessor = Options.Create(options);

                var httpContextMock = new Mock<HttpContext>();
                var responseMock = new Mock<HttpResponse>();
                var cookiesMock = new Mock<IResponseCookies>();

                httpContextMock.SetupGet(h => h.Response).Returns(responseMock.Object);
                responseMock.SetupGet(r => r.Cookies).Returns(cookiesMock.Object);
                cookiesMock.Setup(r => r.Append(CookieRequestCultureProvider.DefaultCookieName,
                                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("fr-CA")),
                                    It.IsAny<CookieOptions>()));

                var controller = new SelectLanguageController(optionsAccessor)
                {
                    ControllerContext = new ControllerContext()
                    {
                        HttpContext = httpContextMock.Object
                    }
                };


                var result = controller.SetLanguage("fr-CA");

                Assert.NotNull(controller.Response.Cookies);
                cookiesMock.VerifyAll();
            }
        }
    }
}
