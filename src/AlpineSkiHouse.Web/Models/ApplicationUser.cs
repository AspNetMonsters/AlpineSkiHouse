using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AlpineSkiHouse.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(70)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(70)]
        public string LastName { get; set; }
    }
}
