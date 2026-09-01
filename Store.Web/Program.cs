using Store.Web.Extensions;


namespace Store.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);



        builder.Services.AddAllServices(builder.Configuration);



        var app = builder.Build();

        await app.ConfigureMiddleWares();

        app.Run();
    }
}
