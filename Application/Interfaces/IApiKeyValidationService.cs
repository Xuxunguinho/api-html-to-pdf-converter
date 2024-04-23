namespace apiConverterHtmlToPdf.Application.Interfaces
{
    public interface IApiKeyValidationService
    {
        bool IsValidApiKey(string userApiKey);
    }
}
