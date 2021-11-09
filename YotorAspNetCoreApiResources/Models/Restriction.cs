using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YotorAspNetCoreApiResources.Models
{
    public class Restriction
    {
        public int restriction_id { get; set; }
        public int landlord_id { get; set; }
        public string description { get; set; }

    }
}
