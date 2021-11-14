using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YotorAspNetCoreApiResources.Models
{
    public class BookingConstructor
    {
        [Required]
        public DateTime Start_date { get; set; }
        [Required]
        public DateTime End_date { get; set; }
        [Required]

        public string Start_address { get; set; }
        [Required]
        public string End_address { get; set; }
        [Required]
        public string Car_name { get; set; }
        [Required]
        public int Full_price { get; set; }

    }
}
