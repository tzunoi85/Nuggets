using Autofac;
using Logging.Autofac.Modules;
using Microsoft.Extensions.Configuration;


namespace Logging.Extensions.DependecyInjection
{
    public static class LoggingServiceCollectionExtensions
    {
        public static void AddLogger(this ContainerBuilder containerBuilder, IConfiguration configuration)
            => containerBuilder.RegisterModule(new LoggingModule(configuration));
    }
}
