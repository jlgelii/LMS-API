using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.API.Configurations.Services
{
    public static class CorsServiceExtensions
    {
        public static void AddCorsAll(this IServiceCollection services)
        {
            services.AddCors(c =>
            {
                c.AddPolicy("CorsPolicy", options => options.AllowAnyOrigin()
                                                             .AllowAnyMethod()
                                                             .AllowAnyHeader());
            });
        }
    }
}
