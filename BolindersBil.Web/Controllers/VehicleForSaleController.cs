using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BolindersBil.Web.Controllers
{
    public class VehicleForSaleController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
      
    }
}