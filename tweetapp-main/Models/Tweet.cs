using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tweetapp.Models
{
    public class Tweet
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int TweetId { get; set; }

        public int ChildId { get; set; } 

        [Required]
        public int UserId { get; set; }
        
        [ForeignKey("UserId")]
        public Users UserDetails { get; set; }

        [MaxLength(144)]
        public string TweetMessage { get; set; }

        public int LikeCount { get; set; }

        public DateTime Created_Datetime { get; set; }

        public Tweet()
        {

        }
        public Tweet(string message, Users users)
        {
            TweetMessage = message;
            Created_Datetime = DateTime.Now;
            UserDetails = users;
        }
    }
}
