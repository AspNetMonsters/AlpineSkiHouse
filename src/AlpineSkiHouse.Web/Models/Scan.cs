using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlpineSkiHouse.Models
{
    public class Scan
    {
        public int Id { get; set; }

        public int CardId { get; set; }

        public int LocationId { get; set; }

        public DateTime DateTime { get; set; }
    }
}
