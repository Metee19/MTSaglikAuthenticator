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
    public class FileRepository : RepositoryBase<File>, IFile
    {
        private readonly MTSaglikAuthenticatorDemoContext _ctx;
        public FileRepository(MTSaglikAuthenticatorDemoContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

    }
}
