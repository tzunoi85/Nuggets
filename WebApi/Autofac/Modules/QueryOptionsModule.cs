using Autofac;
using Common.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.Extensions.Configuration;
using System;
using Module = Autofac.Module;

using WebApi.OData;


namespace WebApi.Autofac.Modules
{
    public class QueryOptionsModule
        : Module
    {
        protected IConfiguration Configuration { get; }


        public QueryOptionsModule(IConfiguration configuration)
           => Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));


        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType(typeof(QueryOptions))
                .As(typeof(IQueryOptions))
                .InstancePerDependency();

            builder
                .Register<ODataValidationSettings>((c, p) =>
                    {
                        return new ODataValidationSettings()
                        {
                            AllowedFunctions = AllowedFunctions.AllFunctions,
                            AllowedArithmeticOperators = AllowedArithmeticOperators.All,
                            AllowedLogicalOperators = AllowedLogicalOperators.All,
                            AllowedQueryOptions = AllowedQueryOptions.All,
                            MaxTop = 10
                        };
                    })
                .SingleInstance();
        }
    }
}
