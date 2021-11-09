using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YotorAspNetCoreApiResources.Models
{
    public class CarConstructor
    {
        [Required]
        public string Model { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Year { get; set; }
        [Required]
        public string Transmission { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Number { get; set; }
    }
}
