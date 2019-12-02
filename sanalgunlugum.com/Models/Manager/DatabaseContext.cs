using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace sanalgunlugum.com.Models.Manager
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Daily> Dailys { get; set; }
        public DbSet<Files> Files{ get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DatabaseContext()
        {

        }
    }
}