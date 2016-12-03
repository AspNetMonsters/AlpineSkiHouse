using AlpineSkiHouse.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlpineSkiHouse.Web.Queries
{
    public class ResolvePass : IRequest<Pass>
    {
        public int CardId { get; set; }

        public int LocationId { get; set; }

        public DateTime DateTime { get; set; }
    }


}
