using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YotorAspNetCoreApiResources.Models
{
    public class CarConstructor
    {
        [Required(ErrorMessage = "Введите модель")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Введите бренд")]
        public string Brand { get; set; }
        [Required(ErrorMessage = "Введите Год выпуска")]
        public string Year { get; set; }
        [Required(ErrorMessage = "Введите трансмиссию")]
        public string Transmission { get; set; }
        [Required(ErrorMessage = "Введите адрес")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Введите статус")]
        public bool Status { get; set; }
        [Required(ErrorMessage = "Введите тип")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Введите цену")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Введите фото")]
        public IFormFile Photo { get; set; }
        [Required(ErrorMessage = "Введите описание")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Введите номер")]
        public string Number { get; set; }
    }
}
