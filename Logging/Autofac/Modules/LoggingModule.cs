using Serilog;
using System;

using ISerilog = Serilog.ILogger;
using ICommonLogger = Common.Loggers.ILogger;
using Microsoft.Extensions.Configuration;
using Autofac;

namespace Logging.Autofac.Modules
{
    public class LoggingModule
        : Module
    {
        public LoggingModule(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected IConfiguration Configuration { get; }

        protected override void Load(ContainerBuilder builder)
        {

            builder
               .RegisterType<Logger>()
               .As<ICommonLogger>()
               .InstancePerDependency();


            builder
                .Register<ISerilog>((c, p) =>
                    {
                        return new LoggerConfiguration()
                        .ReadFrom.Configuration(Configuration)
                        .CreateLogger();
                    })
                .SingleInstance();
        }
    }
}
