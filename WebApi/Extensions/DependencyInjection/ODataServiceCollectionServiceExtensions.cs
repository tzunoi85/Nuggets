using Autofac;
using Microsoft.Extensions.Configuration;

using WebApi.Autofac.Modules;


namespace WebApi.Extensions.DependencyInjection
{
    public static class ODataServiceCollectionServiceExtensions
    {
        public static void AddQueryOptions(this ContainerBuilder containerBuilder, IConfiguration configuration)
           => containerBuilder.RegisterModule(new QueryOptionsModule(configuration));
    }
}
