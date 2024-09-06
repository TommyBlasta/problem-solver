
using FluentValidation;
using System.Net;

namespace ProblemSolver.ErrorHandling
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch
            (Exception ex)
            {
                await EvaluateException(context, ex);
            }
        }

        private async Task EvaluateException(HttpContext context, Exception ex)
        {
            Type type = ex.GetType();

            switch (type)
            {
                case Type t when t == typeof(ValidationException):
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await context.Response.WriteAsJsonAsync(new { ex.Message });
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await context.Response.WriteAsJsonAsync(new { ex.Message });
                    break;

            }
        }
    }
}
