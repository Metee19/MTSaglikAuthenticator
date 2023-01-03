using Microsoft.EntityFrameworkCore;
using MTSaglikAuthenticator.DataAccess.Abstract;
using MTSaglikAuthenticator.DataAccess.Concrete.EntityFramework.Context;
using MTSaglikAuthenticator.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.DataAccess.Concrete
{
    public class UserRepository : RepositoryBase<User>, IUser 
    {
        private readonly MTSaglikAuthenticatorDemoContext _ctx;
        public UserRepository(MTSaglikAuthenticatorDemoContext ctx):base(ctx)
        {
            _ctx = ctx;
        }

        public User GetByPhoneNumber(string phoneNumber)
        {
            return _ctx.Set<User>().Find();
        }

    }
}
