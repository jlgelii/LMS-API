using CleanArchitecture.Application.Configurations.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Configurations
{
    public class ConnectionsConfigurations : IConnectionsConfigurations
    {
        private readonly IConfiguration _configuration;

        public ConnectionsConfigurations(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public string GetConnectionString()
        {
            return _configuration.GetConnectionString("DefaultConnection");
        }
    }
}
