using EntityFrameworkCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides extension methods for registering data services.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddData(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options
                //.UseLazyLoadingProxies()
                .UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")
                        ?? throw new InvalidOperationException("Connection string not found!"))
                .EnableSensitiveDataLogging(true)
            );

        return services;
    }
}