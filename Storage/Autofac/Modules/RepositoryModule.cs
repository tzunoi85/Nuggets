using Autofac;
using Common.Repositories;
using System;
using System.Linq;
using System.Reflection;
using Module = Autofac.Module;


namespace Storage.Autofac.Modules
{
    public class RepositoryModule
        : Module
    {
        protected Assembly[] Assemblies { get; }

        public RepositoryModule(params Assembly[] assemblies)
           => Assemblies = assemblies ?? throw new ArgumentNullException(nameof(assemblies));

        protected override void Load(ContainerBuilder builder)
        {

            builder
                .RegisterAssemblyTypes(Assemblies)
                .Where(t => !t.IsAbstract &&
                             t.GetInterfaces().Any(i => i.IsGenericType &&
                                                        i.GetGenericTypeDefinition() == typeof(IGenericRepository<>)))
                .As(t => t.GetInterfaces().First(i => !i.IsGenericType && i != typeof(IRepository)))
                .InstancePerDependency();
        }
    }
}
