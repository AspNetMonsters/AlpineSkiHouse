using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace AlpineSkiHouse.Models.SkiCardViewModels
{
    public class CreateSkiCardViewModel
    {
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

        [Display(Name = "Image to be Displayed on Card")]
        public IFormFile CardImage { set; get; }
    }
}
