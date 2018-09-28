using BolindersBil.Models;
using BolindersBil.Web.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BolindersBil.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        public ApplicationDbContext ctx;
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


        // List all the Vehicles from DB.
        public IEnumerable<Vehicle> GetAllVehicles()
        {
            return Vehicles;
        }

        public IEnumerable<Vehicle> Search(string searchString)
        {

            var vehicles = (from c in ctx.Vehicles
                            where
                                     c.RegNr.Contains(searchString) ||
                                     c.Brand.Contains(searchString) ||
                                     c.Model.Contains(searchString) ||
                                     c.ModelDescription.Contains(searchString) ||
                                     c.Year.Equals(searchString) ||
                                     c.Kilometer.Equals(searchString)

                                select c
                                );
                                                                                                                          
                return vehicles;
            
        }
    }
}
