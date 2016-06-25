using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlpineSkiHouse.Models.SkiCardViewModels
{
    public class EditSkiCardViewModel
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(70)]
        [Required]
        public string CardHolderFirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(70)]
        public string CardHolderLastName { get; set; }

        [Display(Name = "Birth Date")]
        [Required]
        public DateTime? CardHolderBirthDate { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        public string CardHolderPhoneNumber { get; set; }

    }
}
