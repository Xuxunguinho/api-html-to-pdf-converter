using apiConverterHtmlToPdf.Application.DTOS;

namespace apiConverterHtmlToPdf.Application.Interfaces
{
    public interface IConverterService
    {
        Task<byte[]> ConvetToPdfAsync(string htmBody, PageSettingDto pageSetting);
    }
}