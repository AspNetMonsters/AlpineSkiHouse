using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlpineSkiHouse.Web.Services;

namespace AlpineSkiHouse.Web.Controllers
{
    public class PassValidationController : Controller
    {
        private readonly IPassValidityChecker _passValidityChecker;

        public PassValidationController(IPassValidityChecker passValidityChecker)
        {
            _passValidityChecker = passValidityChecker;
        }        
    }
}
