using BolindersBil.Web.DataAccess;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BolindersBil.Data.DataAccess
{
    public class AdminSeeder : IAdminSeeder
    {
        private const string _admin = "admin";
        private const string _password = "password123";

        // Constructor so we can use Dependency Injection from DB.
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public AdminSeeder(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> CreateAdminAccountIfEmpty()
        {
            if (!_context.Users.Any(u => u.UserName == _admin))
            {
                await _userManager.CreateAsync(new IdentityUser
                {
                    UserName = _admin,
                    Email = "admin@bolindersbil.se",
                    EmailConfirmed = true
                }, _password);
            }

            return true;
        }
    }
}
