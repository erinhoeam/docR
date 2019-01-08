using System;
using DocR.Service.Configurations;
using DocR.Service.Setting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DocR.Service
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Configurações de Autenticação, Autorização e JWT.
            services.AddMvcSecurity(Configuration);

            services.AddOptions();

            services.AddMvc(options =>
            {
                options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
                //options.Filters.Add(new ServiceFilterAttribute(typeof(GlobalActionLogger)));

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Versionamento do WebApi
            services.AddApiVersioning("api/v{version}");

            // AutoMapper
            //services.AddAutoMapper();

            // MediatR
            //services.AddMediatR(typeof(Startup));

            // Registrar todos os DI
            services.AddDIConfiguration();

            // Configurações do Swagger
            //services.AddSwaggerConfig();

            var settings = Configuration.GetSection(nameof(Settings));

            services.Configure<Settings>(options =>
            {
                options.UriBase = settings[nameof(Settings.UriBase)];
                options.SendGridApiKey = settings[nameof(Settings.SendGridApiKey)];
                options.SubscriptionKey = settings[nameof(Settings.SubscriptionKey)];
            });

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }

        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IHttpContextAccessor accessor)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();

            //app.UseSwaggerAuthorized();
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "CadastroProfessores API v1.0");
            });
        }
    }
}
