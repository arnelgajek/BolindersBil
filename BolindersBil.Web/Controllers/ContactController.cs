using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using BolindersBil.Models;
using BolindersBil.Repositories;

namespace BolindersBil.Web.Controllers
{
    public class ContactController : Controller
    {

        private IOfficeRepository officeRepo;
        public ContactController(IOfficeRepository officeRepository)
        {
            officeRepo = officeRepository;
        }
        public IActionResult Index()
        {
            // offices with a small "O" fetches all offices from the database
            var offices = officeRepo.GetAllOffices();
            var vm = new ContactFormViewModel();
            // Offices with a big "O" checks the Office objekt from the local solution
            vm.Offices = offices;
            return View(vm);
        }
        
        // This adds information to smtpClient and sends it to Smtp4Dev
        // And it will not work unless you have it downloaded
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
            // This is the information that will be sent
            var message = new MailMessage($"{vm.Email}", $"{vm.Office}@bolindersbil.se")
            {
                Body = $"{vm.Message}<br />" +
                $"Med vänlig hälsning, {vm.Name}<br />" +
                $"<strong>Telefonnummer: {vm.PhoneNr}",
                Subject = vm.Subject,
                IsBodyHtml = true
            };

            // Sending the mail
            {
                smtpClient.Send(message);
            }
            
            return RedirectToAction("Index");
            
        }
    }
}
