using DDNet.Application.Entities;
using DDNet.Application.Interfaces;
using DDNet.Application.Services;
using DDNet.Infrastructure.MailgunEmailProvider;
using DDNet.Infrastructure.MySqlServer;
using DDNet.Infrastructure.MySqlServer.Repositories;
using DDNet.Infrastructure.SqlServer;
using FluentEmail.Core;
using FluentEmail.Mailgun;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using NodaTime;
using NodaTime.Serialization.JsonNet;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace DDNet.Client.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<LoginService>();
            services.AddTransient<IAuthDispatcher, MailgunApiClient>();
            services.AddTransient<IPinRepository, PinManager>();
            services.AddTransient<IUserRepository, UserManager>();
            services.AddSingleton<IClock>(x => SystemClock.Instance);

            services.Configure<SecurityConfig>(Configuration.GetSection("PasswordSecurity"));
            services.Configure<MailGunConfig>(Configuration.GetSection("MailGunConfig"));
          
            services.AddDbContext<DdNetDbContext>(options => options.UseMySql(Configuration.GetConnectionString("DdNetDb"), 
                new MySqlServerVersion(new Version(8, 0, 23)), mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend)));

            services.AddControllers();

            var basicSettings = new JsonSerializerSettings()
            {
                DateParseHandling = DateParseHandling.None,
                NullValueHandling = NullValueHandling.Ignore
            };
            basicSettings.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);

            JsonConvert.DefaultSettings = () => basicSettings;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DDNet", Version = "v1" });
            });

            services.AddMvc();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DDNet v1"));
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
