using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.Web.Controllers
{
    public class VehicleController : Controller
    {
        // Gets all the vehicles so the admin can see them when logged in:
        [HttpGet]
        public IActionResult Vehicles()
        {
            return View();
        }
    }
}
