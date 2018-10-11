﻿using BolindersBil.Models;
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

        // So we can BulkDeleteVehicles from our DB.
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

        // To reload the page so see the vehicle add with specific id:
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

        // List all the Images from DB.
        public IEnumerable<Image> GetAllImages()
        {
            return Images;
        }

        public IEnumerable<Vehicle> Search(string searchString, bool used)
        {
            IEnumerable<Vehicle> vehicles;
            if (string.IsNullOrEmpty(searchString))
            {

                vehicles = ctx.Vehicles.Where(x => x.Used == used);
            }
            else
            {
                vehicles = ctx.Vehicles.Where(x => x.Brand.Contains(searchString) && x.Used == used ||
                                                   x.Body.Contains(searchString) && x.Used == used ||
                                                   x.Brand.Contains(searchString) && x.Used == used ||
                                                   x.Color.Contains(searchString) && x.Used == used ||
                                                   x.Fuel.Contains(searchString) && x.Used == used ||
                                                   x.Gearbox.Contains(searchString) && x.Used == used ||
                                                   x.Office.Contains(searchString) && x.Used == used ||
                                                   x.VehicleAttribute.Contains(searchString) && x.Used == used);
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

        public IEnumerable<Vehicle> FilterSearch(string year, string fuel, string body, string gearbox, double minPrice, double maxPrice, int maxKm)
        {
            IEnumerable<Vehicle> vehicles;
            
            if (string.IsNullOrEmpty(year) || string.IsNullOrEmpty(fuel) 
                || string.IsNullOrEmpty(body) || string.IsNullOrEmpty(gearbox))
            {
                vehicles = ctx.Vehicles;
            }
            else
            {
                vehicles = ctx.Vehicles.Where(x => x.Year.Contains(year) &&
                                                   x.Fuel.Contains(fuel) &&
                                                   x.Body.Contains(body) &&
                                                   x.Gearbox.Contains(gearbox));

                List<Vehicle> priceFiltered = new List<Vehicle>();
                foreach (var v in vehicles)
                {
                    if (v.Price >= minPrice && v.Price <= maxPrice)
                    {
                        priceFiltered.Add(v);
                    }
                }
                vehicles = priceFiltered;

                List<Vehicle> kmFiltered = new List<Vehicle>();
                foreach (var v in vehicles)
                {
                    if (v.Kilometer <= maxKm)
                    {
                        kmFiltered.Add(v);
                    }
                }
                vehicles = kmFiltered;

            }

            return vehicles;
        }
        
        public IEnumerable<Vehicle> GetNewVehicles()
        {
            IEnumerable<Vehicle> newVehicles;

            newVehicles = ctx.Vehicles.Where(x => x.Used == false);

            return newVehicles;
        }

        public IEnumerable<Vehicle> GetUsedVehicles()
        {
            IEnumerable<Vehicle> usedVehicles;

            usedVehicles = ctx.Vehicles.Where(x => x.Used == true);

            return usedVehicles;
        }

    }
}

