using BolindersBil.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BolindersBil.Web.Controllers
{
    public class MailController : Controller
    {
        // Send/Share the url from the vehicle for sale:
        [HttpPost]
        public IActionResult SendMail()
        {
            return View();
        }
    }
}
