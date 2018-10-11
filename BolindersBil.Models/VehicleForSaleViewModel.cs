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
        public Office Office { get; set; }
        public SendMail SendMail { get; set; }
        public int vehicleId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string ModelDescription { get; set; }
        public string Year { get; set; }
        public string CurrentOffice { get; set; }
        public List<Vehicle> ListOfVehicles { get; set; }
    }
}
