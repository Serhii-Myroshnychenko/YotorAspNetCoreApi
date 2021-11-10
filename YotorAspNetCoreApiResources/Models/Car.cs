using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YotorAspNetCoreApiResources.Models
{
    public class Car
    {
        public int Car_id { get; set; }
        public int Organization_id { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string Year { get; set; }
        public string Transmission { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
        public byte[]? Photo { get; set; }
        public string? Description { get; set; }
        public string Number { get; set; }

    }
}
