using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BolindersBil.Models
{
    public class Office
    {
        public int Id { get; set; }
        public string OfficeCode { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }

        // Does this need to be virtual?
        public List<Vehicle> Vehicles { get; set; }
    }
}
