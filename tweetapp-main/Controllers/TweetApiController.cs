using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tweetapp.DAO;
using tweetapp.Models;
using tweetapp.viewModels;
using Newtonsoft.Json.Linq;

namespace tweetapp.Controllers
{
    [Route("/api/v1/tweets/")]
    public class TweetApiController : ControllerBase
    {
        

        public TweetApiController(IUserHandler userHandler,ITweetHandler tweet)
        {
            UserHandler = userHandler;
            Tweet = tweet;
        }

        public IUserHandler UserHandler { get; }
        public ITweetHandler Tweet { get; }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Register([FromBody]Users userRegister)
        {
            int status = 1;
            if (ModelState.IsValid)
            {
                status = UserHandler.Register(userRegister);
            }
            if (status == -1)
            {
                return new ContentResult() { Content = "User already exist" };
            }
            return status == 0 ? Ok(userRegister):  new BadRequestObjectResult(userRegister); 
        }

        [HttpPost]
        [Route("[action]")]

        public IActionResult Login([FromBody]LoginModel login)
        {
            int status = 1;
            if (ModelState.IsValid)
            {
                login = UserHandler.Login(login,out status);
            }
            return status == 0 ? Ok(login) : new ContentResult() { Content="Invalid User"  };
        }
        [HttpPost]
        [Route("{username}/[action]/{password}")]

        public IActionResult Reset(string username, [FromBody] forgotpassword model,string password)
        {
            int status = 1;
            if (ModelState.IsValid)
            {
                Users user = UserHandler.SearchUser(username); //Search user on f_Name
                if (user == null)
                {
                    return new ContentResult() { Content = "User not found" };
                }
                else if (user.Email.ToLower() == model.Email.ToLower() && user.Password == password)
                {
                    status = UserHandler.resetPassword(model);
                }
                else
                {
                    return new ContentResult() { Content = "User details do not matches" };
                }
            }
            return status == 0 ? Ok(model) : new ContentResult() { Content = "User not found" };
        }

        [HttpPost]
        [Route("{username}/[action]")]

        public IActionResult Forgot(string username,[FromBody] forgotpassword model)
        {
            int status = 1;
            if (ModelState.IsValid)
            {
                Users user = UserHandler.SearchUser(username); //Search user on f_Name
                if (user == null)
                {
                    return new ContentResult() { Content = "User not found" };
                }
                status = UserHandler.resetPassword(model);
            }
            return status == 0 ? Ok(model) : new ContentResult() { Content = "User not found" };
        }
        [HttpGet]
        [Route("[action]/all")]

        public IActionResult Users()
        {
            List<Users> allUsers = null;
            try
            {
                allUsers =  UserHandler.allUsers();
            }
            catch
            {
                return BadRequest();
            }
            return Ok(allUsers);
        }

        [Route("{username}/[action]")]
        [HttpPost]
        public IActionResult Add(string username,[FromBody]tweet tweet)
        {

            if (ModelState.IsValid)
            {
                Users user = UserHandler.SearchUser(username); //Search user on f_Name
                if (user != null)
                {
                    
                        Tweet objTweet = new Tweet(tweet.TweetMessage, user);
                        tweet.Created_Datetime = objTweet.Created_Datetime;
                        Tweet.CreateTweet(objTweet);
                        tweet.TweetId = objTweet.TweetId;
                }
                else
                { 
                    return new ContentResult() { Content = "User not found" };
                }
            }
            else
            {
                return new ContentResult() { Content = "Tweet length exceeds 144 chars"};
            }
            return tweet != null ? Ok(tweet) : new BadRequestObjectResult(tweet) ;
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult All()
        {
            return Ok(Tweet.GetAllTweets());
        }

        [Route("{userName}/[action]")]
        [HttpGet]
        public IActionResult All(string userName)
        {
            List<tweet> tweets = new List<tweet>();
            if(ModelState.IsValid)
            {
                Users user = UserHandler.SearchUser(userName);
                if (user != null)
                {
                    tweets = Tweet.GetAllTweetsByUserID(user.UserId);
                }
            }
            return Ok(tweets);
        }

        [HttpPut]
        [Route("{username}/[action]/{id}")]
        public IActionResult Update(string username, int id, [FromBody] tweet tweet)
        {
            Users user = UserHandler.SearchUser(username);
            if (user != null && user.F_Name.ToLower() == username.ToLower())
            {
                tweet = Tweet.UpdateTweet(tweet.TweetMessage, id, user.UserId);
            }
            return (user == null) ? new ContentResult() { Content = "No tweet found for this user" } : Ok(tweet);
        }

        [HttpDelete]
        [Route("{username}/[action]/{id}")]
        public IActionResult Delete(string username, int id)
        {
            Users user = UserHandler.SearchUser(username);
            int status = 1;
            if (user != null)
            {
                status = Tweet.DeleteTweet(user.UserId, id);
            }
            return status != 0 ? new ContentResult() { Content = "No tweet found for this user" } : Ok("tweet deleted successfully!");
        }

        [HttpPost]
        [Route("{username}/[action]/{tweetId}")]
        public IActionResult Like(int tweetId)
        {
            return Tweet.LikeTweet(tweetId) == 0 ? Ok() : new ContentResult() { Content = "Tweet does not exist" };
        }

        [HttpPost]
        [Route("{username}/[action]/{id}")]
        public IActionResult Reply(string username, int id, [FromBody] tweet tweet)
        {
            Users user = UserHandler.SearchUser(username);
            if (user != null)
            {
                Tweet mainTweet = Tweet.GetTweetByTweetId(id);
                Tweet objTweet = new Tweet(tweet.TweetMessage, user);
                if (mainTweet == null)
                {
                    return new ContentResult() { Content = "No such twweet" };
                }
                tweet.Created_Datetime = objTweet.Created_Datetime;
                objTweet.ChildId = mainTweet.TweetId;
                Tweet.CreateTweet(objTweet);
                tweet.TweetId = objTweet.TweetId;
                tweet.ChildId = objTweet.ChildId;
            }
            else
            {
                return new ContentResult() { Content = "User not found" };
            }
            return tweet == null ? new ContentResult() { Content = "No tweet found for this user" } : Ok(tweet);
        }
    }
}
