using System;
using System.Collections.Generic;
using System.Text;

namespace BolindersBil.Models
{
    public class Image
    {
        public int Id { get; set; }
        public Guid Name { get; set; }
        public string Path { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public int VehicleId { get; set; }

       
    }
}
