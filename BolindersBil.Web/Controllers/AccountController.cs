﻿using BolindersBil.Models;
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
            vehicleRepo = vehicleRepository;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            // Checks if the user is authenticated/signed in and redirects him/her to Admin: 
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Admin");
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
                        return RedirectToAction("Admin");
                    }
                }
            }
            return View("Index", vm);
        }


        [Authorize]
        public IActionResult Admin()
        {
            // To get the list of all Vehicles from the repo.
            var getVehicles = vehicleRepo.GetAllVehicles();

            return View(getVehicles);
        }

        // Sends the user back to the login page:
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }

        // ****TODO: maybe move all the vehicle repo DI and CRUD logic in a Vehicle controller instead.
        [HttpGet]
        public IActionResult AddNewVehicle()
        {
            // This list is used as the dropdown option in the "Årsmodell" input.
            List<object> years = new List<object>();
            var currentYear = DateTime.Now.Year;
            var theFuture = currentYear + 1;
            years.Add(theFuture);
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

               

                //var jkpg = addNewVehicle.OfficeId.OfficeCode;
                //if (addNewVehicle.Office == "Jönköping")
                //{
                //    jkpg = "BB1";
                //    addNewVehicle.OfficeId.OfficeCode= jkpg;
                //}

                vehicleRepo.AddNewVehicle(addNewVehicle);
                return View("TestVehicleAdded");
            }

            return View();
        }

        [HttpGet]
        public IActionResult EditVehicle(int vehicleId)
        {
            var vehicle = vehicleRepo.Vehicles.FirstOrDefault(x => x.Id.Equals(vehicleId));

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

            List<string> gearType = new List<string>
            {
                "Automatisk",
                "Manuell"
            };

            List<string> fuelType = new List<string>
            {
                "Bensin",
                "Diesel",
                "El",
                "Miljöbränsle/Hybrid"
            };

            List<string> theOffices = new List<string>
            {
                "Jönköping",
                "Värnamo",
                "Göteborg"
            };

            var vm = new EditVehicleViewModel()
            {
                Id = vehicle.Id,
                RegNr = vehicle.RegNr,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                ModelDescription = vehicle.ModelDescription,
                Year = vehicle.Year,
                Kilometer = vehicle.Kilometer,
                Price = vehicle.Price,
                Body = vehicle.Body,
                Color = vehicle.Color,
                Gearbox = vehicle.Gearbox,
                Fuel = vehicle.Fuel,
                Horsepower = vehicle.Horsepower,
                Used = vehicle.Used,
                Office = vehicle.Office,
                OfficeId = vehicle.OfficeId,
                Picture = vehicle.Picture,
                Leasable = vehicle.Leasable,
                UpdatedDate = vehicle.UpdatedDate,
                VehicleAttribute = vehicle.VehicleAttribute,
                BodyTypes = bodyType,
                GearTypes = gearType,
                FuelTypes = fuelType,
                Offices = theOffices
            };
            
            return View(vm);
        }

        [HttpPost]
        public IActionResult EditVehicle(EditVehicleViewModel editVehicleViewModel)
        {
            if (ModelState.IsValid)
            {
                editVehicleViewModel.UpdatedDate = DateTime.Now;
                vehicleRepo.UpdateVehicle(editVehicleViewModel);
                return View("TestVehicleAdded");
            }
            else
            {
                // TODO: error message here
                return View(editVehicleViewModel);
            }
        }

        [HttpPost]
        public IActionResult DeleteVehicle(int vehicleId)
        {
            var deleted = vehicleRepo.DeleteVehicle(vehicleId);
            if (deleted != null)
            {
                // Vehicle was found and not deleted...
            }
            else
            {
                // Vehicle was not found - show error
            }
            return RedirectToAction(nameof(Admin));
        }

        [HttpPost]
        public IActionResult BulkDeleteVehicle(string vehicleId)
        {
            // Creates an array with all the Ids checked to make an BulkDelete:
            int[] bulkDelete = Array.ConvertAll(vehicleId.Split(','), int.Parse);

            // Loops through all the Ids from the array above:
            foreach (var vehicle in bulkDelete)
            {
                // Deletes the vehicles with the specific Ids chosen:
                DeleteVehicle(vehicle);
            }
            // Redirects the user to the account/admin:
            return RedirectToAction(nameof(Admin));
        }
    }
}
