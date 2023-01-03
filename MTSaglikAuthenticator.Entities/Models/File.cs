using MTSaglikAuthenticator.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.Entities.Models
{
    public class File : BaseModel
    {
        public string FileHash { get; set; }
        public string FileName { get; set; }

        public Guid CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public  User Customer { get; set; }

    }
}
