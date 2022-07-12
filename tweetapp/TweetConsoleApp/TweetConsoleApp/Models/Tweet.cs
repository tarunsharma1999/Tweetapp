using System;
using System.Collections.Generic;
using System.Text;

namespace TweetConsoleApp.Models
{
    class Tweet
    {

        public string TweetMessage { get; set; }

        public int TweetId { get; set; }

        public int ChildId { get; set; }

        public int LikeCount { get; set; }

        public DateTime Created_Datetime { get; set; }

        public string userName { get; set; }
    }
}
