using CleanArchitecture.Application.Configurations.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Services
{
    public class DateTimeService : IDateTimeService

    {
        public DateTime Now => DateTime.Now;
    }
}
