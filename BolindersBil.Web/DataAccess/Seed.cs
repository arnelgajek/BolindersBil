using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolindersBil.Models;

namespace BolindersBil.Web.DataAccess
{
    public static class Seed
    {
        internal static void FillIfEmpty(ApplicationDbContext ctx)
        {
            //if (!ctx.Categories.Any())
            //{
            //    ctx.Categories.Add(new Category { Name = "Cirkelsågar" });
            //    ctx.Categories.Add(new Category { Name = "Skruvdragare" });
            //    ctx.SaveChanges();
            //}

            //if (!ctx.Products.Any())
            //{
            //    var products = new List<Product>
            //    {
            //        // Cirkelsågar
            //        new Product { Name = "AFG KS55-2", Price = 1295, Description = "Lorem ipsum dolor sit amet.", CategoryId = 1 },
            //        new Product { Name = "Bosch GK5 165", Price = 1275, Description = "Lorem ipsum dolor sit amet.", CategoryId = 1 },
            //        new Product { Name = "DeWalt DWE550", Price = 1190, Description = "Lorem ipsum dolor sit amet.", CategoryId = 1 },
            //        new Product { Name = "Festool HK 55 EBQ-Plus", Price = 3695, Description = "Lorem ipsum dolor sit amet.", CategoryId = 1 },
            //        // Skruvdragare
            //        new Product { Name = "Bosch GDR 10,8V", Price = 2365, Description = "Lorem ipsum dolor sit amet.", CategoryId = 2 },
            //        new Product { Name = "Bosch PSR 10,8 LI-2", Price = 1349, Description = "Lorem ipsum dolor sit amet.", CategoryId = 2 },
            //        new Product { Name = "DeWalt DCF835M2", Price = 3599, Description = "Lorem ipsum dolor sit amet.", CategoryId = 2 },
            //        new Product { Name = "FEIN ASCM 12 C", Price = 3270, Description = "Lorem ipsum dolor sit amet.", CategoryId = 2 }
            //    };
            //    ctx.Products.AddRange(products);
            //    ctx.SaveChanges();
            //}

            var Vehicles = new List<Vehicle>
            {
                new Vehicle
                {
                    RegNr = "ABC - 123",
                    Brand = "Volvo",
                    Model = "V60",
                    Year = 1975,
                    Kilometer = 12,
                    Price = 32999.99,
                    Body = "Halvkombi",
                    Color = "Gul",
                    Gearbox = "Manuell",
                    Fuel = "Bensin",
                    Horsepower = 110,
                    Used = false,
                    Offices = Värnamo,
                    Picture = "Comming soon",
                    Leasable = true,
                    VehicleAttribute = "Speglar|Takfönster|kasettspelare"
                },
                new Vehicle
                {
                    RegNr = "BCD - 234",
                    Brand = "SAAB",
                    Model = "93",
                    Year = 1985,
                    Kilometer = 123,
                    Price = 23999.99,
                    Body = "kombi",
                    Color = "Röd",
                    Gearbox = "Manuell",
                    Fuel = "Bensin",
                    Horsepower = 170,
                    Used = true,
                    Offices = Jönköping,
                    Picture = "Comming soon",
                    Leasable = true,
                    VehicleAttribute = "Speglar|Cdspelare"
                },
                new Vehicle
                {
                    RegNr = "LOL - 404",
                    Brand = "BMW",
                    Model = "x2000",
                    Year = 2009,
                    Kilometer = 1200,
                    Price = 320000.99,
                    Body = "Cab",
                    Color = "Blå",
                    Gearbox = "Automat",
                    Fuel = "Miljöbränsle/hybrid",
                    Horsepower = 320,
                    Used = true,
                    Offices = Göteborg,
                    Picture = "Comming soon",
                    Leasable = false,
                    VehicleAttribute = "Speglar|Takfönster|AUX-uttag|Bluetooth"
                },
                new Vehicle
                {
                    RegNr = "ABC - 123",
                    Brand = "Renault",
                    Model = "55",
                    Year = 2005,
                    Kilometer = 2300,
                    Price = 67000,
                    Body = "Yrkesfordon",
                    Color = "Vit",
                    Gearbox = "Manuell",
                    Fuel = "Disel",
                    Horsepower = 90,
                    Used = true,
                    Offices = Värnamo,
                    Picture = "Comming soon",
                    Leasable = true,
                    VehicleAttribute = "Speglar|Kasettspelare|Cdspelare"
                }
            };

        }
    }
}
