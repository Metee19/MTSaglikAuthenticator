using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.Entities.ViewModels
{
    public class UserViewModel : BaseViewModel
    {

        public string FullName { get; set; }
        public Guid Type { get; set; }
        public InstitutionViewModel Institution { get; set; }
        public Guid InstitutionId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CountryCode { get; set; }
        public decimal Longtitude { get; set; }
        public decimal Lottitude { get; set; }

    }
}
