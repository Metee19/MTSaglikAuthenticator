using MTSaglikAuthenticator.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.Entities.Models
{
    public class Institution : BaseModel
    {
        public string CompanyName { get; set; }
        public Guid Type { get; set; }

        [ForeignKey("Type")]
        public Title Title { get; set; }

    }
}
