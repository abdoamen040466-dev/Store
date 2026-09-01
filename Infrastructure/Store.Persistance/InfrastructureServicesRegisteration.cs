using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Store.Domain.Contracts;
using Store.Persistance.Data.Contexts;
using Store.Persistance.Repositories;

namespace Store.Persistance;

public static class InfrastructureServicesRegisteration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StoreDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddScoped<IDbInitializer, DbInitializer>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
        ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection"))
        );

        return services;
    }
}
