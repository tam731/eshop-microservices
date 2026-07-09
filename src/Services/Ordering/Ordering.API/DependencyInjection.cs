namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        //services.AddCarter();
        //services.AddHealthChecks();
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        //app.UseCarter();

        return app;
    }
}
