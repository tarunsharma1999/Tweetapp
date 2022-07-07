using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tweetapp.Models;

namespace tweetapp.DB_Context
{
    public class DB_Context:DbContext
    {
        public DB_Context(DbContextOptions<DB_Context> op):base(op)
        {
        }
        public DbSet<Users> users { get; set; }
        public DbSet<Tweet> tweets { get; set; }
    }
}
