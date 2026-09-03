using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Store.Services.Abstractions;
using System.Text;

namespace Store.Presentation.API.Attributes;

public class CasheAttribute(int timeInSec) : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var casheService = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().CasheService;

        var chsheKey = GetCasheKey(context.HttpContext.Request);

        var result = await casheService.GetAsync(chsheKey);
        if (!string.IsNullOrEmpty(result))
        {
            var response = new ContentResult()
            {
                Content = result,
                ContentType = "application/json",
                StatusCode = StatusCodes.Status200OK
            };
            context.Result = response;
            return;
        }
        var actionContext = await next.Invoke();
        if (actionContext.Result is OkObjectResult okObjectResult)
        {
            await casheService.SetAsync(chsheKey, okObjectResult, TimeSpan.FromSeconds(timeInSec));
        }
    }

    private string GetCasheKey(HttpRequest request)
    {
        var key = new StringBuilder();
        key.Append(request.Path);

        foreach (var item in request.Query)
        {
            key.Append($"|{item.Key}-{item.Value}");
        }
        return key.ToString();
    }
}
