using MTSaglikAuthenticator.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.Entities.Models
{
    public class UserActivity : BaseModel
    {
        public string ActivityCode { get; set; }
        public decimal Longtitude { get; set; }
        public decimal Lottitude { get; set; }
        public int ResponseCode { get; set; }
       
        
        public string UserActivityData { get; set; }

      
        public Guid? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public User Customer { get; set; }

        
        public Guid AccessedFileId { get; set; }
        [ForeignKey("AccessedFileId")]
        public File AccessedFile { get; set; }
    }
}
