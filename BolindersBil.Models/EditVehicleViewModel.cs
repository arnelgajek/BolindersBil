using System;
using System.Collections.Generic;
using System.Text;

namespace BolindersBil.Models
{
    public class EditVehicleViewModel
    {
        public int Id { get; set; }
        public string RegNr { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string ModelDescription { get; set; }
        public string Year { get; set; }
        public int Kilometer { get; set; }
        public double Price { get; set; }
        public string Body { get; set; }
        public string Color { get; set; }
        public string Gearbox { get; set; }
        public string Fuel { get; set; }
        public int Horsepower { get; set; }
        public bool Used { get; set; }
        public Office OfficeId { get; set; }
        public string Office { get; set; }
        public bool Leasable { get; set; }
        public List<Image> Images { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string VehicleAttribute { get; set; }
        public List<string> BodyTypes { get; set; }
        public List<string> GearTypes { get; set; }
        public List<string> FuelTypes { get; set; }
        public List<string> Offices { get; set; }
    }
}
