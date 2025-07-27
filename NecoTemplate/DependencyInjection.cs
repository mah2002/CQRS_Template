using Swashbuckle.AspNetCore.SwaggerGen;
using NecoTemplate.API.Middleware;

namespace NecoTemplate.API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddSwaggerGen();
        services.AddHttpContextAccessor();
        return services;
    }
    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }

    
}
