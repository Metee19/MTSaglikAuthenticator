using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.DataAccess.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IFile file { get; }
        IUser user { get; }
        IInstitution institution { get; }
        IUserActivity userActivity { get; }
        ITitle title { get; }
        void Save();
    }
}
