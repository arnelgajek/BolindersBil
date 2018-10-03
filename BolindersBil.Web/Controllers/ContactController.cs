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
            var offices = officeRepo.GetAllOffices();
            var vm = new ContactFormViewModel();
            vm.Offices = offices;
            return View(vm);
        }
        
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
            
            return RedirectToAction("Index");
            
        }
    }
}
