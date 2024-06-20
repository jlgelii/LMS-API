using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(SetupConfiguration)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


        private static void SetupConfiguration(HostBuilderContext context, IConfigurationBuilder builder)
        {
            builder.Sources.Clear();

            var config = builder.AddJsonFile("appsettings.json")
                                .Build();
            var env = config.GetValue("Env", "string");

            builder.AddJsonFile("appsettings.json", false, true)
                   .AddJsonFile($"appsettings.{env}.json", false, true)
                   .AddEnvironmentVariables();
        }
    }
}
