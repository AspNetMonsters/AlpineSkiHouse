using AlpineSkiHouse.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlpineSkiHouse.Features.Pass.Scan
{
    public class CardScannedHandler : IAsyncNotificationHandler<CardScanned>
    {
        private readonly PassContext _context;
        public CardScannedHandler(PassContext context)
        {
            _context = context;
        }

        public async Task Handle(CardScanned notification)
        {
            var scan = new Models.Scan
            {
                CardId = notification.CardId,
                DateTime = notification.DateTime,
                LocationId = notification.LocationId
            };
            _context.Scans.Add(scan);
            await _context.SaveChangesAsync();
        }
    }
}
