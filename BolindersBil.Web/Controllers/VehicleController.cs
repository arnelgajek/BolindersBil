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

        //public IActionResult List(int page = 1)
        //{
        //    var ToSkip = (page - 1) * PageLimit;

        //    var vehicles = repo.Vehicles.OrderBy(x => x.Id).Skip(ToSkip).Take(PageLimit);
        //    var paging = new PagingInfo { CurrentPage = page, CarPerPage = PageLimit, TotalICar = repo.Vehicles.Count() };
        //    var vm = new VehicleListViewModel { Vehicle = vehicles, Pager = paging };
        //    return View(vm);

            
        //}
        
        // Gets all the vehicles so the admin can see them when logged in:
        [HttpGet]
        public IActionResult Vehicles()
        {
            return View("Index");
        }

        [HttpPost]
        public IActionResult Search(string searchString, bool Used)
        {
            //should we convert this into a list of vm's?
            var searchResults = repo.Search(searchString, Used);

            return View("Index", searchResults);

            // Todo If checkbox is new send only new cars in view
            // Todo if checkbox is used send only used cars in view
        }



    }
}
