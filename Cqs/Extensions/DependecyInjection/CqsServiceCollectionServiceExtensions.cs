using Autofac;
using Cqs.Autofac.Modules;
using Cqs.Pipelines;
using MediatR;
using System.Reflection;


namespace Cqs.Extensions.DependecyInjection
{
    public static class CqsServiceCollectionServiceExtensions
    {
        public static void AddCqs(this ContainerBuilder containerBuilder, params Assembly[] assemblies)
            => containerBuilder.RegisterModule(new CqsModule(assemblies));

        /// <summary>
        /// Require Cqs (AddCqs) 
        /// </summary>
        public static void AddLoggingPipelineBehavior(this ContainerBuilder containerBuilder)
            => containerBuilder
                            .RegisterGeneric(typeof(LoggingPipelineBehavior<,>))
                            .As(typeof(IPipelineBehavior<,>));

        /// <summary>
        /// Require Cqs (AddCqs) 
        /// </summary>
        public static void AddQueryOptionsPipelineBehavior(this ContainerBuilder containerBuilder)
            => containerBuilder
                            .RegisterGeneric(typeof(QueryOptionsPipelineBehavior<,>))
                            .As(typeof(IPipelineBehavior<,>));

        /// <summary>
        /// Require Cqs (AddCqs) 
        /// </summary>
        public static void AddValidationPipelineBehavior(this ContainerBuilder containerBuilder)
           => containerBuilder
                           .RegisterGeneric(typeof(ValidationPipelineBehavior<,>))
                           .As(typeof(IPipelineBehavior<,>));
    }
}
