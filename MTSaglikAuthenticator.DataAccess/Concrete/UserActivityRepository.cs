using MTSaglikAuthenticator.DataAccess.Abstract;
using MTSaglikAuthenticator.DataAccess.Concrete.EntityFramework.Context;
using MTSaglikAuthenticator.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.DataAccess.Concrete
{
    public class UserActivityRepository : RepositoryBase<UserActivity>, IUserActivity
    {
        private readonly MTSaglikAuthenticatorDemoContext _ctx;
        public UserActivityRepository(MTSaglikAuthenticatorDemoContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

    }
}
