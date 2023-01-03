using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTSaglikAuthenticator.Business.Abstract;
using MTSaglikAuthenticator.Core.Constants;
using MTSaglikAuthenticator.Entities.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISignInManager _signInManager;
        private readonly IUserSession _userSession;

        public LoginController(IUserService userService, ISignInManager signInManager, IUserSession userSession)
        {
            _userService = userService;
            _signInManager = signInManager;
            _userSession = userSession;
        }

        public IActionResult Login()
        {
            if (_userSession.IsAuthenticated)
            {
                if (_userSession.Type == "Admin")
                {
                    return RedirectToAction("Index", "Home");
                }
                else if(_userSession.Type == "Seller")
                {
                    return RedirectToAction("Index", "Seller");
                }
            }
           
                return View();
            
            
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            //_userService.Add();
            if (ModelState.IsValid)
            {
                var data = _userService.FindByPhoneNumber(model.Phone).Data;
                if (data != null)
                {
                    //var userInfo = new SessionContext()
                    //{
                    //    LoginId = data.Id.ToString(),
                    //    FullName = data.FullName,
                    //    Phone = data.Phone
                    //};
                    //HttpContext.Session.SetString(ResultConstants.LoginUserInfo, JsonConvert.SerializeObject(userInfo));

                    _signInManager.SignInAsync(data, model.RememberMe);
                    if (data.Institution.Title.TitleName == "Admin")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (data.Institution.Title.TitleName == "Seller")
                    {
                        return RedirectToAction("Index", "Seller");
                    }
                    
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(LoginController.Login));
        }

        public IActionResult Authentication()
        {
            return View();
        }
    }
}
