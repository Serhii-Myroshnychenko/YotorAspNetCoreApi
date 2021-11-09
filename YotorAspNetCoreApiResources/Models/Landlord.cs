using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YotorAspNetCoreApiResources.Models
{
    public class Landlord
    {
        public int landlord_id { get; set; }
        public int user_id { get; set; }
        public int organization_id { get; set; }
        public string name { get; set; }

    }
}
