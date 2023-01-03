using MTSaglikAuthenticator.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.DataAccess.Abstract
{
    public interface IUser : IRepositoryBase<User>
    {
        User GetByPhoneNumber(string phoneNumber);

    }
}
