using Common.OData;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.OData.Edm;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


namespace WebApi.OData
{
    public class QueryOptions
        : IQueryOptions
    {
        private ODataQueryOptions ODataOptionsInstance { get; set; }

        private ODataQueryOptions ODataOptions
            => ODataOptionsInstance ?? (ODataOptionsInstance = new ODataQueryOptions(ODataContext, ContextAccessor?.HttpContext.Request));

        private ODataQueryContext ODataContext => new ODataQueryContext(GetEdmModel(), T, null);

        private IHttpContextAccessor ContextAccessor { get; }

        private static Type T { get; set; }

        private ODataValidationSettings ODataValidationSettings { get; }

        public QueryOptions(IHttpContextAccessor contextAccessor, ODataValidationSettings validationSettings)
        {
            ContextAccessor = contextAccessor ?? throw new ArgumentNullException(contextAccessor.GetType().ToString());
            ODataValidationSettings = validationSettings ?? throw new ArgumentNullException(validationSettings.GetType().ToString());
        }

        public async Task<IQueryable> ApplyQuery(IQueryable collection)
        {
            T = collection.GetType().GetGenericArguments()[0];
            ODataOptions.Validate(ODataValidationSettings);

            return await Task.Run(() => ODataOptions.ApplyTo(collection.AsQueryable()));
        }

        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();

            MethodInfo method = typeof(ODataConventionModelBuilder).GetMethod(nameof(builder.EntitySet));
            MethodInfo generic = method.MakeGenericMethod(new[] { T });
            generic.Invoke(builder, new[] { nameof(T) });

            return builder.GetEdmModel();
        }
    }
}
