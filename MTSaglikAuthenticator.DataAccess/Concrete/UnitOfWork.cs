using MTSaglikAuthenticator.DataAccess.Abstract;
using MTSaglikAuthenticator.DataAccess.Concrete.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.DataAccess.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MTSaglikAuthenticatorDemoContext _ctx;
        public UnitOfWork(MTSaglikAuthenticatorDemoContext ctx)
        {
            _ctx = ctx;
            file = new FileRepository(_ctx);
            user = new UserRepository(_ctx);
            userActivity = new UserActivityRepository(_ctx);
            institution = new InstitutionRepository(_ctx);
            title = new TitleRepository(_ctx);
        }



        public IFile file { get; private set; }
        public IUser user { get; private set; }
        public IUserActivity userActivity { get; private set; }
        public IInstitution institution { get; private set; }
        public ITitle title { get; private set; }



        public void Dispose()
        {
            _ctx.Dispose();
        }



        public void Save()
        {
            _ctx.SaveChanges();
        }
    }
}
