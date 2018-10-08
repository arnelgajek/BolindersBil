using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolindersBil.Models;
using BolindersBil.Repositories;
using BolindersBil.Web.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace BolindersBil.Web.Controllers
{
    public class VehicleForSaleController : Controller
    {
        private IVehicleRepository vehicleRepo;
        public VehicleForSaleController(IVehicleRepository vehicleRepository)
        {
            vehicleRepo = vehicleRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult VehicleForSale()
        {
            var vm = new VehiclesSearchViewModel
            {
                Brand = "hej"
            };

            return View("Index", vm);
        }
    }
}