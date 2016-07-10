using System.ComponentModel.DataAnnotations;

namespace AlpineSkiHouse.Models
{
    public class PassTypePrice
    {
        public int Id { get; set; }

        [Range(0, 120)]
        public int MinAge { get; set; }

        [Range(0, 120)]
        public int MaxAge { get; set; }

        public decimal Price { get; set; }

        public int PassTypeId { get; set; }
    }
}