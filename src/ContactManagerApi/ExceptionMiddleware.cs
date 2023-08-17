using FluentValidation;
using System.Linq.Expressions;
using System.Net.Mime;

namespace ContactManagerTest
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }   

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                httpContext.Response.StatusCode = ex switch
                {
                    ValidationException valEx => StatusCodes.Status422UnprocessableEntity,
                    ApplicationException appEx => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                };

                await httpContext.Response.WriteAsJsonAsync(new
                {
                    Status = "ERROR",
                    Details = ex.Message
                });
            }
        }
    }
}
