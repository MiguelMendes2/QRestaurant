using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRestaurantMain.Data;
using QRestaurantMain.Models;
using QRestaurantMain.Services;
using QRestaurantMain.Services.Filter;
using QRestaurantMain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QRestaurantMain.Controllers
{
    [LoginFilter]
    [CompanyFilter]
    public class CompanyController : Controller
    {

        private readonly QRestaurantDbContext AppDb;
        public ICompanyService companyService { get; set; }
        public ISecurityServices securityService { get; set; }

        public CompanyController(QRestaurantDbContext Db, ICompanyService _companyService, ISecurityServices _securityService)
        {
            AppDb = Db;
            companyService = _companyService;
            securityService = _securityService;
        }

        // GET - Company Main Page
        [HttpGet]
        public IActionResult Index()
        {
            string CompanyId = HttpContext.Session.GetString("Company");
            var company = AppDb.Company.FirstOrDefault(x => x.CompanyId == CompanyId);
            var owner = AppDb.Users.FirstOrDefault(x => x.UserId == company.OwnerId);
            if (owner != null)
                ViewBag.ownerName = owner.Name;            
            else
                ViewBag.ownerName = "Não encontrado";
            ViewBag.companyName = company.Name;
            return View();
        }

        // GET - Edit Company Schedule
        [HttpGet]
        public IActionResult ChangeSchedule()
        {
            string CompanyId = HttpContext.Session.GetString("Company");
            var company = AppDb.Company.FirstOrDefault(x => x.CompanyId == CompanyId);
            if (company == null)
                return NotFound();
            var model = companyService.GetCompanySchedule(company.Schedule);
            return View(model);
        }

        // POST - Edit Company Schedule
        [HttpPost]
        public IActionResult ChangeSchedule(ScheduleViewModel model)
        {
            string companyId = HttpContext.Session.GetString("Company");
            companyService.EditCompanySchedule(model, companyId);
            return RedirectToAction("Index");
        }

        // GET - Company Roles List
        [HttpGet]
        public IActionResult Roles()
        {
            string companyId = HttpContext.Session.GetString("Company");
            var model = AppDb.UsersRoles.Where(x => x.CompanyId == companyId).ToList();
            return View(model);
        }

        // GET - New Role Form
        [HttpGet]
        public IActionResult NewRole()
        {
            return View();
        }

        // POST - New role
        [HttpPost]
        public IActionResult NewRole(string roleName, RolePermsViewModel perms)
        {
            string companyId = HttpContext.Session.GetString("Company");
            bool flag = companyService.NewUsersRole(companyId, roleName, perms);
            if (flag)
            {
                TempData["RoleSucess"] = "Cargo criado com sucesso!";
                return RedirectToAction("Roles");
            }                
            else
            {
                TempData["RoleSucess"] = "Já existe um cargo com o mesmo nome!";
                return View();
            }           
        }

        // GET - Statistics
        [HttpGet]
        public IActionResult Statistics()
        {
            return View();
        }

        // GET - Company Main Logs
        [HttpGet]
        public IActionResult CompanyLogs()
        {
            return View();
        }
    }
}
