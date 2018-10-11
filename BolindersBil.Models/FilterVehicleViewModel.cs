using System;
using System.Collections.Generic;
using System.Text;

namespace BolindersBil.Models
{
    public class FilterVehicleViewModel
    {
        public int Id { get; set; }
        public string Year { get; set; }
        public string Fuel { get; set; }
        public string Body { get; set; }
        public string Gearbox { get; set; }
        public double Price { get; set; }
        public int Kilometer { get; set; }
        public IEnumerable<Vehicle> Vehicles { get; set; }
    }
}
