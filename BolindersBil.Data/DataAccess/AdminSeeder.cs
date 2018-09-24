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
    public class AdminSeeder : IAdminSeeder
    {
        private const string _admin = "admin";
        private const string _password = "buggeroff";

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
                    EmailConfirmed = true,
                }, _password);
            }

            //if (!_context.Admins.Any())
            //{
            //    ctx.Admins.Add(new Admin { FirstName = "Mattias", LastName = "Jarl" });
            //    _context.Admins.Add(new Admin { FirstName = "Mallory", LastName = "Fraiche" });
            //    _context.Admins.Add(new Admin { FirstName = "Timmie", LastName = "Bark" });
            //    _context.Admins.Add(new Admin { FirstName = "Arnel", LastName = "Gajek" });
            //    _context.SaveChanges();
            //}

            return true;
        }
    }
}
