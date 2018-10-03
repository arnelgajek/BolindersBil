using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace BolindersBil.Models
{
    public class ContactFormViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNr { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Office { get; set; } 
        public IEnumerable<Office> Offices { get; set; }
    }
}
