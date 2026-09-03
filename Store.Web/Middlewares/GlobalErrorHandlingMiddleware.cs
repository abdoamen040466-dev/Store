using Store.Domain.Exceptions.BadRequest;
using Store.Domain.Exceptions.NotFound;
using Store.Shared.ErrorModels;

namespace Store.Web.Middlewares;

public class GlobalErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
            if (context.Response.StatusCode == 404)
            {
                throw new NotFoundException($"$endPoint {context.Request.Path} Was not found");
            }
        }
        catch (Exception ex)
        {
            // 1. Set status code of response

            context.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                BadRequestException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError

            };

            // 2. set content type of response
            context.Response.ContentType = "application/json";

            // 3. Set body of response
            var response = new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                ErrorMessage = ex.Message

            };

            await context.Response.WriteAsJsonAsync(response);

            // return response
        }
    }

}
