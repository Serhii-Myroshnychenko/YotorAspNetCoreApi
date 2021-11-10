using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YotorAspNetCoreApiResources.Models
{
    public class Booking
    {
        public int Booking_id { get; set; }
        public int? Restriction_id { get; set; }
        public int User_id { get; set; }
        public int Car_id { get; set; }
        public int? Feedback_id { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public bool Status { get; set; }
        public int Full_price { get; set; }
        public string Start_address { get; set; }
        public string End_address { get; set; }

    }
}
