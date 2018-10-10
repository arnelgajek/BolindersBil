using BolindersBil.Models;
using BolindersBil.Repositories;
using Microsoft.AspNetCore.Http.Extensions;
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
        private IVehicleRepository vehicleRepo;
        // Send/Share the url from the vehicle for sale:
        [HttpGet]
        public IActionResult SendMail(IVehicleRepository vehicleRepository, VehicleForSaleViewModel vehicleForSaleViewModel, int vehicleId)
        {
            // Mail client
            var smtpClient = new SmtpClient
            {
                Host = "localhost",
                Port = 25,
                UseDefaultCredentials = true
            };

            // Can't have swedish letters in the email

            var vm = vehicleForSaleViewModel;
            if (vm.CurrentOffice == "Jönköping")
            {
                vm.CurrentOffice = "jonkoping";
            }
            else if (vm.CurrentOffice == "Värnamo" || vm.CurrentOffice == "Varnamö")
            {
                vm.CurrentOffice = "varnamo";
            }
            else if (vm.CurrentOffice == "Göteborg")
            {
                vm.CurrentOffice = "goteborg";
            }
            

            // The actual message
            var message = new MailMessage($"{vm.CurrentOffice}@bolindersbil.se", $"{ vm.Email }") 
            {
                Body = $"Någon har skickat dig en länk till en {vm.Brand} {vm.Model} {vm.ModelDescription} {vm.Year} <br />" +
                       $"För att se mer <a href='{vm.Link}'>klicka här</a>",
                Subject = "Bolinders bil",
                IsBodyHtml = true
            };

            // Send the message
            smtpClient.Send(message);

            var vehicle = vehicleRepo.Vehicles.FirstOrDefault(x => x.Id.Equals(vehicleId));
            
            // Return to vehicle
            return RedirectToAction();
        }
    }
}
