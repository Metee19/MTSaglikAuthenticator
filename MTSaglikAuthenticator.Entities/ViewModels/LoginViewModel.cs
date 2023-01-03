using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.Entities.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Telefon Numarası:")]
        [Required(ErrorMessage = "Telefon numarası alanı gereklidir.")]
        [Phone]
        public string Phone { get; set; }

        public bool RememberMe { get; set; }
    }
}
