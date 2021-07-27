using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRestaurantMain.Data;
using QRestaurantMain.Models;
using QRestaurantMain.Services;
using QRestaurantMain.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QRestaurantMain.Controllers
{
    public class AccountController : Controller
    {
        private readonly QRestaurantDbContext AppDb;
        public ISecurityServices securityServices { get; set; }

        public AccountController(QRestaurantDbContext Db, ISecurityServices _securityServices)
        {
            AppDb = Db;
            securityServices = _securityServices;
        }

        // GET - User Details
        [HttpGet]
        [LoginFilter]
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetString("Id");
            if(userId == null)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login");
            }
            UsersModel model = AppDb.Users.FirstOrDefault(x => x.Id == userId);
            return View(model);
        }

        // GET - Change user Password
        [HttpGet]
        [LoginFilter]
        public IActionResult Password()
        {
            var userId = HttpContext.Session.GetString("Id");
            if (userId == null)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login");
            }
            return View();
        }

        // POST - Change user Password
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoginFilter]
        public IActionResult Password(string ActPwd, string NewPwd, string Confpwd)
        {
            if(NewPwd != Confpwd)
            {
                ViewBag.AltPwdError = "As passwords não coicidem!";
                return View();
            }
            if (NewPwd == ActPwd)
            {
                ViewBag.AltPwdError = "A nova password é igual á antiga!";
                return View();
            }
            var userId = HttpContext.Session.GetString("Id");
            if (userId == null)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login");
            }
            if (securityServices.ChangePassword(userId, ActPwd, NewPwd))
            {
                TempData["AccountSucess"] = "Password Alterada com sucesso";
                return RedirectToAction("index");
                
            }
            ViewBag.AltPwdError = "Password atual incorreta!";
            return View();

        }

        /// <summary>
        ///     GET - Change Current Email
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [LoginFilter]
        public IActionResult Email()
        {
            var userId = HttpContext.Session.GetString("Id");
            if (userId == null)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login");
            }
            var user = AppDb.Users.FirstOrDefault(x => x.Id == userId);
            ViewBag.ActEmail = user.Email;
            return View();
        }
        
        /// <summary>
        ///     POST - Change Current Email
        /// </summary>
        /// <param name="newEmail"> new User Email</param>
        /// <returns></returns>
        [HttpPost]
        [LoginFilter]
        public IActionResult Email(string NewEmail, string ConfEmail)
        {
            var userId = HttpContext.Session.GetString("Id");
            if (userId == null)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login");
            }

            if(NewEmail != ConfEmail)
            {
                ViewBag.AltEmailError = "Os campos não coicidem!";
                return View();
            }





            return View();
        }

        // GET - Request Email Confirmation
        [HttpGet]
        [LoginFilter]
        public IActionResult RequestEmailConfirmation()
        {
            return View();
        }

        // GET - Email Confirmation
        [HttpGet]
        [LoginFilter]
        public IActionResult ConfirmEmail(string? key)
        {
            if(key != null)
            {
                if (securityServices.ConfirmEmail(key))
                    return View();
            }
            return NotFound();
        }

        [LoginFilter]
        public IActionResult SelectCompany()
        {
            string UserId = HttpContext.Session.GetString("Id");
            var model = securityServices.UserCompanyList(UserId);
            return View(model);
        }

        // GET - Login
        [HttpGet]
        public IActionResult Login()
        {
            if (TempData.ContainsKey("loginError"))
                ViewBag.LoginError = TempData["loginError"].ToString();
            return View();
        }

        // POST - Verify Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult VerifyLogin(LoginViewModel user)
        {
            if(user.email == null || user.password == null)
            {
                TempData["loginError"] = "Preencha todos os campos!";
                return RedirectToAction("Login");
            }
            string[] verifyLogin = securityServices.VerifyLogin(user.email, user.password);
            if(verifyLogin != null)
            {
                HttpContext.Session.SetString("Id", verifyLogin[0].ToString());
                HttpContext.Session.SetString("Name", verifyLogin[1].ToString());
                HttpContext.Session.SetString("Perms", verifyLogin[2].ToString());
                HttpContext.Session.SetString("CompanyId", verifyLogin[3].ToString());
                return RedirectToAction("index", "Home");
            }
            TempData["loginError"] = "O Email e/ou Password estão incorretos!";
            return RedirectToAction("Login");
        }

        // GET - End Session
        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
