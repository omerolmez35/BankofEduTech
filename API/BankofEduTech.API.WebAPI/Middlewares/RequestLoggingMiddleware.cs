using Serilog;

namespace BankofEduTech.API.WebAPI.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
    
            Log.Information("HTTP {Method} {Path} isteği alındı.", context.Request.Method, context.Request.Path);

            await _next(context);  


            Log.Information("HTTP {Method} {Path} isteği için yanıt {StatusCode}",
                            context.Request.Method,
                            context.Request.Path,
                            context.Response.StatusCode);
        }
    }
}
