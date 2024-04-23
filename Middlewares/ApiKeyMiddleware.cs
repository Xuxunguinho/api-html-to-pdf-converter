
using apiConverterHtmlToPdf.Application.Constants;
using apiConverterHtmlToPdf.Application.Interfaces;

namespace apiConverterHtmlToPdf.Middlewares
{
    //http://codingsonata.com/secure-asp-net-core-web-api-using-api-key-authentication/
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
     
        private readonly IApiKeyValidationService _apiKeyValidationService;
        public ApiKeyMiddleware(RequestDelegate next, IApiKeyValidationService apikeyValidationService)
        {
            _next = next;
            _apiKeyValidationService = apikeyValidationService;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            var values = context.Request.Headers.Values;


            if (!context.Request.Headers.TryGetValue(Constants.APIKEY_NAME, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api Key was not provided");
                throw new Exception("Api Key was not provided");
            }

            var isValid = _apiKeyValidationService.IsValidApiKey(extractedApiKey);

            if (!isValid)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("You are Unauthorized");
                throw new Exception("You are Unauthorized");
              
            }



            await _next(context);
        }
    }


}
