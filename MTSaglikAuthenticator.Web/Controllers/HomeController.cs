using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MTSaglikAuthenticator.Business.Abstract;
using MTSaglikAuthenticator.Core.Constants;
using MTSaglikAuthenticator.Entities.Models;
using MTSaglikAuthenticator.Entities.ViewModels;
using MTSaglikAuthenticator.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.Web.Controllers
{
    [Authorize(Policy = "Admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserSession _userSession;
        private readonly IInstitutionService _institutionService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IInstitutionService institutionService, IUserService userService, IMapper mapper, IUserSession userSession)
        {
            _logger = logger;
            _institutionService = institutionService;
            _userService = userService;
            _mapper = mapper;
            _userSession = userSession;
            
        }     
        #region User Operations
        public IActionResult Index()
        {
            var model = _userService.GetAllUser();

            if (model.IsSuccess)
            {
                return View(model.Data);
            }

            return View();
        }

        public IActionResult Create()
        {
            ViewBag.Institutions = _institutionService.GetAll();

            return View();
        }

        [HttpPost]
        public IActionResult Create(UserViewModel userModel)
        {
            var loginId = _userSession.Id;
            if (ModelState.IsValid)
            {
                var data = _userService.CreateUser(userModel, loginId);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(userModel);
            }

            return View(userModel);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            ViewBag.Institutions = _institutionService.GetAll();

            if (id != null)
            {
                var data = _userService.GetUserById(id);
                if (data.IsSuccess)
                {
                    return View(data.Data);
                }
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(UserViewModel user)
        {
            var loginId = _userSession.Id;
            if (ModelState.IsValid)
            {
                var data = _userService.EditUser(user, loginId);
                if (data.IsSuccess)
                {
                    return RedirectToAction("Index");
                }

                return View(user);
            }
            else
            {
                return View(user);
            }
        }

        [HttpDelete]
        public IActionResult Delete(string phoneNumber)
        {
            if (phoneNumber == null)
            {
                return Json(new { success = false, message = "Silmek için bir kayıt seçiniz" });
            }

            var data = _userService.DeleteUser(phoneNumber);
            if (data.IsSuccess)
            {
                return Json(new { success = data.IsSuccess, message = data.Message });

            }
            else
            {
                return Json(new { success = data.IsSuccess, message = data.Message });
            }
        }


        #endregion

        #region Institution Operations
        public IActionResult InstitutionList()
        {
            var model = _institutionService.GetAll();
            if (model.IsSuccess)
            {
                return View(model.Data);
            }
            return View();
        }


        public IActionResult InstitutionCreate()
        {
            ViewBag.Titles = _institutionService.GetTitles();
            return View();
        }

        [HttpPost]
        public IActionResult InstitutionCreate(InstitutionViewModel model)
        {
            var loginId = _userSession.Id;
            if (model != null)
            {
                var data = _institutionService.CreateInstitution(model, loginId);
                if (data.IsSuccess)
                {
                    return RedirectToAction("InstitutionList");
                }

                return View(model);
            }
            return View(model);
        }


  
        [HttpGet]
        public IActionResult InstitutionEdit(Guid id)
        {
            ViewBag.Titles = _institutionService.GetTitles();
            if (id != null)
            {
                var data = _institutionService.GetInstitutionById(id);
                if (data.IsSuccess)
                {
                    return View(data.Data);
                }
            }

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult InstitutionEdit(InstitutionViewModel model)
        {
            var loginId = _userSession.Id;

            if (ModelState.IsValid)
            {
                var data = _institutionService.EditInstitution(model, loginId);
                if (data.IsSuccess)
                {
                    return RedirectToAction("InstitutionList");
                }
                return View(model);
            }
            return View(model);
        }

        [HttpDelete]
        public IActionResult InstitutionDelete(string companyName)
        {
            if (companyName == null)
            {
                return Json(new { success = false, message = "Silmek için bir kayıt seçiniz" });
            }

            var data = _userService.DeleteUser(companyName);
            if (data.IsSuccess)
            {
                return Json(new { success = data.IsSuccess, message = data.Message });

            }
            else
            {
                return Json(new { success = data.IsSuccess, message = data.Message });
            }
        }
        #endregion




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
