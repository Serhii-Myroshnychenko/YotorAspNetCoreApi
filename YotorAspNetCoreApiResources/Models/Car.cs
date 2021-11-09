using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YotorAspNetCoreApiResources.Models
{
    public class Car
    {
        public int car_id { get; set; }
        public int organization_id { get; set; }
        public string model { get; set; }
        public string brand { get; set; }
        public string year { get; set; }
        public string transmission { get; set; }
        public string address { get; set; }
        public bool status { get; set; }
        public string type { get; set; }
        public int price { get; set; }
        public byte[]? photo { get; set; }
        public string? description { get; set; }
        public string number { get; set; }

    }
}
