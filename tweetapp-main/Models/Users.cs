using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace tweetapp.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class Users
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(12)]
        public string F_Name { get; set; }
        
        [DataType(DataType.Text)]
        [MaxLength(12)]
        public string L_Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        public string Contact_No { get; set; }

        public bool isLoggedin { get; set; }
    }
}
