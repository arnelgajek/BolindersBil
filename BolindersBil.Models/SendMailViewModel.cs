using System;
using System.Collections.Generic;
using System.Text;

namespace BolindersBil.Models
{
    public class SendMailViewModel
    {
        public string Email { get; set; }
        public string Office { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Link { get; set; }
        public Office Offices { get; set; }
    }
}
