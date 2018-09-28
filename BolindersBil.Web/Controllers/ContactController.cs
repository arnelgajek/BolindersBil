using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using BolindersBil.Models;

namespace BolindersBil.Web.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public IActionResult SendTheMail()
        //{
        //    return View("Index");
        //}
        
        [HttpPost]
        public IActionResult SendTheMail(ContactFormViewModel contactFormViewModel)
        {
            var smtpClient = new SmtpClient
            {
                Host = "localhost",
                Port = 25,
                UseDefaultCredentials = true
            };

            var vm = contactFormViewModel;
            if(vm.Office == "Jönköping")
            {
                vm.Office = "jonkoping";
            }
            else if(vm.Office == "Värnamo")
            {
                vm.Office = "varnamo";
            }
            else if(vm.Office == "Göteborg")
            {
                vm.Office = "goteborg";
            }
            else
            {
                vm.Office = "kontakt";
            };
            if (vm.PhoneNr == null)
            {
                vm.PhoneNr = $"{vm.Name} har inte angett ett telefonnummer.";
            }
            var message = new MailMessage($"{vm.Email}", $"{vm.Office}@bolindersbil.se")
            {
                Body = $"{vm.Message}<br />" +
                $"Med vänlig hälsning, {vm.Name}<br />" +
                $"<strong>Telefonnummer: {vm.PhoneNr}",
                Subject = vm.Subject,
                IsBodyHtml = true
            };

            {
                smtpClient.Send(message);
            }

            //return View("Index", new ContactFormViewModel());
            return RedirectToAction("Index");
            //var message = new MailMessage("from@wu18.com", "to@wu18.com")
            //{                
            //    IsBodyHtml = true
            //};





        }

    }
}
