using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tweetapp.DAO;
using tweetapp.Models;
using tweetapp.viewModels;
using Microsoft.EntityFrameworkCore;
//using System.Web.Mvc;

namespace tweetapp.DAO
{
    public class UserHandler : IUserHandler
    {
        private readonly DB_Context.DB_Context db;

        public UserHandler(DB_Context.DB_Context db)
        {
            this.db = db;
        }
        public List<Users> allUsers()
        {
            List<Users> allUser = null;
            try
            {
                allUser = db.users.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                allUser = null;
            }
            return allUser;
        }

        public int Login(LoginModel loginModel)
        {
            int status = 0;
            try
            {
                if (loginModel != null)
                {
                    var isValidUser = db.users.FirstOrDefault(op => op.Email == loginModel.Email && op.Password == loginModel.password);

                    status = isValidUser == null ? 1 : 0;

                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                status = 1;
            }
            return status;

        }

        public int Register(Users user)
        {
            int status = 0;
            // 0 for user registerd ; 1 for error occured
            try
            {
                if (user != null)
                {
                    db.users.Add(user);
                    db.SaveChanges();
                }
            }
            catch(Exception e) 
            {
                Console.WriteLine(e.Message);

                status = 1;
            }
            return status;
        }
    }
}
