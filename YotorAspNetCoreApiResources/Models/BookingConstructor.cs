using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YotorAspNetCoreApiResources.Models
{
    public class BookingConstructor
    {
        [Required(ErrorMessage = "Введите начальную дату")]
        public DateTime Start_date { get; set; }
        [Required(ErrorMessage = "Введите кончную дату")]
        public DateTime End_date { get; set; }
        [Required(ErrorMessage = "Введите начальный адресс")]

        public string Start_address { get; set; }
        [Required(ErrorMessage = "Введите конечный адресс")]
        public string End_address { get; set; }
        [Required(ErrorMessage = "Введите название машини")]
        public string Car_name { get; set; }
        [Required(ErrorMessage = "Введите цену")]
        public int Full_price { get; set; }

    }
}
