using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YotorContext.Models
{
    public class Feedback
    {

        public int Feedback_id { get; }
        public int User_id { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public string Text { get; set; }

    }
}
