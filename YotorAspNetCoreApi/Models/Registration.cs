using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace YotorAspNetCoreApi.Models
{
    public class Registration
    {
        [Required]
        public string Full_name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
