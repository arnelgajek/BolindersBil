using BolindersBil.Models;
using BolindersBil.Web.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


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
        public IEnumerable<Image> Images => ctx.Images;


        public void AddImage(Image image)
        {
            if (image.Id == 0)
            {
                ctx.Images.Add(image);
            }
            ctx.SaveChanges();
        }
        
        // So we can AddNewVehicles to our DB.
        public void AddNewVehicle(Vehicle vehicle)
        {
            if (vehicle.Id == 0)
            {
                ctx.Vehicles.Add(vehicle);
            }
            ctx.SaveChanges();
        }

        // So we can DeleteVehicles from our DB:
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

        public IEnumerable<Vehicle> Search(string searchString, bool Used)
        {
            var vehicles = (from c in ctx.Vehicles
                            where
                                     c.RegNr.Contains(searchString) ||
                                     c.Brand.Contains(searchString) ||
                                     c.Model.Contains(searchString) ||
                                     c.ModelDescription.Contains(searchString) ||
                                     c.Year.Equals(searchString) ||
                                     c.Kilometer.Equals(searchString) &&
                                     c.Used.Equals(Used)

                            select c
                                );

            return vehicles;

        }

        // Update(Edit) the vehicle. 
        public void UpdateVehicle(EditVehicleViewModel v)
        {
            var ctxVehicle = ctx.Vehicles.FirstOrDefault(x => x.Id.Equals(v.Id));
            if (ctxVehicle != null)
            {
                ctxVehicle.RegNr = v.RegNr;
                ctxVehicle.Brand = v.Brand;
                ctxVehicle.Model = v.Model;
                ctxVehicle.ModelDescription = v.ModelDescription;
                ctxVehicle.Year = v.Year;
                ctxVehicle.Kilometer = v.Kilometer;
                ctxVehicle.Price = v.Price;
                ctxVehicle.Body = v.Body;
                ctxVehicle.Color = v.Color;
                ctxVehicle.Gearbox = v.Gearbox;
                ctxVehicle.Fuel = v.Fuel;
                ctxVehicle.Horsepower = v.Horsepower;
                ctxVehicle.Used = v.Used;
                ctxVehicle.OfficeId = v.OfficeId;
                ctxVehicle.Office = v.Office;
                ctxVehicle.Leasable = v.Leasable;
                ctxVehicle.UpdatedDate = v.UpdatedDate;
                ctxVehicle.VehicleAttribute = v.VehicleAttribute;
            }
            ctx.SaveChanges();

        }
    }
}

