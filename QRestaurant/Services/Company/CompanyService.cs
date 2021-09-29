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
        {            var user = AppDb.Users.FirstOrDefault(x => x.UserId == UserId);
            if (user == null || user.UserId == "")
                return null;

            List<CompanySelectorViewModel> model = AppDb.UsersCompany.Where(x => x.UserId == user.UserId)
                .Join(AppDb.Company,
                userCompany => userCompany.CompanyId,
                company => company.CompanyId,
                (userCompany, company) => new CompanySelectorViewModel
                {
                    Id = userCompany.CompanyId,
                    CompanyName = company.Name,
                    CompanyLogo = company.LogoUrl
                }).ToList();
            return model;
        }

        public string GetCompanyPerms(string userId, string companyId)
        {
            var user = AppDb.Users.FirstOrDefault(x => x.UserId == userId);
            if (user == null)
                return null;
            var company = AppDb.UsersCompany.FirstOrDefault(x => x.CompanyId == companyId && x.CompanyId == companyId);
            if (company == null)
                return null;
            var role = AppDb.UsersRoles.FirstOrDefault(x => x.UsersRolesId == company.RoleId);
            return role.Perms;
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
            var company = AppDb.Company.FirstOrDefault(x => x.CompanyId == companyId);
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
            company.Schedule = result;
            AppDb.SaveChanges();
        }
    
        public List<EmplyeeViewModel> GetCompanyEmployees(string companyId)
        {
            var rolesList = AppDb.UsersRoles.Where(x => x.CompanyId == companyId).ToList();
            var company = AppDb.Company.FirstOrDefault(x => x.CompanyId == companyId);
            if (company == null)
                return null;
            List<EmplyeeViewModel> model = (from userCompany in AppDb.UsersCompany
                                            join user in AppDb.Users on userCompany.UserId equals user.UserId
                                            join role in AppDb.UsersRoles on userCompany.RoleId equals role.UsersRolesId
                                            where userCompany.CompanyId == companyId
                                            select new EmplyeeViewModel
                                            {
                                                userId = userCompany.UserId,
                                                userName = user.Name,
                                                perms = role.RoleName
                                            }).ToList();
            return model;
        } 

        public UsersModel GetEmployeeDetails(string userId, string companyId)
        {
            if (userId == null || userId == "" || companyId == null || companyId == "")
                return null;
            UsersModel model = (from userCompany in AppDb.UsersCompany
                                join user in AppDb.Users on userCompany.UserId equals user.UserId
                                join role in AppDb.UsersRoles on userCompany.RoleId equals role.UsersRolesId
                                where userCompany.CompanyId == companyId && userCompany.UserId == userId
                                select new UsersModel
                                {
                                    UserId = userId,
                                    Email = user.Email,
                                    Name = user.Name,
                                    Password = " ",
                                    VerifiedEmail = true                                  
                                }).FirstOrDefault();
            return model;
        }

        public bool NewUsersRole(string companyId, string roleName, RolePermsViewModel perms)
        {
            string permsjoin = string.Join(",", perms.ToString());
            AppDb.UsersRoles.Add(new UsersRolesModel
            {
                UsersRolesId = Guid.NewGuid().ToString(),
                CompanyId = companyId,
                RoleName = roleName,
                Perms = permsjoin
            });
            AppDb.SaveChanges();
            return true;
        }
    }
}
