using CleanArchitecture.Application.Configurations.Services;
using CleanArchitecture.Infrastructure.Database;
using CleanArchitecture.Test.Configurations.Database;
using CleanArchitecture.Test.Configurations.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Test
{

    public class BaseTest : IDisposable
    {
        public readonly LMSDbContext _context;
        public readonly IJwtServices _jwtServices;
        public readonly IDateTimeService _dateTimeService;


        public BaseTest()
        {
            _jwtServices = new JwtServiceTest();
            _dateTimeService = new DateTimeServiceTest();

            var option = new DbContextOptionsBuilder<LMSDbContext>()
                                                   .UseInMemoryDatabase(databaseName: "SampleDb")
                                                   .UseInternalServiceProvider(new ServiceCollection().AddEntityFrameworkInMemoryDatabase()
                                                                                                      .BuildServiceProvider())
                                                   .Options;

            _context = new LMSDbContext(option, _dateTimeService, _jwtServices);
            SeedTestData.Seed(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
