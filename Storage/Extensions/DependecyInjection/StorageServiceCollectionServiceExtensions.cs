using Autofac;
using Storage.Autofac.Modules;
using System.Reflection;

namespace Storage.Extensions.DependecyInjection
{
    public static class StorageServiceCollectionServiceExtensions
    {
        public static void AddGenericRepository(this ContainerBuilder containerBuilder, params Assembly[] assemblies)
            => containerBuilder.RegisterModule(new RepositoryModule(assemblies));

        /// <summary>
        /// Require Generic repository (AddGenericRepository) 
        /// </summary>
        public static void AddUnitOfWork(this ContainerBuilder containerBuilder)
            => containerBuilder.RegisterModule(new UnitOfWorkModule());

    }
}
