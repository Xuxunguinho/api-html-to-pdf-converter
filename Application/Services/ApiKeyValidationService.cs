using apiConverterHtmlToPdf.Application.Interfaces;

namespace apiConverterHtmlToPdf.Application.Services
{
    public class ApiKeyValidationService : IApiKeyValidationService
    {
        private readonly IConfiguration _configuration;
        public ApiKeyValidationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool IsValidApiKey(string userApiKey)
        {
            if (string.IsNullOrWhiteSpace(userApiKey))
                return false;

            string? apiKey = _configuration.GetValue<string>(Constants.Constants.APIKEY_NAME);

            if (apiKey == null || apiKey != userApiKey)
                return false;


            return true;
        }
    }
}
