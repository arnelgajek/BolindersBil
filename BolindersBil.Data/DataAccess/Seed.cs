using BolindersBil.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.Web.DataAccess
{
    public static class Seed
    {
        public static void FillIfEmpty(ApplicationDbContext ctx)
        {
            //if (!ctx.Admins.Any())
            //{
            //    //ctx.Admins.Add(new Admin { FirstName = "Mattias", LastName = "Jarl" });
            //    ctx.Admins.Add(new Admin { FirstName = "Mallory", LastName = "Fraiche" });
            //    ctx.Admins.Add(new Admin { FirstName = "Timmie", LastName = "Bark" });
            //    ctx.Admins.Add(new Admin { FirstName = "Arnel", LastName = "Gajek" });
            //    ctx.SaveChanges();
            //}
            if (!ctx.Offices.Any())
            {
                ctx.Offices.Add(new Office { OfficeCode = "BB2", Name = "Bolinders Bil Värnamo", Address = "Bultgatan 2", ZipCode = 12345, City = "Värnamo", PhoneNumber = 0370123456, Email = "varnamo@bolindersbil.se" });
                ctx.Offices.Add(new Office { OfficeCode = "BB1", Name = "Bolinders Bil Jönköping", Address = "Lovsjövägen 33", ZipCode = 67890, City = "Jönköping", PhoneNumber = 036123456, Email = "jonkoping@bolindersbil.se" });
                ctx.Offices.Add(new Office { OfficeCode = "BB3", Name = "Bolinders Bil Göteborg", Address = "Industrivägen 1", ZipCode = 12378, City = "Göteborg", PhoneNumber = 031123456, Email = "goteborg@bolindersbil.se" });
                ctx.SaveChanges();
            }

            if (!ctx.Vehicles.Any())
            {
                var vehicles = new List<Vehicle>
                {
                new Vehicle
                {
                    RegNr = "ABC - 123",
                    Brand = "Volvo",
                    Model = "V60",
                    ModelDescription = "The description.",
                    Year = 1975,
                    Kilometer = 12,
                    Price = 32999.99,
                    Body = "Halvkombi",
                    Color = "Gul",
                    Gearbox = "Manuell",
                    Fuel = "Bensin",
                    Horsepower = 110,
                    Used = false,
                    Picture = File.ReadAllBytes("BMW.jpg"),
                    Leasable = true,
                    VehicleAttribute = "Speglar|Takfönster|kasettspelare"
                },
                new Vehicle
                {
                    RegNr = "BCD - 234",
                    Brand = "SAAB",
                    Model = "93",
                    ModelDescription = "The description.",
                    Year = 1985,
                    Kilometer = 123,
                    Price = 23999.99,
                    Body = "kombi",
                    Color = "Röd",
                    Gearbox = "Manuell",
                    Fuel = "Bensin",
                    Horsepower = 170,
                    Used = true,
                    Picture = File.ReadAllBytes("BMW.jpg"),
                    Leasable = true,
                    VehicleAttribute = "Speglar|Cdspelare"
                },
                new Vehicle
                {
                    RegNr = "LOL - 404",
                    Brand = "BMW",
                    Model = "x2000",
                    ModelDescription = "The description.",
                    Year = 2009,
                    Kilometer = 1200,
                    Price = 320000.99,
                    Body = "Cab",
                    Color = "Blå",
                    Gearbox = "Automat",
                    Fuel = "Miljöbränsle/hybrid",
                    Horsepower = 320,
                    Used = true,
                    Picture = File.ReadAllBytes("BMW.jpg"),
                    Leasable = false,
                    VehicleAttribute = "Speglar|Takfönster|AUX-uttag|Bluetooth"
                },
                new Vehicle
                {
                    RegNr = "ABC - 123",
                    Brand = "Renault",
                    Model = "55",
                    ModelDescription = "The description.",
                    Year = 2005,
                    Kilometer = 2300,
                    Price = 67000,
                    Body = "Yrkesfordon",
                    Color = "Vit",
                    Gearbox = "Manuell",
                    Fuel = "Disel",
                    Horsepower = 90,
                    Used = true,
                    Picture = File.ReadAllBytes("BMW.jpg"),
                    Leasable = true,
                    VehicleAttribute = "Speglar|Kasettspelare|Cdspelare"
                },
                new Vehicle
                {
                    RegNr = "JLT - 202",
                    Brand = "Ford",
                    Model = "Mondeo",
                    ModelDescription = "The description.",
                    Year = 2002,
                    Kilometer = 13000,
                    Price = 49999.99,
                    Body = "Kombi",
                    Color = "Silver",
                    Gearbox = "Manuell",
                    Fuel = "Bensin",
                    Horsepower = 130,
                    Used = true,
                    Picture = File.ReadAllBytes("BMW.jpg"),
                    Leasable = true,
                    VehicleAttribute = "Speglar|Cdspelare|Elegant"
                },
                new Vehicle
                {
                    RegNr = "YTB - 321",
                    Brand = "Minicooper",
                    Model = "XS",
                    ModelDescription = "The description.",
                    Year = 2018,
                    Kilometer = 0,
                    Price = 12000000.00,
                    Body = "Småbil",
                    Color = "Svart",
                    Gearbox = "Automat",
                    Fuel = "Miljöbränsle/Hybrid",
                    Horsepower = 175,
                    Used = false,
                    Picture = File.ReadAllBytes("BMW.jpg"),
                    Leasable = true,
                    VehicleAttribute = "Speglar|Takfönster|Bluetooth|AUX-uttag|Cab"
                },
                new Vehicle
                {
                    RegNr = "ABC - 123",
                    Brand = "Mercedes",
                    Model = "CLK200",
                    ModelDescription = "The description.",
                    Year = 2018,
                    Kilometer = 5,
                    Price = 549.999,
                    Body = "Sedan",
                    Color = "Svart",
                    Gearbox = "Manuell",
                    Fuel = "Bensin",
                    Horsepower = 190,
                    Used = false,
                    Picture = File.ReadAllBytes("BMW.jpg"),
                    Leasable = false,
                    VehicleAttribute = "Speglar|Takfönster|CD-växlare"
                },
                new Vehicle
                {
                    RegNr = "HYA - 156",
                    Brand = "Opel",
                    Model = "Corsa",
                    ModelDescription = "The description.",
                    Year = 2014,
                    Kilometer = 4000,
                    Price = 126.595,
                    Body = "Sedan",
                    Color = "Grå",
                    Gearbox = "Automat",
                    Fuel = "Diesel",
                    Horsepower = 96,
                    Used = true,
                    Picture = File.ReadAllBytes("BMW.jpg"),
                    Leasable = false,
                    VehicleAttribute = "Speglar|Vinterdäck|El-speglar"
                },
                new Vehicle
                {
                    RegNr = "YUU - 574",
                    Brand = "Volvo",
                    Model = "C30",
                    ModelDescription = "The description.",
                    Year = 2010,
                    Kilometer = 12000,
                    Price = 114.000,
                    Body = "Halvsedan",
                    Color = "Vit",
                    Gearbox = "Manuell",
                    Fuel = "Bensin",
                    Horsepower = 116,
                    Used = true,
                    Picture = File.ReadAllBytes("BMW.jpg"),
                    Leasable = false,
                    VehicleAttribute = "Speglar|Takfönster|Original-fälgar"
                },
                new Vehicle
                {
                    RegNr = "HUA - 123",
                    Brand = "Porsche",
                    Model = "Turbo",
                    ModelDescription = "The description.",
                    Year = 1966,
                    Kilometer = 25000,
                    Price = 379.999,
                    Body = "Coupe",
                    Color = "Röd",
                    Gearbox = "Manuell",
                    Fuel = "Bensin",
                    Horsepower = 250,
                    Used = true,
                    Picture = File.ReadAllBytes("BMW.jpg"),
                    Leasable = false,
                    VehicleAttribute = "Speglar|Takfönster|Original-fälgar"
                },
                new Vehicle
                {
                    RegNr = "HEJ - 178",
                    Brand = "Mercedes",
                    Model = "CLK200",
                    ModelDescription = "The description.",
                    Year = 2018,
                    Kilometer = 5,
                    Price = 549.999,
                    Body = "Sedan",
                    Color = "Svart",
                    Gearbox = "Manuell",
                    Fuel = "Bensin",
                    Horsepower = 190,
                    Used = false,
                    Picture = File.ReadAllBytes("BMW.jpg"),
                    Leasable = false,
                    VehicleAttribute = "Speglar|Takfönster|CD-växlare"
                }
                };

                ctx.Vehicles.AddRange(vehicles);
                ctx.SaveChanges();
            }

        }
    }
}
