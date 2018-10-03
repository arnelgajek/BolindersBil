using BolindersBil.Models;
using BolindersBil.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;



namespace BolindersBil.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private IVehicleRepository vehicleRepo;

        public AccountController(IVehicleRepository vehicleRepository, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            vehicleRepo = vehicleRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            // Checks if the user is authenticated/signed in and redirects him/her to Admin: 
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Admin", "Vehicle");
            }
            else
            {
                // Otherwise, maybe return to an error page?
                return View();
            }
        }

        // Checks if the password matches to the account, redirects the user to Admin:
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(vm.UserName);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    if ((await _signInManager.PasswordSignInAsync(user, vm.Password, false, false)).Succeeded)
                    {
                        return RedirectToAction("Admin", "Vehicle");
                    }
                }
            }
            return View("Index", vm);
        }
        
        // Sends the user back to the login page:
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
