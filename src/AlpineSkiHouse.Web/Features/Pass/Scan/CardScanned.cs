using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlpineSkiHouse.Features.Pass.Scan
{
    public class CardScanned : IAsyncNotification
    {
        public int CardId { get; set; }
        public int LocationId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
