using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YotorAspNetCoreApiResources.Models
{
    public class Restriction
    {
        public int Restriction_id { get; set; }
        public int Landlord_id { get; set; }
        public string Description { get; set; }

    }
}
