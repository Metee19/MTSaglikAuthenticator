using MTSaglikAuthenticator.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.Business.Abstract
{
    public interface IUserActivityService
    {
        UserActivity Get(Expression<Func<UserActivity, bool>> filter);
        IList<User> GetList(Expression<Func<UserActivity, bool>> filter = null);

        UserActivity GetById(Guid id);
        void Add(UserActivity entity);
        void Update(UserActivity entity);
        void Delete(UserActivity entity);
    }
}
