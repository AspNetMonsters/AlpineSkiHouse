using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AlpineSkiHouse.Models;

namespace AlpineSkiHouse.Data
{
    /// <summary>
    /// Information related to application user profiles, user roles, user claims and user tokens
    /// </summary>
    public class ApplicationUserContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationUserContext(DbContextOptions<ApplicationUserContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
