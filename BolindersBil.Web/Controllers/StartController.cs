using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolindersBil.Repositories;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Search()
        {
            

            return View();
        }
    }
}


       
    


