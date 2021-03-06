using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace YotorAspNetCoreApi.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Введите почту")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }
    }
}
