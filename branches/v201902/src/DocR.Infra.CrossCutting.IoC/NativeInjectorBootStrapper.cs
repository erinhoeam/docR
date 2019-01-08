using AutoMapper;
using DocR.Domain.Core.Notifications;
using DocR.Domain.Handlers;
using DocR.Domain.Interfaces;
using DocR.Infra.CrossCutting.Data.Context;
using DocR.Infra.CrossCutting.Data.UoW;
using DocR.Infra.CrossCutting.Identity.Models;
using DocR.Infra.CrossCutting.Identity.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace DocR.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASPNET
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(
                sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            //Domain - Commands
            //services.AddScoped<IRequestHandler<InserirProfessorCommand>, ProfessorCommandHandler>();

            //Domain - Eventos
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            //Infra - Data
            //services.AddScoped<IProfessorRepository, ProfessorRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<DataBaseContext>();

            //Infra - Identity
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddScoped<IUser, User>();

            // Infra - Filtros
            //services.AddScoped<ILogger<GlobalActionLogger>, Logger<GlobalActionLogger>>();
            //services.AddScoped<GlobalActionLogger>();
        }
    }
}
