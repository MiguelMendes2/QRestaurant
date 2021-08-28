using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRestaurantMain.Data;
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
        public IActionResult Index()
        {
            string CompanyId = HttpContext.Session.GetString("Company");
            var company = AppDb.Company.FirstOrDefault(x => x.Id == CompanyId);
            var owner = AppDb.Users.FirstOrDefault(x => x.Id == company.OwnerId);
            if (owner != null)
                ViewBag.ownerName = owner.Name;            
            else
                ViewBag.ownerName = "Não encontrado";
            ViewBag.companyName = company.CompanyName;
            return View();
        }

        // GET - Edit Company Schedule
        public IActionResult ChangeSchedule()
        {
            string CompanyId = HttpContext.Session.GetString("Company");
            var company = AppDb.Company.FirstOrDefault(x => x.Id == CompanyId);
            if (company == null)
                return NotFound();
            var model = companyService.GetCompanySchedule(company.CompanySchedule);
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

        // GET - Employees List
        public IActionResult Employees()
        {
            string companyId = HttpContext.Session.GetString("Company");
            var model = companyService.GetCompanyEmployees(companyId);
            return View(model);
        }

        // GET - Employee Details
        public IActionResult EmployeeDetails(string? id)
        {
            string companyId = HttpContext.Session.GetString("Company");
            var user = companyService.GetEmployeeDetails(id, companyId);
            return View(user);
        }

        // POST - Remove Employee
        [HttpPost]
        public IActionResult RemoveEmployee(string? id)
        {
            string companyId = HttpContext.Session.GetString("Company");
            string adminId = HttpContext.Session.GetString("Id");
            int result = securityService.RemoveFromCompany(adminId, id, companyId);
            switch (result)
            {
                case 0:
                    TempData["EmployeeSucess"] = "Funcionário removido com sucesso";
                    return RedirectToAction("Employees");
                case -1:
                    TempData["EmployeeError"] = "Algo correu mal, tente novamente mais tarde";
                    break;
                case -2:
                    TempData["EmployeeError"] = "Você não pode remover se a si proprio da empresa";
                    break;
                case -3:
                    TempData["EmployeeError"] = "Você não consegue remover o proprietário da empresa";
                    break;
            }
            
            return RedirectToAction("EmployeeDetails", new { id });
        }

        // GET - Statistics
        public IActionResult Statistics()
        {
            return View();
        }

        // GET - Company Main Logs
        public IActionResult CompanyLogs()
        {
            return View();
        }
    }
}
