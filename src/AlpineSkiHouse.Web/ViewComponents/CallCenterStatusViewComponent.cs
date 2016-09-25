using AlpineSkiHouse.Models.CallCenterViewModels;
using AlpineSkiHouse.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlpineSkiHouse.ViewComponents
{
    public class CallCenterStatusViewComponent : ViewComponent
    {
        private readonly ICsrInformationService _csrInformationService;

        public CallCenterStatusViewComponent(ICsrInformationService csrInformationService)
        {
            _csrInformationService = csrInformationService;
        }

        public IViewComponentResult Invoke()
        {
            if (_csrInformationService.CallCenterOnline)
            {
                var viewModel = new CallCenterStatusViewModel
                {
                    OnlineRepresentatives = _csrInformationService.OnlineRepresentatives,
                    PhoneNumber = _csrInformationService.CallCenterPhoneNumber
                };
                return View(viewModel);
            }
            else
            {
                return View("Closed");
            }
        }
    }
}
