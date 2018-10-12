using System;
using System.Collections.Generic;
using System.Text;

namespace BolindersBil.Models
{
    public class VehiclePagerViewModel
    {
        public IEnumerable<Vehicle> Vehicles { get; set; }
        public PagingInfo Pager { get; set; }
    }
}
