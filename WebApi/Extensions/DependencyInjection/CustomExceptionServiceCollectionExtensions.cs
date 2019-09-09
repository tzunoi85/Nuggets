using Microsoft.AspNetCore.Builder;

using WebApi.ExceptionHandlers;


namespace WebApi.Extensions.DependencyInjection
{
    public static class CustomExceptionServiceCollectionExtensions
    {
        public static IApplicationBuilder UseAuthenticationExceptionHandlerMiddleware(this IApplicationBuilder builder)
           => builder.UseMiddleware<AuthenticationExceptionHandlerMiddleware>();

        public static IApplicationBuilder UseGeneralExceptionHandlerMiddleware(this IApplicationBuilder builder)
           => builder.UseMiddleware<GeneralExceptionHandlerMiddleware>();

        public static IApplicationBuilder UseValidationExceptionHandlerMiddleware(this IApplicationBuilder builder)
           => builder.UseMiddleware<FluentValidationExceptionHandlerMiddleware>();
    }
}
