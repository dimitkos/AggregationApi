using FluentValidation;
using Gateway;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace Api.Middlewares
{
    class ExceptionsMiddleware
    {
        private static readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings { ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() } };

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionsMiddleware> _logger;

        public ExceptionsMiddleware(
            RequestDelegate next,
            ILogger<ExceptionsMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                context.Request.EnableBuffering();
                await _next(context);
            }
            catch (ApiClientException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"Something went wrong. ExMessage: {ex.Message}");
                _logger.LogError(ex.Message);
            }
            catch (ValidationException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var response = JsonConvert.SerializeObject(ex.Errors, _serializerSettings);
                await context.Response.WriteAsync(response);
                _logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"Something went wrong.ExMessage: {ex.Message}");
                _logger.LogError(ex.Message);
            }
        }
    }
}
