using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YotorContext.Models
{
    public class Landlord
    {
        public int Landlord_id { get; set; }
        public int User_id { get; set; }
        public int Organization_id { get; set; }
        public string Name { get; set; }

    }
}
