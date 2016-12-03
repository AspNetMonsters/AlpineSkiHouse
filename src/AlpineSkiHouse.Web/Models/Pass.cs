using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlpineSkiHouse.Models
{
    public class Pass
    {
        public Pass()
        {
            this.Activations = new List<PassActivation>();
        }

        public int Id { get; set; }

        public int CardId { get; set; }

        [Display(Name ="Pass Type")]
        public int PassTypeId { get; set; }

        public DateTime CreatedOn { get; set; }

        public List<PassActivation> Activations { get; set; }
    }
}
