using System;
using System.Collections.Generic;
using System.Text;

namespace BolindersBil.Models
{
     public class VehiclesSearchViewModel
     {
        public string RegNr { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Kilometer { get; set; }
        public double Price { get; set; }
        public string Body { get; set; }
        public string Color { get; set; }
        public string Gearbox { get; set; }
        public string Fuel { get; set; }
        public int Horsepower { get; set; }
        public bool Used { get; set; }
        public virtual Office OfficeId { get; set; }       
        public bool Leasable { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string VehicleAttribute { get; set; }
        public IEnumerable<Vehicle> Vehicles { get; set; }
        public IEnumerable<string> Brands { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public string Path { get; set; }
        public int VehicleId { get; set; }
        public PagingInfo Pager { get; set; }
        public int PriceFrom { get; set; }
        public int PriceTo { get; set; }
        public int MillageFrom { get; set; }
        public int MillageTo { get; set; }
        public string YearFrom { get; set; }
        public string YearTo { get; set; }
     }
}
