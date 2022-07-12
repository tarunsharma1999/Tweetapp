using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tweetapp.Models;
using tweetapp.viewModels;

namespace tweetapp.Repository
{
    public interface IDbRepo
    {
        List<Users> GetAllUsers();

        Users GetUserById(string F_name);

        LoginModel CheckUserCredintials(LoginModel login, out int status);

        Users AddUser(Users user, out int status);

        int ResetPassword(forgotpassword reset);
    }
}
