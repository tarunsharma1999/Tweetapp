using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TweetConsoleApp.Models;

namespace TweetConsoleApp.DAO
{
    class TweetService
    {
        string URL = "https://localhost:44322/api/v1/tweets/";
        public async Task<int> AddTweet(Tweet tweet, string userName)
        {
            int status = -1;
            if (tweet != null)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        var reqObj = JsonConvert.SerializeObject(tweet);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(reqObj);
                        var bufferStream = new ByteArrayContent(buffer);
                        bufferStream.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        URL = URL + userName + "/Add";
                        var Response = await client.PostAsync(URL, bufferStream);

                        var res = await Response.Content.ReadAsStringAsync();

                        try
                        {
                            Tweet result = JsonConvert.DeserializeObject<Tweet>(res);
                            if (result.TweetId != 0 || result != null)
                            {
                                status = 0;
                            }
                        }
                        catch(Exception e)
                        {
                            string result = JsonConvert.DeserializeObject<string>(res);
                            if (result == "User not found")
                            {
                                status = -1;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    status =1 ;
                }
            }

            return status;
        }

        public async Task<List<Tweet>> GetAllTweetSelf(string userName)
        {
            List<Tweet> allTweets = new List<Tweet>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    URL = URL + userName + "/all";
                    var Response = await client.GetAsync(URL);
                    var res = await Response.Content.ReadAsStringAsync();
                    allTweets = JsonConvert.DeserializeObject<List<Tweet>>(res);

                }
            }   
            catch(Exception e)
            {
                allTweets = new List<Tweet>();
            }
            return allTweets;
        }

        public async Task<List<Tweet>> GetAllTweets()
        {
            List<Tweet> alltweeets = new List<Tweet>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    URL = URL + "all";
                    var Response = await client.GetAsync(URL);
                    var result = await Response.Content.ReadAsStringAsync();

                    alltweeets = JsonConvert.DeserializeObject<List<Tweet>>(result);
                }
            }
            catch (Exception e)
            {
                alltweeets = new List<Tweet>();
            }
            return alltweeets;
        }
    }
}
