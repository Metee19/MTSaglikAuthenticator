using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MTSaglikAuthenticator.Business.Abstract;
using MTSaglikAuthenticator.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.Web.Controllers
{
    [Authorize(Policy ="Seller")]
    public class SellerController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserSession _userSession;
        private readonly IInstitutionService _institutionService;

        public SellerController(IUserService userService, IInstitutionService institutionService, IUserSession userSession)
        {
            _userService = userService;
            _institutionService = institutionService;
            _userSession = userSession;
        }

        public IActionResult Index()
        {
            var model = _userService.GetCustomers();

            if (model.IsSuccess)
            {
                return View(model.Data);
            }

            return View();
        }

        public IActionResult Create()
        {
            ViewBag.CustomerInstitutions = _institutionService.GetCustomerType();
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserViewModel model)
        {
            var loginId = _userSession.Id;
            if (ModelState.IsValid)
            {
                var data = _userService.CreateUser(model, loginId);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            return View(model);
        }

        public IActionResult UploadFile()
        {
            ViewBag.File = _userService.GetCustomers();
            return View();
        }

    }
}
