using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlpineSkiHouse.Models
{
    public class PassType
    {
        public PassType()
        {
            PassTypeResorts = new List<PassTypeResort>();
            Prices = new List<PassTypePrice>();
        }

        public int Id { get; set; }

        [MaxLength(255)]
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }

        /// <summary>
        /// Maximum number of times a pass of this type can be activated.
        /// For example a standard day pass would have max activations of 1.
        ///             an annual pass might have max activations of 265 (number of days the ski hill is open)
        /// </summary>
        public int MaxActivations { get; set; }

        public List<PassTypeResort> PassTypeResorts { get; set; }

        public List<PassTypePrice> Prices { get; set; }
    }
}
