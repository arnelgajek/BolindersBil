using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BolindersBil.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Office> Offices { get; set; }
    }
}
