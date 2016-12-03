using Microsoft.AspNetCore.Mvc;
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
