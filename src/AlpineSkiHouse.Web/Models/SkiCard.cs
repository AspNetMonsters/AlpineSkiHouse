using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlpineSkiHouse.Models
{
    public class SkiCard
    {
        public int Id { get; set; }        

        /// <summary>
        /// The Id of the ApplicationUser who owns this ski card
        /// </summary>
        [MaxLength(450)]
        [Required]
        public string ApplicationUserId { get; set; }

        /// <summary>
        /// The date when the card was created
        /// </summary>
        public DateTime CreatedOn { get; set; }

        [MaxLength(70)]
        [Required]
        public string CardHolderFirstName { get; set; }

        [MaxLength(70)]
        public string CardHolderLastName { get; set; }

        public DateTime CardHolderBirthDate { get; set; }

        [Phone]
        public string CardHolderPhoneNumber { get; set; }

        public Guid? CardImageId { get; set; }
    }
}
