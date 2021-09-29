using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRestaurantMain.Data;
using QRestaurantMain.Services;

namespace QRestaurantMain.Controllers.Company
{
    [LoginFilter]
    [Area("Company")]
    public class EmployeesController : Controller
    {
        private readonly QRestaurantDbContext AppDb;
        public ICompanyService companyService { get; set; }
        public ISecurityServices securityService { get; set; }

        public EmployeesController(QRestaurantDbContext Db, ICompanyService _companyService, ISecurityServices _securityService)
        {
            AppDb = Db;
            companyService = _companyService;
            securityService = _securityService;
        }
        // GET - Employees List
        [HttpGet]
        [Route("Company/Employees/")]
        public IActionResult Index()
        {
            string companyId = HttpContext.Session.GetString("Company");
            var model = companyService.GetCompanyEmployees(companyId);
            return View(model);
        }

        // GET - Employee Details
        [HttpGet]
        [Route("Company/Employees/Details/{id}")]
        public IActionResult Details(string id)
        {
            if(id == null)
                return NotFound();
            string companyId = HttpContext.Session.GetString("Company");
            var user = companyService.GetEmployeeDetails(id, companyId);
            return View(user);
        }

        // POST - Remove Employee
        [HttpPost]
        [Route("Company/Employees/Remove/{id}")]
        public IActionResult Remove(string? id)
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
    }
}
