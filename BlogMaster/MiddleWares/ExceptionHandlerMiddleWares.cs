using System;
using System.Text.Json;
using System.Threading.Tasks;
using BlogMaster.Core.Exceptions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;

namespace Auto_Showroom.Middlewares
{
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
            catch (ArticleNotFoundException e)
            {
                httpContext.Response.StatusCode = 404;
                await httpContext.Response.WriteAsync(
                    JsonSerializer.Serialize(new { error = e.Message }));
                _logger.LogError(e, "Car not found");
            }
            catch (OrderNotFoundException e)
            {
                httpContext.Response.StatusCode = 404;
                await httpContext.Response.WriteAsync(
                    JsonSerializer.Serialize(new { error = e.Message }));
                _logger.LogError(e, "Order not found");
            }
            catch (Exception e)
            {
                await httpContext.Response.WriteAsync(
                    JsonSerializer.Serialize(new { error = "unexpected error"}));
                _logger.LogError(e.Message);
            }
        }
    }

    
}