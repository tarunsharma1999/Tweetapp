using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tweetapp.Models;
using tweetapp.viewModels;

namespace tweetapp.DAO
{
    public class TweetHandler : ITweetHandler
    {
        private readonly DB_Context.DB_Context db;

        public TweetHandler(DB_Context.DB_Context db)
        {
            this.db = db;
        }
        public Tweet CreateTweet(Tweet tweet)
        {
            try
            {
                if (tweet != null)
                {
                    db.tweets.Add(tweet);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                tweet = null;
            }
            return tweet;
        }

        public List<tweet> GetAllTweets()
        {
            List<tweet> allTweets = null;
            try
            {
                var dbTweet =  db.tweets.ToList();
                allTweets = new List<tweet>();
                foreach (var tweets in dbTweet)
                {
                    tweet t = new tweet(tweets.TweetMessage, tweets.TweetId, tweets.ChildId, tweets.LikeCount,  tweets.Created_Datetime);
                    tweets.UserDetails = db.users.Find(tweets.UserId);
                    t.UserName = tweets.UserDetails.F_Name;
                    allTweets.Add(t);
                }
            }
            catch(Exception e)
            {
                allTweets = null;
            }
            return allTweets;
        }

        public List<tweet> GetAllTweetsByUserID(int userID)
        {
            List<tweet> allTweets = new List<tweet>();
            try
            {
                var all = db.tweets.Where(op => op.UserId == userID).ToList();

                foreach (var i in all)
                {
                    tweet tweet = new tweet(i.TweetMessage, i.TweetId, i.ChildId, i.LikeCount, i.Created_Datetime);
                    allTweets.Add(tweet);
                }
            }
            catch (Exception e)
            {
                allTweets = null;
            }
            return allTweets;
        }

        public tweet UpdateTweet(string message, int tweetId, int userId)
        {
            int status = 1; //0 updated
            tweet updatedTweet = new tweet();
            try
            {
                
                Tweet tweet =  db.tweets.FirstOrDefault(op => op.TweetId == tweetId && op.UserId == userId);
                if (tweet == null)
                {
                    status = -1;
                }
                else
                { 
                    tweet.TweetMessage = message;
                    tweet.Created_Datetime = DateTime.Now;
                    db.SaveChanges();
                    updatedTweet = new tweet(tweet.TweetMessage, tweetId, tweet.ChildId, tweet.LikeCount, tweet.Created_Datetime);
                    status = 0;
                }
            }
            catch (Exception e)
            {
                status = -1;
            }
            return status==0 ? updatedTweet : null;
        }


        public int DeleteTweet(int userId, int tweetId)
        {
            int status = 1; //0 for delete done
            try
            {
                Tweet tweet = db.tweets.FirstOrDefault(o => o.TweetId == tweetId && o.UserId == userId);
                db.tweets.Remove(tweet);
                db.SaveChanges();
                status = 0;
            }
            catch (Exception e)
            {
                status = 1;
            }
            return status;
        }

        public int LikeTweet(int tweeetId)
        {
            int status = 1;
            try
            {
                db.tweets.Find(tweeetId).LikeCount++;
                db.SaveChanges();
                status = 0;
            }
            catch(Exception e)
            {
                status = 1;
            }
            return status;
        }

        public Tweet GetTweetByTweetId(int tweetId)
        {
            Tweet tweet = new Tweet();
            try
            {
                tweet = db.tweets.Find(tweetId);
            }
            catch (Exception e)
            {
                tweet = null;
            }
            return tweet;
        }
    }
}
