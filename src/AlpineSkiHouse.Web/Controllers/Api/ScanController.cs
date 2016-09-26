using AlpineSkiHouse.Data;
using AlpineSkiHouse.Models;
using AlpineSkiHouse.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AlpineSkiHouse.Controllers.Api
{
    [Route("api/[controller]")]
    public class ScanController : Controller
    {
        private readonly PassContext _context;
        private readonly ICardValidator _cardValidator;
        public ScanController(PassContext context, ICardValidator passValidator)
        {
            _cardValidator = passValidator;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Scan scan)
        {
            _context.Scans.Add(scan);
            await _context.SaveChangesAsync();

            if(_cardValidator.IsValid(scan.CardId, scan.LocationId))
                return StatusCode((int)HttpStatusCode.Created);
            return StatusCode((int)HttpStatusCode.Unauthorized);
        }
    }
}
