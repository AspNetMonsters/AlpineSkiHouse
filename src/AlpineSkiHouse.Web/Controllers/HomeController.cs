using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AlpineSkiHouse.Services;

namespace AlpineSkiHouse.Controllers
{
    public class HomeController : Controller
    {
        private ICsrInformationService _service;

        public HomeController(ICsrInformationService service)
        {
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
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
