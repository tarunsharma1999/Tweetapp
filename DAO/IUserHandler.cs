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
        public int Login(LoginModel loginModel);

        public int Register(Users user);

        public List<Users> allUsers();
    }
}
