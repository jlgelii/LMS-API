using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Test.Configurations.Database
{
    public class SeedTestData
    {
        public static void Seed(SampleDbContext context)
        {
            SeedUserAccount(context);
        }

        public static void UnSeedData(SampleDbContext context)
        {
            if (context.UserAccount.Count() != 0)
                context.UserAccount.RemoveRange(context.UserAccount.ToList());

            context.SaveChanges();
        }




        private static void SeedUserAccount(SampleDbContext context)
        {
            var users = new List<UserAccount>()
            {
                new UserAccount() { Id = 1, Username = "User1", Password = "Password1" },
                new UserAccount() { Id = 2, Username = "User2", Password = "Password1" },
                new UserAccount() { Id = 3, Username = "User3", Password = "Password1" },
                new UserAccount() { Id = 4, Username = "User4", Password = "Password1" },
                new UserAccount() { Id = 5, Username = "User5", Password = "Password1" },
            };

            if (context.UserAccount.Count() == 0)
                context.AddRange(users);

            context.SaveChanges();
        }
    }
}
