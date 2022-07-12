using System;
using System.Collections.Generic;
using System.Text;

namespace TweetConsoleApp.Models
{
    class Users
    {
        public int UserId { get; set; }

        
        public string F_Name { get; set; }

       
        public string L_Name { get; set; }

       
        public string Email { get; set; }

        
        public string Password { get; set; }

        public string Contact_No { get; set; }

        public bool isLoggedin { get; set; }
    }
}
