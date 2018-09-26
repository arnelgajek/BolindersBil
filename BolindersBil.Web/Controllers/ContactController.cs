using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace BolindersBil.Web.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {

            SendTheEmail();

            return View();
        }

        //Detta ska ersättas senare (ska inte vara i controllern)
        private static void SendTheEmail()
        {
            var smtpClient = new SmtpClient
            {
                Host = "localhost",
                Port = 25,
                UseDefaultCredentials = true
            };

            var message = new MailMessage("from@wu18.com", "to@wu18.com")
            {
                Subject = "This is the subject of the email",
                Body = "<p>This is the body of the email</p><a> href='#'>The link</a>",
                IsBodyHtml = true
            };
            
            {
                smtpClient.Send(message);
            }
        }

    }
}
