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
        public IActionResult SendTheLinkByMail(SendMailViewModel sendMailViewModel)
        {
            var smtpClient = new SmtpClient
            {
                Host = "localhost",
                Port = 25,
                UseDefaultCredentials = true
            };

            var vm = sendMailViewModel;

            var message = new MailMessage($"{vm.Email}", $"{vm.Office}@bolindersbil.se")
            {
                Body = $"{vm.Message}<br />" +
                $"Med vänlig hälsning, {vm.Office}<br />",
                Subject = vm.Subject,
                IsBodyHtml = true
            };

            // Sending the link to mail:
            {
                smtpClient.Send(message);
            }

            return RedirectToAction("Index");

        }
    }
}
