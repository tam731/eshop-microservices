using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        //services.AddDbContext<OrderingDbContext>(options =>
        //{
        //    options.UseSqlServer(
        //        configuration.GetConnectionString("OrderingConnectionString"),
        //        sqlOptions => sqlOptions.MigrationsAssembly(typeof(OrderingDbContext).Assembly.FullName));
        //});
        return services;
    }
}
