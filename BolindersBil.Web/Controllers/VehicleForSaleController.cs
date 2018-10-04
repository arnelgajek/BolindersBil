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