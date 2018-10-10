using BolindersBil.Web.DataAccess;
using BolindersBil.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.Data.DataAccess
{
    public static class LogoSeed
    {
        public static void FillIfEmpty(ApplicationDbContext ctx)
        {
            if (!ctx.Logos.Any())
            {
                ctx.Add(new Logo { ImageUrl = "http://www.carlogos.org/Car-Logos/Nissan-logo.html" });
                ctx.Add(new Logo { ImageUrl = "http://www.carlogos.org/Car-Logos/Porsche-logo.html" });
                ctx.Add(new Logo { ImageUrl = "http://www.carlogos.org/Car-Logos/BMW-logo.html" });
                ctx.Add(new Logo { ImageUrl = "http://www.carlogos.org/Car-Logos/Mercedes-Benz-logo.html" });
                ctx.Add(new Logo { ImageUrl = "http://www.carlogos.org/Car-Logos/Toyota-logo.html" });
                ctx.Add(new Logo { ImageUrl = "http://www.carlogos.org/Car-Logos/Renault-logo.html" });
                ctx.Add(new Logo { ImageUrl = "http://www.carlogos.org/Car-Logos/Volvo-logo.html" });
                ctx.Add(new Logo { ImageUrl = "http://www.carlogos.org/Car-Logos/Mini-logo.html" });

                ctx.SaveChanges();
            }
        }
    }
}
