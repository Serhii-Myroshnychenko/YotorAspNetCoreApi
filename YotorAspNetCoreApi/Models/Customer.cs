using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YotorAspNetCoreApi.Models
{
    public class Customer
    {
        public int user_id { get; set; }
        public string full_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string password { get; set; }
        public byte[]? photo { get; set; }
        public byte[]? passport { get; set; }
        public byte[]? drivers_license { get; set; }
        public bool is_admin { get; set; }

    }
}
