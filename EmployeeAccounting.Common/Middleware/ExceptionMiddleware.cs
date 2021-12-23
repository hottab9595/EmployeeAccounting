using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using EmployeeAccounting.Common.Errors;
using EmployeeAccounting.Common.Exceptions;

namespace EmployeeAccounting.Services.Core
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
            catch (Exception ex)
            {
                if (ex is BaseException baseException)
                {
                    await HandleExceptionAsync(httpContext, baseException);
                }
                else
                {
                    await HandleExceptionAsync(httpContext, ex, HttpStatusCode.InternalServerError);
                }
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode code)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal server exception",
            }.ToString());
        }

        private static Task HandleExceptionAsync(HttpContext context, BaseException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)exception.HttpStatusCode;

            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message,
            }.ToString());
        }
    }
}