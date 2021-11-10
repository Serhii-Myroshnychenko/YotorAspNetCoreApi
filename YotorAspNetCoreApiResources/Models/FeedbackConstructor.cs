using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YotorAspNetCoreApiResources.Models
{
    public class FeedbackConstructor
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
