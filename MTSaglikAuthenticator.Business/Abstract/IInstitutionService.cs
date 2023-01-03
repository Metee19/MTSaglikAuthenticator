using MTSaglikAuthenticator.Core.Utilities;
using MTSaglikAuthenticator.Entities.Models;
using MTSaglikAuthenticator.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.Business.Abstract
{
    public interface IInstitutionService
    {
        Result<List<InstitutionViewModel>> GetAll();

        Result<List<InstitutionViewModel>> GetCustomerType();
        Result<List<TitleViewModel>> GetTitles();

        Result<InstitutionViewModel> CreateInstitution(InstitutionViewModel model, string loginId);

        Result<InstitutionViewModel> GetInstitutionById(Guid id);

        Result<InstitutionViewModel> EditInstitution(InstitutionViewModel model, string loginId);

    }
}
