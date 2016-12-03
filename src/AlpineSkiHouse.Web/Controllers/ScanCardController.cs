using Microsoft.AspNetCore.Mvc;
using AlpineSkiHouse.Web.Services;
using MediatR;
using AlpineSkiHouse.Web.Command;
using AlpineSkiHouse.Data;
using System.Linq;
using AlpineSkiHouse.Models;
using AlpineSkiHouse.Web.Queries;
using AlpineSkiHouse.Services;

namespace AlpineSkiHouse.Web.Controllers
{
    public class ScanCardController : Controller
    {
        private readonly IMediator _mediator;
        private readonly PassContext _passContext;
        private readonly IDateService _dateService;

        public ScanCardController(PassContext passContext, IMediator mediator, IDateService dateService)
        {
            _passContext = passContext;
            _mediator = mediator;
            _dateService = dateService;
        }        

        /// <summary>
        /// Called by the RFID scanners to validate a ski card at a lift location
        /// </summary>
        /// <param name="cardId"></param>
        /// <param name="locationId"></param>
        /// <returns></returns>
        public IActionResult Get(int cardId, int locationId)
        {
            var scanId = _mediator.Send(new CreateScan { CardId = cardId, LocationId = locationId });

            Pass pass = _mediator.Send(new ResolvePass { CardId = cardId, LocationId = locationId, DateTime =_dateService.Now() });

            if (pass == null)
            {
                //TODO: We might want to publish an event that a scan did not find a valid pass
                return Ok(false);
            }

            if (!_passContext.PassActivations.Any(p => p.PassId == pass.Id))
            {
                _mediator.Send(new ActivatePass { PassId = pass.Id, ScanId = scanId });
            }
            
            //TODO: We might want to publish an event that a pass was validated at the current location
            return Ok(true);
        }
    }
}
