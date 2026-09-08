using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Store.Domain.Contracts;
using Store.Domain.Entities.Identity;
using Store.Persistance.Data.Contexts;
using Store.Persistance.Identity.Contexts;
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
        services.AddDbContext<IdentityStoreDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
        });
        services.AddScoped<IDbInitializer, DbInitializer>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddScoped<ICasheRepository, CasheRepository>();
        services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
        ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection"))
        );

        services.AddIdentityCore<AppUser>(options =>
        {
            options.User.RequireUniqueEmail = true;
        })
           .AddRoles<IdentityRole>()
           .AddEntityFrameworkStores<IdentityStoreDbContext>();
        return services;
    }
}
