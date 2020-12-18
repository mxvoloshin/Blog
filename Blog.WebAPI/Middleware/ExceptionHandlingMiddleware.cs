using System;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Blog.WebAPI.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next,
                                           ILogger<ExceptionHandlingMiddleware> logger,
                                           IWebHostEnvironment hostingEnvironment)
        {
            _next = next;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        public virtual async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(context, exception);
            }
        }

        private Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            var response = GetResponsePayload(exception);
            var payload = JsonConvert.SerializeObject(response);

            context.Response.ContentType = "application/json";

            switch (exception)
            {
                case ValidationException _:
                    context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                    break;
                default:
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    _logger.LogError(exception, exception.Message);
                    break;
            }

            return context.Response.WriteAsync(payload);
        }

        private object GetResponsePayload(Exception exception)
        {
            if (_hostingEnvironment.IsDevelopment())
            {
                return new { exception.Message, exception.StackTrace };
            }

            return new { exception.Message };
        }
    }
}