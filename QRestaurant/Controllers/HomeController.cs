using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QRestaurantMain.Data;
using QRestaurantMain.Models;
using QRestaurantMain.Services;
using QRestaurantMain.Services.Filter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace QRestaurantMain.Controllers
{
    [LoginFilter]
    public class HomeController : Controller
    {
        private readonly QRestaurantDbContext AppDb;
        public ICompanyService companyService { get; set; }

        public HomeController (QRestaurantDbContext Db, ICompanyService _companyService)
        {
            AppDb = Db;
            companyService = _companyService;
        }

        // GET - Home Page
        [CompanyFilter]
        [LoginFilter]
        public IActionResult Index()
        {
            return View();
        }

        // GET - Select user Company
        [LoginFilter]
        public IActionResult SelectCompany()
        {
            var userId = HttpContext.Session.GetString("Id");
            var model = companyService.UserCompanyList(userId);
            return View(model);
        }

        /// <summary>
        ///  Action Select Company
        /// </summary>
        /// <param name="Id">Company Id</param>
        /// <returns> Home Page </returns>
        [LoginFilter]
        public IActionResult SelectedCompany(string? Id)
        {
            string userId = HttpContext.Session.GetString("Id");
            string perms = companyService.GetCompanyPerms(userId, Id); 
            HttpContext.Session.SetString("Company", Id);
            HttpContext.Session.SetString("Perms", perms);
            return RedirectToAction("Index");
        }

        [LoginFilter]
        public IActionResult ChangeCompany()
        {
            HttpContext.Session.Remove("Company");
            HttpContext.Session.Remove("Perms");
            return RedirectToAction("SelectCompany");
        }
    }
}
