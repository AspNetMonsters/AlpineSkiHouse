using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AlpineSkiHouse.Models;
using AlpineSkiHouse.Web.Areas.Admin.Models;

namespace AlpineSkiHouse.Web.Areas.Admin.Controllers
{
    [Produces("application/json")]
    [Route("Admin/api/Users")]
    [Authorize]
    public class UsersController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IEnumerable<User> Index()
        {
            return _userManager.Users.Select(x => new User
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                LockoutEnabled = x.LockoutEnabled
            });
        }

        [HttpPut]
        [Route("Lock/{userId}")]
        public async Task Lockout(Guid userId)
        {
            var user = _userManager.Users.Where(x => x.Id == userId.ToString()).Single();
            await _userManager.SetLockoutEnabledAsync(user, true);
        }

        [HttpPut]
        [Route("Unlock/{userId}")]
        public async Task Unlock(Guid userId)
        {
            var user = _userManager.Users.Where(x => x.Id == userId.ToString()).Single();
            await _userManager.SetLockoutEnabledAsync(user, false);
        }
    }
}