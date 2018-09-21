using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View("Login");
        }
        public IActionResult Login()
        {
            return View();
        }



        [HttpGet]
        public IActionResult AddNewVehicle()
        {
            // Creates a List of all the years from one year in the future and back.
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

            // 
            List<string> bodyType = new List<string>();
            bodyType.Add("Småbil");
            bodyType.Add("Sedan");
            bodyType.Add("Halvkombi");
            bodyType.Add("Kombi");
            bodyType.Add("SUV");
            bodyType.Add("Coupé");
            bodyType.Add("Cab");
            bodyType.Add("Familjebuss");
            bodyType.Add("Yrkesfordon");
            ViewBag.bodyTypes = bodyType;


            return View();
        }

        [HttpPost]
        public IActionResult AddNewVehicle(int addNewVehicleFormId)
        {
            return View();
        }

    }
}
