using QRestaurantMain.Data;
using QRestaurantMain.Models;
using QRestaurantMain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QRestaurantMain.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly QRestaurantDbContext AppDb;

        public CompanyService(QRestaurantDbContext Db)
        {
            AppDb = Db;
        }

        public List<CompanySelectorViewModel> UserCompanyList(string UserId)
        {
            List<CompanySelectorViewModel> model = new List<CompanySelectorViewModel>();
            var user = AppDb.Users.FirstOrDefault(x => x.Id == UserId);
            if (user == null || user.CompanyId == "")
                return null;
            string[] CompanyIdArr = user.CompanyId.Split(',');
            string nullCompanys = "";
            // Get list of companys
            for (int i = 0; i < CompanyIdArr.Length; i++)
            {
                CompanySelectorViewModel company = AppDb.Company.Select(s => new CompanySelectorViewModel
                {
                    Id = s.Id,
                    CompanyName = s.CompanyName,
                    CompanyLogo = s.CompanyLogoUrl
                }).FirstOrDefault(x => x.Id == CompanyIdArr[i]);
                if (company != null)
                    model.Add(company);
                else
                    nullCompanys += CompanyIdArr[i] + ",";
            }            
            // Remove non existing companys from user data
            if(nullCompanys != "")
            {
                nullCompanys = nullCompanys.Remove(nullCompanys.Length - 1);
                string[] nullCompanysArr = nullCompanys.Split(',');
                string[] permsArr = user.Perms.Split(',');
                user.CompanyId = "";
                user.Perms = "";
                for (int i = 0; i < CompanyIdArr.Length; i++)
                {
                    bool flag = true;
                    for (int j = 0; j < nullCompanysArr.Length; j++)
                    {
                        if (CompanyIdArr[i] == nullCompanysArr[j])
                            flag = false;
                    }
                    if (flag)
                    {
                        user.CompanyId += CompanyIdArr[i] + ",";
                        user.Perms += permsArr[i] + ",";
                    }
                        
                }
                user.CompanyId = user.CompanyId.Remove(user.CompanyId.Length - 1);
                user.Perms = user.Perms.Remove(user.Perms.Length - 1);
                AppDb.SaveChanges();
            }
            return model;
        }

        public string GetCompanyPerms(string userId, string companyId)
        {
            var user = AppDb.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null || user.CompanyId == "")
                return null;
            string[] CompanyIdArr = user.CompanyId.Split(',');
            string[] PermsArr = user.Perms.Split(',');
            for (int i = 0; i < CompanyIdArr.Length; i++)
            {
                if (CompanyIdArr[i] == companyId)
                {
                    var role = AppDb.UsersRoles.FirstOrDefault(x => x.Id == PermsArr[i]);
                    return role.Perms;
                }
            }
            return null;
        }

        public ScheduleViewModel GetCompanySchedule(string schedule)
        {
            ScheduleViewModel model = new ScheduleViewModel();
            string[] scheduleDays = schedule.Split(';');
            for(int i = 0; i < scheduleDays.Length; i++)
            {
                string[] scheduleHours = scheduleDays[i].Split(',');
                for(int j = 0; j < scheduleHours.Length; j++)
                {
                    switch (i)
                    {
                        case 0:
                            switch (j)
                            {
                                case 0:
                                    model.timeInput0_0 = scheduleHours[j];
                                    break;
                                case 1:
                                    model.timeInput0_1 = scheduleHours[j];
                                    break;
                                case 2:
                                    model.timeInput0_2 = scheduleHours[j];
                                    break;
                                case 3:
                                    model.timeInput0_3 = scheduleHours[j];
                                    break;
                            }
                            break;
                        case 1:
                            switch (j)
                            {
                                case 0:
                                    model.timeInput1_0 = scheduleHours[j];
                                    break;
                                case 1:
                                    model.timeInput1_1 = scheduleHours[j];
                                    break;
                                case 2:
                                    model.timeInput1_2 = scheduleHours[j];
                                    break;
                                case 3:
                                    model.timeInput1_3 = scheduleHours[j];
                                    break;
                            }
                            break;
                        case 2:
                            switch (j)
                            {
                                case 0:
                                    model.timeInput2_0 = scheduleHours[j];
                                    break;
                                case 1:
                                    model.timeInput2_1 = scheduleHours[j];
                                    break;
                                case 2:
                                    model.timeInput2_2 = scheduleHours[j];
                                    break;
                                case 3:
                                    model.timeInput2_3 = scheduleHours[j];
                                    break;
                            }
                            break;
                        case 3:
                            switch (j)
                            {
                                case 0:
                                    model.timeInput3_0 = scheduleHours[j];
                                    break;
                                case 1:
                                    model.timeInput3_1 = scheduleHours[j];
                                    break;
                                case 2:
                                    model.timeInput3_2 = scheduleHours[j];
                                    break;
                                case 3:
                                    model.timeInput3_3 = scheduleHours[j];
                                    break;
                            }
                            break;
                        case 4:
                            switch (j)
                            {
                                case 0:
                                    model.timeInput4_0 = scheduleHours[j];
                                    break;
                                case 1:
                                    model.timeInput4_1 = scheduleHours[j];
                                    break;
                                case 2:
                                    model.timeInput4_2 = scheduleHours[j];
                                    break;
                                case 3:
                                    model.timeInput4_3 = scheduleHours[j];
                                    break;
                            }
                            break;
                        case 5:
                            switch (j)
                            {
                                case 0:
                                    model.timeInput5_0 = scheduleHours[j];
                                    break;
                                case 1:
                                    model.timeInput5_1 = scheduleHours[j];
                                    break;
                                case 2:
                                    model.timeInput5_2 = scheduleHours[j];
                                    break;
                                case 3:
                                    model.timeInput5_3 = scheduleHours[j];
                                    break;
                            }
                            break;
                        case 6:
                            switch (j)
                            {
                                case 0:
                                    model.timeInput6_0 = scheduleHours[j];
                                    break;
                                case 1:
                                    model.timeInput6_1 = scheduleHours[j];
                                    break;
                                case 2:
                                    model.timeInput6_2 = scheduleHours[j];
                                    break;
                                case 3:
                                    model.timeInput6_3 = scheduleHours[j];
                                    break;
                            }
                            break;
                    }
                }
            }
            return model;
        }

        public void EditCompanySchedule(ScheduleViewModel model, string companyId)
        {
            var company = AppDb.Company.FirstOrDefault(x => x.Id == companyId);
            string result = "";
            int count = 0;
            string date = "";
            foreach (var item in model.GetType().GetProperties())
            {
                if (item.GetValue(model, null) == null)
                    date = "";
                else
                    date = item.GetValue(model, null).ToString();
                if (count > 2)
                {
                    result += date + ";";
                    count = 0;
                }
                else
                {
                    result += date + ",";
                    count++;
                }

            }
            result = result.Remove(result.Length - 1);
            company.CompanySchedule = result;
            AppDb.SaveChanges();
        }
    
        public List<EmplyeeViewModel> GetCompanyEmployees(string companyId)
        {
            List<EmplyeeViewModel> model = new List<EmplyeeViewModel>();
            var rolesList = AppDb.UsersRoles.Where(x => x.CompanyId == companyId).ToList();
            var company = AppDb.Company.FirstOrDefault(x => x.Id == companyId);
            string[] employees = company.CompanyEmployees.Split(',');
            List<string> nullEmployees = new List<string>();
            for(int j = 0; j < employees.Length; j += 2)
            {
                EmplyeeViewModel employee = new EmplyeeViewModel();
                bool flag = true; 
                for (int i = j; i < j+2; i++)
                {
                    if (i % 2 == 0)
                    {
                        var user = AppDb.Users.FirstOrDefault(x => x.Id == employees[i]);
                        if (user != null)
                        {
                            employee.userId = employees[i];
                            employee.userName = user.Name;
                        }
                        else
                        {
                            flag = false;
                            nullEmployees.Add(employees[i]);
                        }
                    }
                    else if(flag)
                    {
                        employee.perms = rolesList.Where(x => x.Id == employees[i]).Select(x => x.RoleName).FirstOrDefault();
                    }
                }
                if(employee.userName != null && employee.userId != null && employee.perms != null)
                    model.Add(employee);
            }
            if (nullEmployees.Count != 0)
                RemoveNullEmployees(nullEmployees, companyId);
            return model;
        } 

        public UsersModel GetEmployeeDetails(string userId, string companyId)
        {
            var user = AppDb.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null)
                return null;
            var company = AppDb.Company.FirstOrDefault(x => x.Id == companyId);
            if (company == null)
                return null;
            string[] companyArr = user.CompanyId.Split(',');
            for(int i = 0; i < companyArr.Length; i++)
            {
                if(companyId == companyArr[i])
                {
                    user.CompanyId = company.CompanyName;
                    user.Password = " ";
                    // Get User Role
                    string[] userPermsArr = user.Perms.Split(',');
                    var Role = AppDb.UsersRoles.FirstOrDefault(x => x.Id == userPermsArr[i]);
                    if (Role == null)
                        return null;
                    user.Perms = Role.RoleName;
                    return user;
                }
            }
            return null;
        }
        
        public async Task RemoveNullEmployees(List<string> nullEmployees, string companyId)
        {
            var company = AppDb.Company.FirstOrDefault(x => x.Id == companyId);
            string[] employees = company.CompanyEmployees.Split(',');
            string employeeStr = "";
            for (int j = 0; j < employees.Length; j += 2)
            {
                bool flag = true;
                for (int i = 0; i < nullEmployees.Count(); i++)
                {
                    if (employees[j] == nullEmployees[i])
                        flag = false;
                }

                if (flag)
                {
                    employeeStr += employees[j] + "," + employees[j + 1] + ",";
                }
            }
            employeeStr = employeeStr.Remove(employeeStr.Length - 1);
            company.CompanyEmployees = employeeStr;
            await AppDb.SaveChangesAsync();
        }
    }
}
