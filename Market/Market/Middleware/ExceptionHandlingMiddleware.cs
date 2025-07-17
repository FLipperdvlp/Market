namespace Market.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await next.Invoke(httpContext);
        }
        catch (Exception e)
        {
            httpContext.Response.StatusCode = 500;
            await httpContext.Response.WriteAsJsonAsync(new
            {
                error = e.Message,
            });
        }
    }
}

public static class ExceptionHandlingMiddlewareExtensions
{
    public static void UseCustomExceptionHandlingMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}