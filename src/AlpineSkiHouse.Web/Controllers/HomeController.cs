using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AlpineSkiHouse.Services;
using Microsoft.Extensions.Logging;

namespace AlpineSkiHouse.Web.Controllers
{
    public class HomeController : Controller
    {
        private ICsrInformationService _service;
        private ILogger<HomeController> _logger;

        public HomeController(ICsrInformationService service, ILogger<HomeController> logger)
        {
            _logger = logger;
            _logger.LogWarning("The application client secret key was less than the expected length.");
            _service = service;
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return View();
            return RedirectToAction("LoggedIn");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult LoggedIn()
        {

            return View();
        }

        public IActionResult Contact()
        {
            _logger.LogDebug("User visited contact page at {VisitTime}", DateTime.Now);
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
