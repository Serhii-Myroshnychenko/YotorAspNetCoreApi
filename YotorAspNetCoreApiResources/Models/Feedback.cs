using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YotorAspNetCoreApiResources.Models
{
    public class Feedback
    {
        public int feedback_id { get; set; }
        public int user_id { get; set; }
        public string name{ get;set;}
        public DateTime? date { get; set; }
        public string text { get; set; }


    }
}
