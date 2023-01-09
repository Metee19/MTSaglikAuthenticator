using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTSaglikAuthenticator.Business.Abstract;
using MTSaglikAuthenticator.Core.Cryptography;
using MTSaglikAuthenticator.Entities.Helpers;
using MTSaglikAuthenticator.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
namespace MTSaglikAuthenticator.Web.Controllers
{
    [Authorize(Policy ="Seller")]
    public class SellerController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserSession _userSession;
        private readonly IFileService _fileService;
        private readonly IInstitutionService _institutionService;

        public SellerController(IUserService userService, IInstitutionService institutionService, IUserSession userSession, IFileService fileService)
        {
            _userService = userService;
            _institutionService = institutionService;
            _userSession = userSession;
            _fileService = fileService;
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
        [HttpPost]
        public IActionResult UploadFile(FileViewModel model, IFormFile files)
        {


            var filePath = AppDomain.CurrentDomain.BaseDirectory + files.FileName;

            using (var stream = files.OpenReadStream())
            using (var output = new FileStream(filePath, FileMode.Create))
            {
                // Dosya verilerini okuma
                byte[] buffer = new byte[8192];
                int bytesRead;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    // Dosya verilerini başka bir dosya olarak yazma
                    output.Write(buffer, 0, bytesRead);
                }
            }
            var key = AesCrypto.CreateUpdateKey();
            var iv = AesCrypto.CreateIv();

            AesCrypto.EncryptFile(filePath, key, iv);
            AesCrypto.DecryptFile(filePath, key, iv);

            byte[] pdf = System.IO.File.ReadAllBytes(filePath);
            byte[] encryptedSoftwarePacket = AesCrypto.Encrypt(pdf, key, iv);

            byte[] b = AesCrypto.Decrypt(encryptedSoftwarePacket, key, iv);

            model.FileHash =  FileHashing.SHA256CheckSum(filePath);
            model.FileName = files.FileName;

            var sellerId = _userSession.Id;
            var data = _fileService.CreateFileForUser(model, sellerId);

            if(data.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            
            return View(model);
        }

       


    }
}
