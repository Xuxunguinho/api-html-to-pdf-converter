using apiConverterHtmlToPdf.Application.DTOS;
using apiConverterHtmlToPdf.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace apiConverterHtmlToPdf.Controllers.v1
{

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public partial class ConverterController : ControllerBase
    {


        private readonly ILogger<ConverterController> _logger;
        private readonly IConverterService _converter;
        public ConverterController(ILogger<ConverterController> logger, IConverterService converter)
        {
            _logger = logger;
            _converter = converter;
        }
        /// <summary>
        /// Route to convert any html to pdf
        /// </summary>
        /// <param name="body"></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        [HttpPost("html-to-pdf")]
        [ProducesResponseType(typeof(IFormFile), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> post([FromBody] string body, [FromQuery] PageSettingDto setting)
        {
            var filename = DateTime.Now.ToString("yyyyMMddHHmmss");
            return File(await _converter.ConvetToPdfAsync(body, setting), "application/pdf", $"{filename}.pdf");
        }



    }
}