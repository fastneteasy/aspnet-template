using AspNetTemplate.WebAPI.Models;
using AspNetTemplate.Extension;
using Newtonsoft.Json;
using System.Net;
using System.Security.Authentication;

namespace AspNetTemplate.WebAPI
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger logger;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IConfiguration configuration;

        public ExceptionHandler(RequestDelegate next, ILoggerFactory loggerFactory,
            IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _next = next;
            this.logger = loggerFactory.CreateLogger(GetType().FullName);
            this.webHostEnvironment = webHostEnvironment;
            this.configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            Func<string, HttpStatusCode, string, Task> func = async (exceptionCode, httpStatusCode, message) =>
            {
                logger.LogError(exception, $"Global Exception Handling :{exceptionCode}");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)httpStatusCode;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new MessageModel
                {
                    Code = exceptionCode,
                    Message = message
                }));
            };

            switch (exception)
            {
                case BusinessException _:
                    await func("BUSINESS_EXCEPTION", HttpStatusCode.BadRequest, exception?.Message);
                    break;
                case ArgumentNullException _:
                case ArgumentException _:
                    await func("ARGUMENT_EXCEPTION", HttpStatusCode.BadRequest, exception?.Message);
                    break;
                case AuthenticationException _:
                    await func("AUTHENTICATION_EXCEPTION", HttpStatusCode.Unauthorized, exception?.Message);
                    break;
                default:
                    if (webHostEnvironment.IsDevelopment() || webHostEnvironment.IsStaging())
                    {
                        await func("SYSTEM_EXCEPTION", HttpStatusCode.BadRequest, exception?.Message);
                    }
                    else
                    {
                        await func("SYSTEM_EXCEPTION", HttpStatusCode.BadRequest, "Error");
                    }

                    break;
            }
        }
    }
}