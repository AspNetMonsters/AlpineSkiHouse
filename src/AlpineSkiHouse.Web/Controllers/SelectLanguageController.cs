using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace AlpineSkiHouse.Web.Controllers
{
    public class SelectLanguageController : Controller
    {
        private readonly RequestLocalizationOptions _requestLocalizationOptions;

        public SelectLanguageController(IOptions<RequestLocalizationOptions> requestLocalizationOptions)
        {
            _requestLocalizationOptions = requestLocalizationOptions.Value;
        }

        public IActionResult Index(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(_requestLocalizationOptions.SupportedUICultures);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetLanguage(string cultureName, string returnUrl = null)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cultureName)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
    }
}
