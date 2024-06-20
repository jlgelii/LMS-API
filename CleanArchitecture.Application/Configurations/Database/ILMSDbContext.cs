using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Configurations.Database
{
    public interface ILMSDbContext
    {
        DbSet<UserAccount> UserAccount { get; set; }


        void SaveChanges();
    }
}
