using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TestWebAPIBranch.Models;

namespace TestWebAPIBranch.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("DefaultConnection") { }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
    }
}