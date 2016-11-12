using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AlpineSkiHouse.Web.Controllers
{
    public class ConditionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}