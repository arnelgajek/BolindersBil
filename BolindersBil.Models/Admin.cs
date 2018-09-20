using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BolindersBil.Models
{
    public class Admin : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
