using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YotorAspNetCoreApiResources.Models
{
    public class Booking
    {
        public int booking_id { get; set; }
        public int? restriction_id { get; set; }
        public int user_id { get; set; }
        public int car_id { get; set; }
        public int? feedback_id { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public bool status { get; set; }
        public int full_price { get; set; }
        public string start_address { get; set; }
        public string end_address { get; set; }

    }
}
