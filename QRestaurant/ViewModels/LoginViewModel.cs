using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QRestaurantMain.ViewModels
{
    public class LoginViewModel
    {
        [Key]
        [Required(ErrorMessage = "Este campo é obrigatório!")]
        public string email { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório!")]
        public string password { get; set; }

    }
}
