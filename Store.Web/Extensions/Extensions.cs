using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Store.Domain.Contracts;
using Store.Persistance;
using Store.Services;
using Store.Shared;
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
        services.AddConfigureApiBehaviorOptionsServices();

        services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

        services.AddAuthenticationService(configuration);

        return services;
    }

    private static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "Bearer";
            options.DefaultChallengeScheme = "Bearer";
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = jwtOptions.Issure,
                ValidateAudience = true,
                ValidAudience = jwtOptions.Audience,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
            };
        });
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

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();


        return app;
    }

    private static async Task<WebApplication> SeedData(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var DbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        await DbInitializer.InitializeAsync();
        await DbInitializer.InitializeIdentityAsync();
        return app;
    }
}
