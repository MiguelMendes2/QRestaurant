using QRestaurantMain.Models;
using QRestaurantMain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QRestaurantMain.Services
{
    public interface ICompanyService
    {
        public List<CompanySelectorViewModel> UserCompanyList(string userId);

        public string GetCompanyPerms(string userId, string companyId);

        public ScheduleViewModel GetCompanySchedule(string schedule);

        public void EditCompanySchedule(ScheduleViewModel model, string companyId);

        public List<EmplyeeViewModel> GetCompanyEmployees(string companyId);

        public UsersModel GetEmployeeDetails(string userId, string companyId);

        public bool NewUsersRole(string companyId, string roleName, RolePermsViewModel perms);
    }
}
