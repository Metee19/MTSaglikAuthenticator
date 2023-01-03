using Microsoft.AspNetCore.Http;
using MTSaglikAuthenticator.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.Business.Concrete
{
    public class UserSession : IUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string FullName => _httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.Name)?.Value;

        public string Type => _httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.Role)?.Value;

        public string Phone => _httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.MobilePhone)?.Value;

        public bool IsAuthenticated => _httpContextAccessor.HttpContext.User?.Identity?.IsAuthenticated ?? false;

        public string Id => _httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.Sid)?.Value;
    }
}
