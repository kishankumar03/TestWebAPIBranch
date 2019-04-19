using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TestWebAPIBranch.Models;

namespace TestWebAPIBranch.Context
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            base.Seed(context);
            var user = new UserLogin { Id = 1, password = "test@123", user_name = "test" };

            var userdetail = new UserInfo { firstName= "Test First Name",lastName="LastName" , gender="Male",
            userId =1, UserLogin = user};
            user.UserInfo = userdetail;
            context.UserInfos.Add(userdetail);
            context.SaveChanges();
        }

    }
}