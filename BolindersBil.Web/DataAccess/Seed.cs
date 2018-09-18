using BolindersBil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.Web.DataAccess
{
    public static class Seed
    {
        internal static void FillIfEmpty(ApplicationDbContext ctx)
        {
            if (!ctx.Admins.Any())
            {
                ctx.Admins.Add(new Admin { FirstName = "Mallory", LastName = "Fraiche" });
                ctx.Admins.Add(new Admin { FirstName = "Timmie", LastName = "Bark" });
                ctx.Admins.Add(new Admin { FirstName = "Arnel", LastName = "Gajek" });
                ctx.SaveChanges();
            }

            if (!ctx.Offices.Any())
            {
                ctx.Offices.Add(new Office { Name = "Bolinders Bil Värnamo", Address = "Bultgatan 2", ZipCode = 12345, City = "Värnamo", PhoneNumber = 0370123456, Email = "varnamo@bolindersbil.se" });
                ctx.Offices.Add(new Office { Name = "Bolinders Bil Jönköping", Address = "Lovsjövägen 33", ZipCode = 67890, City = "Jönköping", PhoneNumber = 036123456, Email = "jonkoping@bolindersbil.se" });
                ctx.Offices.Add(new Office { Name = "Bolinders Bil Göteborg", Address = "Industrivägen 1", ZipCode = 12378, City = "Göteborg", PhoneNumber = 031123456, Email = "goteborg@bolindersbil.se" });
                ctx.SaveChanges();

            }
        }
    }
}
