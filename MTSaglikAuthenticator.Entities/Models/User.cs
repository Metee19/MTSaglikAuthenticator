using MTSaglikAuthenticator.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.Entities.Models
{
    public class User : BaseModel
    {
        public string FullName { get; set; }
        public Guid Type { get; set; }       
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CountryCode { get; set; }
        public decimal Longtitude { get; set; }
        public decimal Lottitude { get; set; }      
        public Guid InstitutionId { get; set; }

        [ForeignKey("InstitutionId")]
        public Institution Institution { get; set; }

        public List<UserActivity> UserActivities { get; set; }

    }
}
