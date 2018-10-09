using System;
using System.Collections.Generic;
using System.Text;

namespace BolindersBil.Models
{
    public class VehicleForSaleViewModel
    {
        public string Email { get; set; }
        public string Link { get; set; }
        public Vehicle Vehicle { get; set; }
        public List<Image> Images { get; set; }
        public Office Offices { get; set; }
        public SendMail SendMail { get; set; }
    }
}
