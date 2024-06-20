using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Database
{
    internal static class SeedDataBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            SeedUserAccount(modelBuilder);
        }


        private static void SeedUserAccount(ModelBuilder modelBuilder)
        {
            var roles = new List<UserAccount>()
            {
                new UserAccount() { Id = 1, Username = "Admin", Password = "Admin" },
            };

            foreach (var item in roles)
            {
                modelBuilder.Entity<UserAccount>().HasData(item);
            }
        }
    }
}
