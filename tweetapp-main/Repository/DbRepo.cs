using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tweetapp.Models;
using tweetapp.viewModels;

namespace tweetapp.Repository
{
    public class DbRepo:IDbRepo
    {
        private readonly DB_Context.DB_Context dB;

        public DbRepo(DB_Context.DB_Context dB)
        {
            this.dB = dB;
        }

        public Users AddUser(Users user,out int status)
        {
            if (dB.users.Count(x => x.Email == user.Email) > 0)
            {
                status = -1;
            }
            else
            {
                dB.users.Add(user);
                status = 0;
            }
            dB.SaveChanges();
            return user;
        }

        public LoginModel CheckUserCredintials(LoginModel login,out int status)
        {
            var isValidUser = dB.users.SingleOrDefault(op => op.Email == login.Email);
            if (isValidUser == null)
            {
                status = 1;
                return login;
            }
            status = string.Compare(isValidUser.Password, login.password, false) != 0 ? 1 : 0;

            if (status == 0)
            {
                isValidUser.isLoggedin = login.isLoggedin == 0 ? true : false;
                login.userName = isValidUser.F_Name;
                dB.SaveChanges();
            }
            return login;
        }

        public List<Users> GetAllUsers()
        {
            List<Users> allUsers = new List<Users>();
            allUsers = dB.users.ToList();
            return allUsers;
        }

        public Users GetUserById(string F_name)
        {
            var user = dB.users.FirstOrDefault(o => o.F_Name == F_name);
            return user;
        }

        public int ResetPassword(forgotpassword forgotpassword)
        {
            var User = dB.users.FirstOrDefault(u => u.Email == forgotpassword.Email);
            if (User == null)
            {
                return -1;
            }
            User.Password = forgotpassword.NewPassword;
            dB.SaveChanges();
            return 0;
        }
    }
}
