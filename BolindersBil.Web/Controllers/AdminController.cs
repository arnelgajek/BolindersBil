using BolindersBil.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AdminController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            // Checks if the user is authenticated/signed in and redirects him/her to Admin: 
            if (User.Identity.IsAuthenticated)
            {
                return View("Admin");
            }
            else
            {
                // Otherwise, maybe return to an error page?
                return View();
            }
        }
        
        // Checks if the password matches to the account, redirects the user to Admin:
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(AdminViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(vm.UserName);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    if ((await _signInManager.PasswordSignInAsync(user, vm.Password, false, false)).Succeeded)
                        {
                        return RedirectToAction("Index", "Admin");
                        }
                }
            }
            return View("Index", vm);
        }

        public IActionResult Admin()
        {
            return View();
        }
        // Sends the user back to the login page:
        [HttpDelete]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
