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
    public interface IUserService
    { 
        Result<User> FindByPhoneNumber(string phoneNumber);

        Result<UserViewModel> CreateUser(UserViewModel model, string guidId);

        Result<List<UserViewModel>> GetAllUser();

        Result<UserViewModel> GetUserById(Guid id);

        Result<UserViewModel> EditUser(UserViewModel model, string guidId);

        Result<UserViewModel> DeleteUser(string phoneNumber);

        Result <List<UserViewModel>> GetCustomers();
        
    }
}
