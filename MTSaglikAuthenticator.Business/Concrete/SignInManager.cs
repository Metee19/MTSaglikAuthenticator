using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using MTSaglikAuthenticator.Business.Abstract;
using MTSaglikAuthenticator.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.Business.Concrete
{
    public class SignInManager : ISignInManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SignInManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SignInAsync(User user, bool rememberMe)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.MobilePhone, user.Phone),
                new Claim(ClaimTypes.Sid, user.Id.ToString())
            };

            claims.Add(new Claim(ClaimTypes.Role, user.Institution.Title.TitleName));

            var identity = new ClaimsIdentity(claims, "local", "name", "role");
            var principal = new ClaimsPrincipal(identity);

            AuthenticationProperties authenticationProperties = new AuthenticationProperties() { };
            if (rememberMe)
            {
                authenticationProperties.IsPersistent = rememberMe;
                // One month for example
                authenticationProperties.ExpiresUtc = DateTimeOffset.UtcNow.AddMonths(1);
            }
            else
            {
                authenticationProperties.IsPersistent = !rememberMe;
                authenticationProperties.ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(1);

            }
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);
        }

        public async Task SignOutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
