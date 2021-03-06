﻿using BolindersBil.Models;
using BolindersBil.Web.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BolindersBil.Repositories
{
    public class OfficeRepository : IOfficeRepository
    {
        private ApplicationDbContext ctx;
        public OfficeRepository(ApplicationDbContext context)
        {
            ctx = context;
        }
        // The Offices are the Offices (ctx.Offices) from our database
        public IEnumerable<Office> Offices => ctx.Offices;
        public IEnumerable<Office> GetAllOffices()
        {
            return Offices;
        }
    }
}
