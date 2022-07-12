using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tweetapp.DAO;
using tweetapp.Models;
using tweetapp.viewModels;
using Microsoft.EntityFrameworkCore;
using tweetapp.Repository;

namespace tweetapp.DAO
{
    public class UserHandler : IUserHandler
    {
        private readonly IDbRepo dbObj;

        public UserHandler(IDbRepo dbObj)
        {
            //Constructor based DI
            this.dbObj = dbObj;
        }
        public Users SearchUser(string userId)
        {
            return dbObj.GetUserById(userId);
        }

        public List<Users> allUsers()
        {
            List<Users> allUser = null;
            try
            {
                allUser = dbObj.GetAllUsers();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                allUser = null;
            }
            return allUser;
        }

        public LoginModel Login(LoginModel loginModel , out int status)
        {
            status = 1;
            try
            {
                if (loginModel != null)
                { 
                    loginModel = dbObj.CheckUserCredintials(loginModel, out status);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                status = 1;
            }
            return loginModel;
        }

        public int Register(Users user)
        {
            int status = 0;
            // 0 for user registerd ; 1 for error occured
            try
            {
                if (user != null)
                {
                    dbObj.AddUser(user, out status);
                }
            }
            catch(Exception e) 
            {
                Console.WriteLine(e.Message);
                status = 1;
            }
            return status;
        }

        public int resetPassword(forgotpassword forgotpassword)
        {
            int status = 1; 
            // 0 password changed
            try
            {
                if (forgotpassword.Email != null)
                {
                    status = dbObj.ResetPassword(forgotpassword);
                }
            }
            catch (Exception e)
            {
                status = 1;
            }
            return status;
        }
    }
}
