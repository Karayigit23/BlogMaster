using System.Text.Json;
using BlogMaster.Core.Exception;


namespace BlogMaster.MiddleWares;

public class ExceptionHandlerMiddleware
{
    
    private readonly RequestDelegate _requestDelegate;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;


    public ExceptionHandlerMiddleware(RequestDelegate requestDelegate,ILogger<ExceptionHandlerMiddleware>logger)
    {
        _requestDelegate = requestDelegate;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _requestDelegate(httpContext);
        }
        catch (NotFoundException e)
        {
            httpContext.Response.StatusCode = 404;
            await httpContext.Response.WriteAsync(
                JsonSerializer.Serialize(new { error = e.Message }));
            _logger.LogError(e, "not found");
        }
        catch (CountException e)
        {
            httpContext.Response.StatusCode = 404;
            await httpContext.Response.WriteAsync(
                JsonSerializer.Serialize(new { error = e.Message }));
            _logger.LogError(e, "not found");
        }
        catch (ControlException e)
        {
            httpContext.Response.StatusCode = 404;
            await httpContext.Response.WriteAsync(
                JsonSerializer.Serialize(new { error = e.Message }));
            _logger.LogError(e, "not found");
        }
        catch (Exception e)
        {
            await httpContext.Response.WriteAsync(
                JsonSerializer.Serialize(new { error = "unexpected error"}));
            _logger.LogError(e.Message);
        }
    }

}
