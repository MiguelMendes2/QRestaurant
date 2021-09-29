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

        public bool ChangeName(string userId, string newName);

        public bool ChangeEmail(string userId, string newEmail);

        public int ConfirmEmail(string Id);

        public bool ChangePassword(string userId, string actPwd, string newPwd);

        public bool AddUserToCompany(string email, string companyId, string roleId);

        public int RemoveFromCompany(string adminId,string userId, string companyId);

        
    }
}
