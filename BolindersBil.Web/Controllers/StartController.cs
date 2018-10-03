using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolindersBil.Models;
using BolindersBil.Repositories;
using BolindersBil.Web.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BolindersBil.Web.Controllers
{
    public class StartController : Controller
    {
        // To be able to use the services.AddTransient from startup.cs.
        // Private property and private contructor. 
        private IVehicleRepository vehicleRepo;
        public StartController(IVehicleRepository vehicleRepository)
        {
            vehicleRepo = vehicleRepository;
        }

        public IActionResult Cookies()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            VehiclesSearchViewModel model = new VehiclesSearchViewModel();

            return View(model);
        }

        [HttpGet]
        public IActionResult Vehicles()
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

            // This list is used as the dropdown option in the "Växellådstyp" input.
            List<string> gearType = new List<string>
            {
                "Automatisk",
                "Manuell"
            };
            ViewBag.gears = gearType;

            return RedirectToAction("Index", "Vehicle");
        }

    }
}       


       
    


