using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.Entities.ViewModels
{
    public class FileViewModel : BaseViewModel
    {
        public string FileHash { get; set; }
        public string FileName { get; set; }
        public Guid CustomerId { get; set; }
    }
}
