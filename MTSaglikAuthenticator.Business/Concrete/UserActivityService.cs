using MTSaglikAuthenticator.Business.Abstract;
using MTSaglikAuthenticator.DataAccess.Abstract;
using MTSaglikAuthenticator.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.Business.Concrete
{
    public class UserActivityService : IUserActivityService
    {
        private readonly IUserActivity _userActivity;

        public UserActivityService(IUserActivity userActivity)
        {
            _userActivity = userActivity;
        }

        public void Add(UserActivity entity)
        {
            _userActivity.Add(entity);
        }

        public void Delete(UserActivity entity)
        {
            throw new NotImplementedException();
        }

        public UserActivity Get(Expression<Func<UserActivity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public UserActivity GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IList<User> GetList(Expression<Func<UserActivity, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public void Update(UserActivity entity)
        {
            throw new NotImplementedException();
        }
    }
}
