using BolindersBil.Models;
using BolindersBil.Web.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
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

        // So we can DeleteVEhicles from our DB:
        public Vehicle DeleteVehicle(int vehicleId)
        {
            var ctxVehicle = ctx.Vehicles.FirstOrDefault(x => x.Id.Equals(vehicleId));
            if (ctxVehicle != null)
            {
                ctx.Vehicles.Remove(ctxVehicle);
                ctx.SaveChanges();
            }
            return ctxVehicle;
        }

        // List all the Vehicles from DB.
        public IEnumerable<Vehicle> GetAllVehicles()
        {
            return Vehicles;
        }
        
    }
}
