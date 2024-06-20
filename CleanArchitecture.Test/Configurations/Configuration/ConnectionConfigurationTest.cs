using CleanArchitecture.Application.Configurations.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Test.Configurations.Services
{
    public class ConnectionConfigurationTest : IConnectionsConfigurations
    {
        public string GetConnectionString()
        {
            return String.Empty;
        }
    }
}
