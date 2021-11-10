using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YotorAspNetCoreApiResources.Models
{
    public class Feedback
    {
        public int Feedback_id { get; set; }
        public int User_id { get; set; }
        public string Name{ get;set;}
        public DateTime? Date { get; set; }
        public string Text { get; set; }


    }
}
