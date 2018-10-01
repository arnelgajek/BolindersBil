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

    

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            VehiclesSearchViewModel model = new VehiclesSearchViewModel();

            return View(model);
        }

        public IActionResult Vehicles()
        {
            return RedirectToAction("Vehicles", "Vehicle");
        }

    }
}       


       
    


