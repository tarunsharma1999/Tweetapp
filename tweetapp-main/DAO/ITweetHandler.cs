using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tweetapp.Models;
using tweetapp.Repository;
using tweetapp.viewModels;

namespace tweetapp.DAO
{
    public interface ITweetHandler
    {

        public Tweet CreateTweet(Tweet tweet);

        public List<tweet> GetAllTweets();

        public List<tweet> GetAllTweetsByUserID(int userID);

        public tweet UpdateTweet(string message, int tweetId, int userID);

        public int DeleteTweet(int userId, int tweetId);

        public int LikeTweet(int tweeetId);

        public Tweet GetTweetByTweetId(int tweetId);

    }
}
