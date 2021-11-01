using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YotorAspNetCoreApiResources.Models
{
    public class Organization
    {
        public int organization_id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string code { get; set; }
        public string taxes { get; set; }
        public string address { get; set; }
        public string founder { get; set; }
        public string account { get; set; }


    }
}
