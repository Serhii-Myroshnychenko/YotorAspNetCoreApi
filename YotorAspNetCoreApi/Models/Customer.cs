using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YotorAspNetCoreApi.Models
{
    public class Customer
    {
        public int User_id { get; set; }
        public string Full_name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public byte[]? Photo { get; set; }
        public byte[]? Passport { get; set; }
        public byte[]? Drivers_license { get; set; }
        public bool Is_admin { get; set; }

    }
}
