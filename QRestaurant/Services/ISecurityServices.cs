using QRestaurantMain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QRestaurantMain.Services
{
    public interface ISecurityServices
    {
        public string[] VerifyLogin(string email, string password);

        public void ChangeEmail(string userId, string newEmail);

        public Boolean ConfirmEmail(string Id);

        public Boolean ChangePassword(string userId, string actPwd, string newPwd);

        public Boolean AddUserToCompany(string Name, string Email, string CompanyId, string perms);

        public Boolean RemoveFromCompany(string UserId, string CompanyId);

        public List<CompanySelectorViewModel> UserCompanyList(string UserId);
    }
}
