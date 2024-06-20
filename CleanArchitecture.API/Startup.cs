using CleanArchitecture.API.Configurations.Extensions;
using CleanArchitecture.API.Configurations.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using MediatR;
using CleanArchitecture.Application;
using CleanArchitecture.Application.Configurations.Validation;
using CleanArchitecture.Infrastructure.Configurations;
using CleanArchitecture.Application.Configurations.Services;
using CleanArchitecture.Infrastructure.Services;
using CleanArchitecture.Infrastructure.Database;
using CleanArchitecture.Application.Configurations.Database;

namespace CleanArchitecture.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddJwt();
            services.AddSwagger();
            services.AddValidators();
            services.AddCorsAll();

            services.AddHttpContextAccessor();
            services.AddMediatR(typeof(EntryPoint).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddSingleton<IConnectionsConfigurations, ConnectionsConfigurations>();
            services.AddSingleton<IJwtServices, JwtService>();
            services.AddSingleton<IDateTimeService, DateTimeService>();
            services.AddScoped<ILMSDbContext, LMSDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            app.ConfigureSwaggerHandler();
            app.ConfigureExceptionHandler(Convert.ToBoolean(Configuration["Logging:IsShowException"]));

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
