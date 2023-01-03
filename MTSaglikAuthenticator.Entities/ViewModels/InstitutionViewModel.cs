using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.Entities.ViewModels
{
    public class InstitutionViewModel : BaseViewModel
    {
        public string CompanyName { get; set; }
        public Guid Type { get; set; }
        public TitleViewModel Title { get; set; }
    }
}
