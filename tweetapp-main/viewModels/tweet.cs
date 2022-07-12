using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tweetapp.viewModels
{
    public class tweet
    {
        [MaxLength(144)]
        public string TweetMessage { get; set; }

        public int TweetId { get; set; }

        public int ChildId { get; set; }

        public int LikeCount { get; set; }

        public DateTime Created_Datetime { get; set; }

        public string UserName { get; set; }

        public tweet()
        {

        }
        public tweet(string TweetMessage,int TweetId, int ChildId, int LikeCount, DateTime Created_DateTime)
        {
            this.TweetId = TweetId;
            this.TweetMessage = TweetMessage;
            this.ChildId = ChildId;
            this.LikeCount = LikeCount;
            this.Created_Datetime = Created_DateTime;
        }
    }
}
