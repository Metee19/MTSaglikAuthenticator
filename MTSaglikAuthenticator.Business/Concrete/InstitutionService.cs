using AutoMapper;
using MTSaglikAuthenticator.Business.Abstract;
using MTSaglikAuthenticator.Core.Constants;
using MTSaglikAuthenticator.Core.Utilities;
using MTSaglikAuthenticator.DataAccess.Abstract;
using MTSaglikAuthenticator.Entities.Models;
using MTSaglikAuthenticator.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.Business.Concrete
{
    public class InstitutionService : IInstitutionService
    {
        private readonly IInstitution _institution;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public InstitutionService(IInstitution institution, IUnitOfWork uow, IMapper mapper)
        {
            _institution = institution;
            _uow = uow;
            _mapper = mapper;
        }

        public Result<InstitutionViewModel> CreateInstitution(InstitutionViewModel model, string loginId)
        {
            if (model != null)
            {
                try
                {
                    var institution = _mapper.Map<InstitutionViewModel, Institution>(model);
                    institution.Id = Guid.NewGuid();
                    institution.CompanyName = model.CompanyName;
                    institution.Type = model.Type;
                    institution.CreatedBy = new Guid(loginId);
                    institution.CreatedDate = DateTime.Now;
                    institution.ModifiedBy = new Guid(loginId);
                    institution.ModifiedDate = DateTime.Now;
                    institution.Status = true;

                    _uow.institution.Add(institution);
                    _uow.Save();

                    return new Result<InstitutionViewModel>(true, "Institution is created");
                }
                catch (Exception)
                {

                    return new Result<InstitutionViewModel>(false, "Institution creating is failed");
                    throw;
                }

            }

            return new Result<InstitutionViewModel>(false, ResultConstants.ModelisEmpty);
        }

        public Result<InstitutionViewModel> EditInstitution(InstitutionViewModel model, string loginId)
        {
            if (model != null)
            {
                var data = _mapper.Map<InstitutionViewModel, Institution>(model);
                data.ModifiedBy = new Guid(loginId);
                data.ModifiedDate = DateTime.Now;

                _uow.institution.Update(data);
                _uow.Save();

                return new Result<InstitutionViewModel>(true, "Edit is OK");
            }

            return new Result<InstitutionViewModel>(false, "Edit is FAILED");
        }

        public Result<List<InstitutionViewModel>> GetAll()
        {
            var data = _uow.institution.GetAll(i => i.Status == true, includeProperties:"Title").ToList();
            var institution = _mapper.Map<List<Institution>, List<InstitutionViewModel>>(data);
            if (institution != null)
            {
                return new Result<List<InstitutionViewModel>>(true, "Şirket listesi", institution);
            }

            return new Result<List<InstitutionViewModel>>(false, "Herhangi bir şirket bulunamadı", null);
        }

        public Result<List<InstitutionViewModel>> GetCustomerType()
        {
            List<InstitutionViewModel> institutionVM = new List<InstitutionViewModel>();
            var institutions = _uow.institution.GetAll(l=>l.Status == true, includeProperties: "Title").ToList();
            if(institutions!=null)
            {
                var institution = _mapper.Map<List<Institution>, List<InstitutionViewModel>>(institutions);
                foreach (var item in institution)
                {
                    if(item.Title.TitleName == "Customer")
                    {
                        institutionVM.Add(item);
                    }
                }

                return new Result<List<InstitutionViewModel>>(true, "OK", institutionVM);
            }
            return new Result<List<InstitutionViewModel>>(false, "FAILED", null);
        }

        public Result<InstitutionViewModel> GetInstitutionById(Guid id)
        {
            var data = _uow.institution.GetById(id);
            if (data != null)
            {
                var model = _mapper.Map<Institution, InstitutionViewModel>(data);
                return new Result<InstitutionViewModel>(true, "Institution info", model);
            }

            return new Result<InstitutionViewModel>(false, "failed", null);
        }

        public Result<List<TitleViewModel>> GetTitles()
        {
            var data = _uow.title.GetAll(t => t.Status == true).ToList();
            var titles = _mapper.Map<List<Title>, List<TitleViewModel>>(data);
            if (titles != null)
            {
                return new Result<List<TitleViewModel>>(true, "Titles", titles);
            }

            else
                return new Result<List<TitleViewModel>>(false, "Failed", null);
        }
    }
}
