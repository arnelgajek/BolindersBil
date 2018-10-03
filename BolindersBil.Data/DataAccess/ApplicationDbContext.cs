using BolindersBil.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.Web.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser> 
    {
      

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Office> Offices { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Image> Images { get; set; }

    }
}
