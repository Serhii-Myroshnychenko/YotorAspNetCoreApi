using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YotorContext.Models
{
    public class Organization
    {
        public int Organization_id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Code { get; set; }
        public string Taxes { get; set; }
        public string Address { get; set; }
        public string Founder { get; set; }
        public string Account { get; set; }

    }
}
