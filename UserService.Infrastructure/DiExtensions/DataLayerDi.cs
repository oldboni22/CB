using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Domain.Context;
using UserService.Infrastructure.EfInterceptors;

namespace UserService.Infrastructure.DiExtensions;

public static class DataLayerDi
{
    public static void AddDataLayerDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddTimeProvider()
            .AddUserServiceDbContext(configuration);
    }

    private static IServiceCollection AddTimeProvider(this IServiceCollection services)
    {
        return services
            .AddSingleton<TimeProvider>(TimeProvider.System);
    }

    private static IServiceCollection AddUserServiceDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("UserDb");
        
        return services
            .AddScoped<TimeStampsInterceptor>()
            .AddDbContext<UserServiceDbContext>((sp, options) =>
            {
                options
                    .UseNpgsql(connectionString)
                    .AddInterceptors(sp.GetRequiredService<TimeStampsInterceptor>());


            });
    }
}
