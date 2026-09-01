using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Services.Abstractions;
using Store.Services.Mapping.Basket;
using Store.Services.Mapping.Products;

namespace Store.Services;

public static class ApplicationServicesRegisterations
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(c => c.AddProfile(new ProductProfile(configuration)));
        services.AddAutoMapper(c => c.AddProfile(new BasketProfile()));
        services.AddScoped<IServiceManager, ServiceManager>();

        return services;
    }
}
