using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tweetapp.DAO;
using tweetapp.Models;
using tweetapp.viewModels;

namespace tweetapp.Controllers
{
    [Route("/api/v1/tweets/")]
    public class TweetApiController : ControllerBase
    {
        public TweetApiController(IUserHandler userHandler)
        {
            UserHandler = userHandler;
        }

        public IUserHandler UserHandler { get; }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromBody]Users userRegister)
        {
            int status;
            if (ModelState.IsValid)
            {
                status = UserHandler.Register(userRegister);

                if (status == 1)
                {
                    //throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
                }
                return Ok(userRegister);
            }
            return null;
            //throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody]LoginModel login)
        {
            int status;
            if (!ModelState.IsValid)
            {
                status = UserHandler.Login(login);
            }
            return Ok();
        }
        [HttpGet]
        public IActionResult GetallUsers()
        {
            List<Users> allUsers = null;
            try
            {
                allUsers =  UserHandler.allUsers();
            }
            catch
            {
                return null;
            }
            return Ok(allUsers);
        }
    }
}
