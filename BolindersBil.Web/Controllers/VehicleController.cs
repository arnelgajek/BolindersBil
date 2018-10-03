using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolindersBil.Models;
using BolindersBil.Web.DataAccess;
using BolindersBil.Data;
using BolindersBil.Repositories;

namespace BolindersBil.Web.Controllers
{
    public class VehicleController : Controller
    {

        //  Now we can take on numbers and keep track on number page pagineringen.

        private IVehicleRepository repo;
        public int PageLimit = 8;

        public VehicleController(IVehicleRepository vehicleRepository)
        {
            repo = vehicleRepository;
        }

        // Paging
        public IActionResult VehicleList(int page = 1)
        {
            // page = 0 x pagelimit
            var toSkip = (page - 1) * PageLimit;

            // Gets the (pagelimit) amount of vehicles and orders them by their ID
            // This shall be changed to sort by UpdateDate in the future
            var vehicles = repo.Vehicles.OrderBy(x => x.Id).Skip(toSkip).Take(PageLimit);

            // Gets new info for the paging. Page becomes 1. (1 (page) x 8 (pagelimit)). 
            // And creates new pages for the amount over the pagelimit (since page becomes 2 then 3...)
            var paging = new PagingInfo
            { CurrentPage = page,
              ItemsPerPage = PageLimit,
              TotalItems = repo.Vehicles.Count()
            };


            var vm = new VehiclesSearchViewModel
            {
              Vehicles = vehicles,
              Pager = paging
            };

            return View("Index", vm);
        }
         // Index isn't used for anything important atm but is crutial
        public IActionResult Index()
        {
            return RedirectToAction("VehicleList");
        }
        
        // Gets all the vehicles so the admin can see them when logged in:
        [HttpGet]
        public IActionResult Vehicles()
        {
            var vehicles = repo.GetAllVehicles();
            var vm = new VehiclesSearchViewModel();

            vm.Vehicles = vehicles;

            return View("Index", vm);
        }

        [HttpPost]
        public IActionResult Search(string searchString, bool used)
        {
            //should we convert this into a list of vm's?
            var searchResults = repo.Search(searchString, used);
                         
            

            return View("Index", searchResults);



            // Todo If checkbox is new send only new cars in view
            // Todo if checkbox is used send only used cars in view
        }

    }
}
