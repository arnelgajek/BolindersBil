using BolindersBil.Models;
using BolindersBil.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.Web.Controllers
{
    public class AdminController : Controller
    {
        private IVehicleRepository vehicleRepo;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;


        public AdminController(IVehicleRepository vehicleRepository, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            vehicleRepo = vehicleRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

       
        // TODO: maybe move all the vehicle repo DI and CRUD logic in a Vehicle controller instead.
        [HttpGet]
        public IActionResult AddNewVehicle()
        {
            // This list is used as the dropdown option in the "Årsmodell" input.
            List<object> years = new List<object>();
            var currentYear = DateTime.Now.Year;
            var theFuture = currentYear + 1;
            years.Add(theFuture);
            years.Add(currentYear);
            var stopYear = 1980;
            for (int y = currentYear; y >= stopYear; y--)
            {
                years.Add(y);
            }
            var seventies = "70-tal";
            var sixties = "60-tal";
            var fifties = "50-tal";
            var superOld = "40-tal eller äldre";
            years.Add(seventies);
            years.Add(sixties);
            years.Add(fifties);
            years.Add(superOld);
            ViewBag.vehicleYearOptions = years;

            // This list is used as the dropdown option in the "Karosstyp" input.
            List<string> bodyType = new List<string>
            {
                "Småbil",
                "Sedan",
                "Halvkombi",
                "Kombi",
                "SUV",
                "Coupé",
                "Cab",
                "Familjebuss",
                "Yrkesfordon"
            };
            ViewBag.bodyTypes = bodyType;

            // This list is used as the dropdown option in the "Bränsletyp" input.
            List<string> fuelType = new List<string>
            {
                "Bensin",
                "Diesel",
                "El",
                "Miljöbränsle/Hybrid"
            };
            ViewBag.fuelTypes = fuelType;

            // This list is used as the dropdown option in the "Anläggning" input.
            List<string> theOffices = new List<string>
            {
                "Jönköping",
                "Värnamo",
                "Göteborg"
            };
            ViewBag.offices = theOffices;

            // This list is used as the dropdown option in the "Växellådstyp" input.
            List<string> gearType = new List<string>
            {
                "Automatisk",
                "Manuell"
            };
            ViewBag.gears = gearType;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewVehicle(Vehicle addNewVehicle, List<IFormFile> Picture)
        {
            if (ModelState.IsValid && addNewVehicle != null)
            {
                // To add the uploaded images into the Picture property of Vehicle.
                foreach (var item in Picture)
                {
                    if (item.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await item.CopyToAsync(stream);
                            addNewVehicle.Picture = stream.ToArray();
                        }
                    }
                }
                addNewVehicle.AddedDate = DateTime.Now;
                vehicleRepo.AddNewVehicle(addNewVehicle);
                return View("TestVehicleAdded");
            }

            return View();
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
            // To get the list of all Vehicles from the repo.
            var getVehicles = vehicleRepo.GetAllVehicles();
            

            return View(getVehicles);
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
