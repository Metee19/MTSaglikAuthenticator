using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.Business.Abstract
{
    public interface IUserSession
    {
        bool IsAuthenticated { get; }
        string Id { get; }
        string FullName { get; }
        string Type { get; }
        string Phone { get; }
    }
}
