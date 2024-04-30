using apiConverterHtmlToPdf.Application.DTOS;
using apiConverterHtmlToPdf.Application.Interfaces;

namespace apiConverterHtmlToPdf.Application.Services
{
    public class ConverterService : IConverterService
    {

        private readonly ILogger<ConverterService> _logger;

        public ConverterService(ILogger<ConverterService> logger)
        {
            _logger = logger;
        }



        public async Task<byte[]> ConvetToPdfAsync(string htmBody, PageSettingDto pageSetting)
        {
            try
            {
                using var converter = new ChromiumHtmlToPdfLib.Converter();

                var fileName = GenerateFileName();

                var timeout = (int)TimeSpan.FromSeconds(20).TotalMilliseconds;

                var setting = BuildPageSettings(pageSetting);

                converter.AddChromiumArgument("--no-sandbox");
                converter.AddChromiumArgument("--disable-dev-shm-usage");

                await converter.ConvertToPdfAsync(htmBody, fileName, setting, null, timeout, timeout, timeout, _logger);

                var bytes = await File.ReadAllBytesAsync(fileName);

                File.Delete(fileName);

                return bytes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Array.Empty<byte>();
            }

        }

        string GenerateFileName() => $"Generated_{Guid.NewGuid().ToString().Replace("-", string.Empty)}.pdf";

        private ChromiumHtmlToPdfLib.Settings.PageSettings BuildPageSettings(PageSettingDto dto)
        {

            var setting = new ChromiumHtmlToPdfLib.Settings.PageSettings();

            setting.Landscape = dto.Landscape;

            if (!dto.Height.HasValue || !dto.Width.HasValue)
                if (dto.PaperFormat.HasValue)
                    setting.SetPaperFormat(dto.PaperFormat.Value);

            //var pattern = 0.393701;
            var pattern = 1;

            if (dto.Width.HasValue)
                setting.PaperWidth = dto.Width.Value * pattern;

            if (dto.Height.HasValue)
                setting.PaperHeight = dto.Height.Value * pattern;

            if (dto.MarginTop.HasValue)
                setting.MarginTop = dto.MarginTop.Value * pattern;

            if (dto.MarginLeft.HasValue)
                setting.MarginLeft = dto.MarginLeft.Value * pattern;

            if (dto.MarginBottom.HasValue)
                setting.MarginBottom = dto.MarginBottom.Value * pattern;

            if (dto.MarginRight.HasValue)
                setting.MarginRight = dto.MarginRight.Value * pattern;


            return setting;
        }

    }
}
