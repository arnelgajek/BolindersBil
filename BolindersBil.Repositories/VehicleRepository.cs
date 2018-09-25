using BolindersBil.Models;
using BolindersBil.Web.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace BolindersBil.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private ApplicationDbContext ctx;
        public VehicleRepository(ApplicationDbContext context)
        {
            ctx = context;
        }
        public IEnumerable<Vehicle> Vehicles => ctx.Vehicles;

        // So we can AddNewVehicles to our DB.
        public void AddNewVehicle(Vehicle vehicle)
        {
            //ctx.AttachRange(vehicle);
            if (vehicle.Id == 0)
            {
                ctx.Vehicles.Add(vehicle);
            }
            ctx.SaveChanges();
        }
<<<<<<< HEAD
        
=======


        // TODO: LIST ALL VEHICLES
        public IEnumerable<Vehicle> GetAllVehicles()
        {
            return Vehicles;
        }

>>>>>>> 3f6c20b41eb390dd77d61f40f71e7e929e25724b
    }
}
