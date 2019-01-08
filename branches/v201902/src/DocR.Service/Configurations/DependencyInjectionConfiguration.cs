using DocR.Infra.CrossCutting.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace DocR.Service.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDIConfiguration(this IServiceCollection services)
        {
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}