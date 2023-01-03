using MTSaglikAuthenticator.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.Business.Abstract
{
    public interface ISignInManager
    {
        Task SignInAsync(User user, bool rememberMe);

        Task SignOutAsync();
    }
}
