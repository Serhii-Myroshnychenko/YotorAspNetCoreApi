using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YotorAspNetCoreApiResources.Models
{
    public class RestrictionConstructor
    {
        [Required(ErrorMessage = "Введите название машини")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите описание машини")]
        public string Description { get; set; }
    }
}
