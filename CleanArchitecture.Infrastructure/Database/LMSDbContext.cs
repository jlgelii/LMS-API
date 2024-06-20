using CleanArchitecture.Application.Configurations.Database;
using CleanArchitecture.Application.Configurations.Services;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Database
{
    public class LMSDbContext : DbContext, ILMSDbContext
    {
        private readonly string _databaseName;
        private readonly IJwtServices _jwtServices;
        private readonly IDateTimeService _dateTimeService;

        #region Constructor
        public LMSDbContext(IConnectionsConfigurations connections,
            IDateTimeService dateTimeService,
            IJwtServices jwtServices)
        {
            _databaseName = connections.GetConnectionString();
            _dateTimeService = dateTimeService;
            _jwtServices = jwtServices;
        }

        public LMSDbContext(DbContextOptions options,
            IDateTimeService dateTimeService,
            IJwtServices jwtServices) : base(options)
        {
            this._dateTimeService = dateTimeService;
            _jwtServices = jwtServices;
        }
        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>()
                .HasQueryFilter(u => !u.Deleted);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }


        public DbSet<UserAccount> UserAccount { get; set; }


        #region Private override
        void ILMSDbContext.SaveChanges()
        {
            SaveChanges();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(_databaseName))
                optionsBuilder.UseSqlServer(_databaseName);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntities>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _jwtServices.GetLoggedUser().UserId;
                        entry.Entity.CreatedDate = _dateTimeService.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = _jwtServices.GetLoggedUser().UserId;
                        entry.Entity.UpdatedDate = _dateTimeService.Now;
                        break;
                }
            }

            return base.SaveChanges();
        }
        #endregion
    }
}
