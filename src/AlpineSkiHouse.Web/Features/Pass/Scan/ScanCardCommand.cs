using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlpineSkiHouse.Features.Pass.Scan
{
    public class ScanCardCommand : IAsyncRequest<ScanCardResult>
    {
        public int CardId { get; set; }
        public int LocationId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
