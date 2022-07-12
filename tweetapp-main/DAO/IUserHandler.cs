using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tweetapp.Models;
using tweetapp.viewModels;

namespace tweetapp.DAO
{
    public interface IUserHandler
    {
        public LoginModel Login(LoginModel loginModel,out int status);

        public int Register(Users user);

        public List<Users> allUsers();

        public int resetPassword(forgotpassword forgotpassword);
        public Users SearchUser(string userName);
    }
}
