using Autofac;
using MediatR;
using System.Linq;
using System.Reflection;
using Module = Autofac.Module;

namespace Cqs.Autofac.Modules
{
    public class CqsModule
        : Module
    {
        private Assembly[] Assemblies { get; }

        public CqsModule(params Assembly[] assemblies)
            => Assemblies = assemblies;

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(Assemblies)
                .Where(t => !t.IsAbstract && t.IsPublic)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder
                .RegisterAssemblyTypes(Assemblies)
                .Where(t => !t.IsAbstract && t.IsPublic)
                .AsClosedTypesOf(typeof(INotificationHandler<>));


            Assemblies.SelectMany(a => a.GetTypes()
                       .Where(t => !t.IsAbstract &&
                                    t.IsPublic &&
                                    t.GetInterfaces().Where(i => i.IsGenericType)
                                                      .Any(i => i.GetGenericTypeDefinition() == typeof(IPipelineBehavior<,>))))
                       .ToList()
                       .ForEach(t => builder.RegisterGeneric(t).As(typeof(IPipelineBehavior<,>)));

            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerDependency();

            builder
                .Register<ServiceFactory>(ctx =>
                {
                    var c = ctx.Resolve<IComponentContext>();
                    return t => c.Resolve(t);
                });
        }
    }
}
