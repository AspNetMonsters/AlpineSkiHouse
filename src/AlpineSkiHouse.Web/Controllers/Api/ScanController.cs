using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AlpineSkiHouse.Features.Pass.Scan;
using MediatR;

namespace AlpineSkiHouse.Controllers.Api
{
    [Route("api/[controller]")]
    public class ScanController : Controller
    {
        private readonly IMediator _mediator;
        public ScanController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ScanCardCommand scan)
        {
            if ((await _mediator.SendAsync(scan)).CardIsValid)
                return StatusCode((int)HttpStatusCode.Created);
            return StatusCode((int)HttpStatusCode.Unauthorized);
        }
    }
}
