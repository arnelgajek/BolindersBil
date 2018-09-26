using BolindersBil.Models;
using BolindersBil.Web.DataAccess;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolindersBil.Data.DataAccess
{
    public class IdentitySeeder : IIdentitySeeder
    {
        private const string _admin = "admin@bolindersbil.se";
        private const string _password = "buggeroff";

        // Constructor so we can use Dependency Injection from DB.
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IdentitySeeder(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> CreateAdminAccountIfEmpty()
        {
            if (!_context.Users.Any(u => u.UserName == _admin))
            {
                var user = new IdentityUser
                {
                    UserName = _admin,
                    Email = "admin@bolindersbil.se",
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, _password);
                var test = result.Succeeded;
            }

            return true;
        }
    }
}
