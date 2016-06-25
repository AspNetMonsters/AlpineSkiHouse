using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlpineSkiHouse.Models
{
    public class Resort
    {
        public Resort() 
        {
            Locations = new HashSet<Location>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Location> Locations { get; set; }
    }
}
