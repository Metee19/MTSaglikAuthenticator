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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }
        public Result<UserViewModel> CreateUser(UserViewModel model, string guidId)
        {
            if (model != null)
            {
                try
                {
                    var user = _mapper.Map<UserViewModel, User>(model);
                    user.CreatedDate = DateTime.Now;
                    user.ModifiedDate = DateTime.Now;
                    user.ModifiedBy = new Guid(guidId);
                    user.CreatedBy = new Guid(guidId);
                    user.Status = true;
                    user.Id = Guid.NewGuid();
                    _uow.user.Add(user);
                    _uow.Save();

                    return new Result<UserViewModel>(true, ResultConstants.UserCreated);
                }
                catch (Exception)
                {

                    return new Result<UserViewModel>(false, ResultConstants.UserCreateFail);
                }
            }
            else
                return new Result<UserViewModel>(false, ResultConstants.ModelisEmpty);
        }

        public Result<UserViewModel> DeleteUser(string phoneNumber)
        {
            var data = _uow.user.GetFirstOfDefault(x => x.Phone == phoneNumber);
            if (data != null)
            {
                data.Status = false;
                _uow.user.Update(data);
                _uow.Save();

                return new Result<UserViewModel>(true, ResultConstants.DeleteOk);
            }
            else
                return new Result<UserViewModel>(false, ResultConstants.DeleteNotOk);
        }

        public Result<UserViewModel> EditUser(UserViewModel model, string guidId)
        {
            if (model != null)
            {
                var user = _mapper.Map<UserViewModel, User>(model);

                user.ModifiedDate = DateTime.Now;
                user.ModifiedBy = new Guid(guidId);

                _uow.user.Update(user);
                _uow.Save();
                return new Result<UserViewModel>(true, ResultConstants.EditSuccess);
            }
            else
                return new Result<UserViewModel>(false, ResultConstants.EditFail);
        }

        public Result<User> FindByPhoneNumber(string phoneNumber)
        {
            var data = _uow.user.GetFirstOfDefault(x => x.Phone == phoneNumber, includeProperties: "Institution");
            var type = _uow.institution.GetFirstOfDefault(y => y.Type == data.Type, includeProperties: "Title");

            if (data != null)
            {
                return new Result<User>(true, ResultConstants.PhoneIsExist, data);
            }
            return new Result<User>(false, ResultConstants.PhoneIsNotExist, null);
        }

        public Result<List<UserViewModel>> GetAllUser()
        {
            var user = _uow.user.GetAll(u => u.Status == true, includeProperties: "Institution").ToList();
            var type = _uow.institution.GetAll(u => u.Status == true, includeProperties: "Title").ToList();

            if (user != null)
            {
                #region 1.yöntem
                //List<UserViewModel> userVm = new List<UserViewModel>();
                //foreach (var item in user)
                //{
                //    userVm.Add(new UserViewModel()
                //    {
                //        Id = item.Id,
                //        //Institution = item.Institution,
                //        FullName = item.FullName,
                //        Email = item.Email,
                //        Type = item.Institution.Title.Id,
                //        CountryCode = item.CountryCode,
                //        Longtitude = item.Longtitude,
                //        Lottitude = item.Lottitude,
                //        Phone = item.Phone,
                //        InstitutionId = item.InstitutionId,
                //    }); ;
                //}

                #endregion

                var userVm = _mapper.Map<List<User>, List<UserViewModel>>(user);
                return new Result<List<UserViewModel>>(true, ResultConstants.UserList, userVm);
            }
            return new Result<List<UserViewModel>>(false, ResultConstants.UserListNotFound);
        }

        public Result<List<UserViewModel>> GetCustomers()
        {
            List<UserViewModel> customerList = new List<UserViewModel>();
            var customers = _uow.user.GetAll(u => u.Status == true, includeProperties: "Institution").ToList();
            var type = _uow.institution.GetAll(u => u.Status == true, includeProperties: "Title").ToList();
            if (customers != null)
            {
                var userVm = _mapper.Map<List<User>, List<UserViewModel>>(customers);
                foreach (var item in userVm)
                {
                    if (item.Institution.Title.TitleName == "Customer")
                    { 
                        customerList.Add(item);
                    }
                }
                return new Result<List<UserViewModel>>(true, ResultConstants.UserList, customerList);
            }

            return new Result<List<UserViewModel>>(false, ResultConstants.UserListNotFound);
        }

        public Result<UserViewModel> GetUserById(Guid id)
        {
            var data = _uow.user.GetById(id);
            if (data != null)
            {
                var user = _mapper.Map<User, UserViewModel>(data);
                return new Result<UserViewModel>(true, ResultConstants.UserIsExistById, user);
            }
            else
                return new Result<UserViewModel>(false, ResultConstants.UserIsNotExistById, null);
        }
    }
}
