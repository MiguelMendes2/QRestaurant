using QRestaurantMain.Data;
using QRestaurantMain.Models;
using QRestaurantMain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace QRestaurantMain.Services
{
    public class SecurityServices : ISecurityServices
    {
        private readonly QRestaurantDbContext AppDb;

        public SecurityServices(QRestaurantDbContext Db)
        {
            AppDb = Db;
        }

        public string[] VerifyLogin(string email, string password)
        {
            string[] userdata = { "", "", "", "" };
            UsersModel user = AppDb.Users.FirstOrDefault(x => x.Email == email && x.Password == GetHashString(password));
            if(user != null)
            {
                userdata[0] = user.Id;
                userdata[1] = user.Name;
                userdata[2] = user.Perms.ToString();
                userdata[3] = user.CompanyId;
                return userdata;
            }
            return null;
        }

        /// <summary>
        ///  Change Email
        /// </summary>
        /// <param name="userId"> User Id </param>
        /// <param name="newEmail"> New user Email </param>
        public void ChangeEmail(string userId, string newEmail)
        {
            var user = AppDb.Users.FirstOrDefault(x => x.Id == userId);
            string ConfirmationId = Guid.NewGuid().ToString();
            AppDb.UsersActions.Add(new UsersActionsModel
            {
                Id = ConfirmationId,
                UserId = userId,
                Type = 0,
                Date = DateTime.Now,
                ExpDate = DateTime.Now.AddDays(3),
                Data = newEmail
            });
            AppDb.SaveChanges();
            // --- Send Email ---

        }

        /// <summary>
        ///  Email Confirmation
        /// </summary>
        /// <param name="Id"> Action Id </param>
        /// <returns> 
        ///     False -> user not Found 
        ///     True -> Email Confirmed with Sucess
        /// </returns>
        public Boolean ConfirmEmail(string Id)
        {
            var action = AppDb.UsersActions.FirstOrDefault(x => x.Id == Id);
            if (action == null)
                return false;
            var user = AppDb.Users.FirstOrDefault(x => x.Id == action.UserId);
            if (user == null)
                return false;
            if(action.Data == null)
                user.VerifiedEmail = true;
            else
                user.Email = action.Data;
            AppDb.SaveChanges();
            return true;
        }

        /// <summary>
        ///  Change user Password
        /// </summary>
        /// <param name="userId"> User Id </param>
        /// <param name="actPwd"> Actual Password </param>
        /// <param name="newPwd"> New Password </param>
        /// <returns>
        ///     False -> Actual password incorrect
        ///     True -> Password reseted with sucess
        /// </returns>
        public Boolean ChangePassword(string userId, string actPwd ,string newPwd)
        {
            var user = AppDb.Users.FirstOrDefault(x => x.Id == userId);
            if (GetHashString(actPwd) != user.Password)
                return false;
            user.Password = GetHashString(newPwd);
            AppDb.SaveChanges();
            string RevoverId = Guid.NewGuid().ToString(); 
            AppDb.UsersActions.Add(new UsersActionsModel
            {
                Id = RevoverId,
                UserId = userId,
                Type = 0,
                Date = DateTime.Now,
                ExpDate = DateTime.Now.AddDays(15),
                Data = ""
            });
            AppDb.SaveChanges();
            // --- SEND EMAIL 
            // RecoverId -> Pwd Recover

            return true;
        }

        /// <summary>
        ///  Add user to a company
        /// </summary>
        /// <param name="Name"> User Name </param>
        /// <param name="Email"> User Email </param>
        /// <param name="CompanyId"> Id of the new company</param>
        /// <returns> 
        ///     False -> Users already is registed in the compan
        ///     True -> User Added with sucess
        /// </returns>
        public Boolean AddUserToCompany(string Name, string Email, string CompanyId, string perms)
        {
            UsersModel user = AppDb.Users.FirstOrDefault(x => x.Email == Email);
            if(user != null)
            {
                if(user.CompanyId == "")
                {
                    user.CompanyId = CompanyId;
                    user.Perms = perms;
                    AppDb.SaveChanges();
                    return true;
                }
                Boolean flag = true;
                string[] CompanyIdArr = user.CompanyId.Split(',');
                for (int i = 0; i < CompanyId.Length; i++)
                {
                    if(CompanyIdArr[i] == CompanyId)
                    {
                        flag = false;
                        break;
                    }
                }
                if (!flag)
                    return false;
                user.CompanyId += "," + CompanyId;
                user.Perms += "," + perms;
                AppDb.SaveChanges();
                return true;
            }
            string generatedPwd = GeneratePassword();
            AppDb.Users.Add(new UsersModel { Id = Guid.NewGuid().ToString(), Name = Name, CompanyId = CompanyId, Perms = perms, Email = Email, Password = GetHashString(generatedPwd), VerifiedEmail = false });
            AppDb.SaveChanges();

            // --- SEND EMAIL -- 

            return true;
        }

        /// <summary>
        /// Remove user From a company
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="CompanyId"></param>
        /// <returns>
        ///     False -> User not Found
        ///     True -> User Removed from company with sucess
        /// </returns>
        public Boolean RemoveFromCompany(string UserId, string CompanyId)
        {
            UsersModel user = AppDb.Users.FirstOrDefault(x => x.Id == UserId);
            if (user == null)
                return false;
            string[] CompanyIdArr = user.CompanyId.Split(',');
            if(CompanyIdArr.Length == 0)
            {
                user.CompanyId = "";
                user.Perms = "";
                AppDb.SaveChanges();
            }
            int flag = 0;
            for(int i= 0; i < CompanyIdArr.Length; i++)
            {
                if (CompanyIdArr[i] == CompanyId)
                {
                    flag = i;
                    break;
                }
            }
            user.CompanyId = "";
            for (int i = 0; i < CompanyIdArr.Length; i++)
            {
                if (i != flag)
                {
                    user.CompanyId += CompanyIdArr[i];
                    if (i + 1 < CompanyIdArr.Length)
                        user.CompanyId += ",";
                }
            }
            AppDb.SaveChanges();
            return true;
        }

        /// <summary>
        ///  DELETE USER ACCOUNT
        /// </summary>
        /// <param name="UserId"> User Id </param>
        /// <param name="actPwd"> User current password </param>
        /// <returns>
        ///     False -> Incorrect current password
        ///     True -> Account Deleted with success
        /// </returns>
        public Boolean DeleteUser(string UserId, string actPwd)
        {
            UsersModel user = AppDb.Users.FirstOrDefault(x => x.Id == UserId);
            if (user.Password != GetHashString(actPwd))
                return false;
            AppDb.Users.Remove(user);
            AppDb.SaveChanges();
            return true;
        }

        public List<CompanySelectorViewModel> UserCompanyList(string UserId)
        {
            List<CompanySelectorViewModel> model = new List<CompanySelectorViewModel>();
            UsersModel user = AppDb.Users.FirstOrDefault(x => x.Id == UserId);
            if(user == null || user.CompanyId == "")
                return null;
            string[] CompanyIdArr = user.CompanyId.Split(',');
            for(int i = 0; i < CompanyIdArr.Length; i++)
            {
                CompanySelectorViewModel company = AppDb.Company.Select(s => new CompanySelectorViewModel 
                { 
                    Id = s.Id,
                    CompanyName = s.CompanyName,
                    CompanyLogo = s.CompanyLogoUrl
                }).FirstOrDefault(x => x.Id == CompanyIdArr[i]);
                model.Add(company);
            }
            return model;
        }

        public static string GeneratePassword()
        {
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789#&";
            Random random = new Random();
            int size = random.Next(5, 8);
            char[] chars = new char[size];
            for (int i = 0; i < size; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }

            return chars.ToString();
        }

        public static string GetHashString(string String)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(String))
                sb.Append(b.ToString("X2"));
            return sb.ToString();
        }

        public static byte[] GetHash(string String)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(String));
        }
    }
}
