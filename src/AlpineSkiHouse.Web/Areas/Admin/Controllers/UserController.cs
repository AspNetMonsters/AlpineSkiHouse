using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlpineSkiHouse.Areas.Admin.Controllers
{
    [Produces("application/json")]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}