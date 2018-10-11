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

        // So we can BulkDeleteVehicles from our DB:
        public Vehicle BulkDeleteVehicle(int vehicleId)
        {
            var ctxVehicle = ctx.Vehicles.FirstOrDefault(x => x.Id.Equals(vehicleId));
            if (ctxVehicle != null)
            {
                ctx.Vehicles.Remove(ctxVehicle);
                ctx.SaveChanges();
            }
            return ctxVehicle;
        }


        // To reload the page so see the vehicle add with specifik id:
        public Vehicle Vehicle(int vehicleId)
        {
            var ctxVehicle = ctx.Vehicles.FirstOrDefault(x => x.Id.Equals(vehicleId));
            
            return ctxVehicle;
        }

        // List all the Vehicles from DB.
        public IEnumerable<Vehicle> GetAllVehicles()
        {


            return Vehicles;
        }

        // List all the Images from DB
        public IEnumerable<Image> GetAllImages()
        {
            return Images;
        }
        //public IEnumerable<Vehicle> Filter(string searchString)
        //{
        //    IEnumerable<Vehicle> vehicles;

            
        //}
      

        public IEnumerable<Vehicle> Search(string searchString, bool? used)
        {
            IEnumerable<Vehicle> vehicles;
            if (string.IsNullOrEmpty(searchString))
            {

                vehicles = ctx.Vehicles.Where(x => x.Used == used.Value);
            }
            else
            {
                vehicles = ctx.Vehicles.Where(x => x.Brand.Contains(searchString) ||
                                                   x.Body.Contains(searchString) ||
                                                   x.Brand.Contains(searchString) ||
                                                   x.Color.Contains(searchString) ||
                                                   x.Fuel.Contains(searchString) ||
                                                   x.Gearbox.Contains(searchString) ||
                                                   x.Office.Contains(searchString) ||
                                                   x.VehicleAttribute.Contains(searchString));
                if (used.HasValue)
                {
                    vehicles = vehicles.Where(x => x.Used == used.Value);
                }

            }

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
                ctxVehicle.Images = v.Images;
            }
            ctx.SaveChanges();
        }
    }
}

