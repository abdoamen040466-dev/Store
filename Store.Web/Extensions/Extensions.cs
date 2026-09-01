using Microsoft.AspNetCore.Mvc;
using Store.Domain.Contracts;
using Store.Persistance;
using Store.Services;
using Store.Shared.ErrorModels;
using Store.Web.Middlewares;

namespace Store.Web.Extensions;

public static class Extensions
{
    public static IServiceCollection AddAllServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddWebServices();

        services.AddInfrastructureServices(configuration);

        services.AddApplicationServices(configuration);
        services.AddApplicationServices(configuration);
        services.AddConfigureApiBehaviorOptionsServices();


        return services;
    }

    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }

    public static IServiceCollection AddConfigureApiBehaviorOptionsServices(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(config =>
        {
            config.InvalidModelStateResponseFactory = (actionContext) =>
            {
                var errors = actionContext.ModelState.Where(m => m.Value.Errors.Any())
                                                     .Select(m => new ValidationError()
                                                     {
                                                         Field = m.Key,
                                                         Error = m.Value.Errors.Select(e => e.ErrorMessage)
                                                     }).ToList();



                var response = new ValidationErrorResponse()
                {
                    Errors = errors
                };
                return new BadRequestObjectResult(response);
            };
        });
        return services;
    }







    public static async Task<WebApplication> ConfigureMiddleWares(this WebApplication app)
    {
        app.SeedData();

        app.UseMiddleware<GlobalErrorHandlingMiddleware>();

        app.UseStaticFiles();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();


        return app;
    }

    private static async Task<WebApplication> SeedData(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var DbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        await DbInitializer.InitializeAsync();
        return app;
    }
}
