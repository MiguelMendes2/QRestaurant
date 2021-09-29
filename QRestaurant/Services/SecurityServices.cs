using Microsoft.AspNetCore.Http;
using QRestaurantMain.Data;
using QRestaurantMain.Models;
using QRestaurantMain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QRestaurantMain.Services
{
    public class SecurityServices : ISecurityServices
    {
        private readonly QRestaurantDbContext AppDb;

        public SecurityServices(QRestaurantDbContext Db)
        {
            AppDb = Db;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"> login email </param>
        /// <param name="password"> login password </param>
        /// <returns> 
        ///     null -> incorrect email or password 
        ///     array -> login succeeded [0] User Id / [1] User Name 
        /// </returns>
        public string[] VerifyLogin(string email, string password)
        {
            string[] userdata = { "", "", "", "" };
            var user = AppDb.Users.FirstOrDefault(x => x.Email == email && x.Password == GetHashString(password));
            if(user != null)
            {
                userdata[0] = user.UserId;
                userdata[1] = user.Name;
                return userdata;
            }
            return null;
        }

        /// <summary>
        ///  Change User Name
        /// </summary>
        /// <param name="userId"> User Id </param>
        /// <param name="newName">  New User Name </param>
        /// <returns> 
        ///     false -> New UserName equal to current UserName
        ///     true -> UserName changed with success
        /// </returns>
        public bool ChangeName(string userId, string newName)
        {
            var user = AppDb.Users.FirstOrDefault(x => x.UserId == userId);
            if(user != null)
            {
                if(user.Name != newName)
                {
                    user.Name = newName;
                    AppDb.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        ///  Change Email
        /// </summary>
        /// <param name="userId"> User Id </param>
        /// <param name="newEmail"> New user Email </param>
        public bool ChangeEmail(string userId, string newEmail)
        {
            var user = AppDb.Users.FirstOrDefault(x => x.UserId == userId);
            if (user.Email == newEmail)
                return false;
            string ConfirmationId = Guid.NewGuid().ToString();
            AppDb.UsersActions.Add(new UsersActionsModel
            {
                UsersActionsId = ConfirmationId,
                UserId = userId,
                Type = 0,
                Date = DateTime.Now,
                ExpDate = DateTime.Now.AddDays(3),
                Data = newEmail
            });
            AppDb.SaveChanges();

            // --- Send Email ---

            return true;
        }

        /// <summary>
        ///  Email Confirmation
        /// </summary>
        /// <param name="Id"> Action Id </param>
        /// <returns> 
        ///     0 -> user or action not found
        ///     1 -> Email Confirmed with Sucess / New user
        ///     2 -> Email Confirmed with Sucess / Existing user
        /// </returns>
        public int ConfirmEmail(string id)
        {
            var action = AppDb.UsersActions.FirstOrDefault(x => x.UsersActionsId == id);
            if (action == null)
                return 0;
            var user = AppDb.Users.FirstOrDefault(x => x.UserId == action.UserId);
            if (user == null)
                return 0;
            if(action.Data == null)
            {
                user.VerifiedEmail = true;
                AppDb.UsersActions.Remove(action);
                AppDb.SaveChanges();
                return 1;
            }
            else
                user.Email = action.Data;
            AppDb.UsersActions.Remove(action);
            AppDb.SaveChanges();
            return 2;
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
        public bool ChangePassword(string userId, string actPwd ,string newPwd)
        {
            var user = AppDb.Users.FirstOrDefault(x => x.UserId == userId);
            if (GetHashString(actPwd) != user.Password)
                return false;
            user.Password = GetHashString(newPwd);
            AppDb.SaveChanges();
            string RevoverId = Guid.NewGuid().ToString(); 
            AppDb.UsersActions.Add(new UsersActionsModel
            {
                UsersActionsId = RevoverId,
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
        public bool AddUserToCompany(string email, string companyId, string roleId)
        {
            UsersModel user = AppDb.Users.FirstOrDefault(x => x.Email == email);
            if(user != null)
            {
                AppDb.UsersCompany.Add(new UsersCompanys 
                { CompanyId = companyId, 
                    UserId = user.UserId, 
                    RoleId = roleId 
                });
                AppDb.SaveChanges();
                return true;
            }

            string generatedPwd = GeneratePassword();
            string userId = Guid.NewGuid().ToString();
            AppDb.Users.Add(new UsersModel 
            { 
                UserId = userId, 
                Name = "Utilizador",
                Email = email, 
                Password = GetHashString(generatedPwd), 
                VerifiedEmail = false 
            });

            AppDb.UsersCompany.Add(new UsersCompanys
            {
                CompanyId= companyId,
                UserId = userId,
                RoleId = roleId
            });

            string ConfirmationId = Guid.NewGuid().ToString();
            AppDb.UsersActions.Add(new UsersActionsModel
            {
                UsersActionsId = ConfirmationId,
                UserId = userId,
                Type = 0,
                Date = DateTime.Now,
                ExpDate = DateTime.Now.AddDays(20),
                Data = null
            });
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
        ///     0  -> Sucess user removed from the company
        ///     -1 -> Error user or company not found
        ///     -2 -> Error Current user id equals user to be removed 
        ///     -3 -> Error is not possible remove the owner from the company 
        /// </returns>
        public int RemoveFromCompany(string adminId, string userId, string companyId)
        {
            if (adminId == userId)
                return -2;

            var user = AppDb.Users.FirstOrDefault(x => x.UserId == userId);
            var company = AppDb.Company.FirstOrDefault(x => x.CompanyId == companyId);

            if (company == null || user == null)
                return -1;
            if (userId == company.OwnerId)
                return -3;

            var userCompany = AppDb.UsersCompany.FirstOrDefault(x => x.UserId == userId && x.CompanyId == company.CompanyId);
            AppDb.UsersCompany.Remove(userCompany);
            AppDb.SaveChanges();
            return 0;
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
        public bool DeleteUser(string UserId, string actPwd)
        {
            UsersModel user = AppDb.Users.FirstOrDefault(x => x.UserId == UserId);
            if (user.Password != GetHashString(actPwd))
                return false;
            AppDb.Users.Remove(user);
            AppDb.SaveChanges();
            return true;
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
