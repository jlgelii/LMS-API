using CleanArchitecture.Application.Configurations.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Test.Configurations.Services
{
    public class DateTimeServiceTest : IDateTimeService
    {
        public DateTime Now => new DateTime(2022, 1, 31);
    }
}
