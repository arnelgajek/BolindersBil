using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BolindersBil.Models
{
    public class SendMail
    {
        [Required]
        public string Email { get; set; }
    }
}
