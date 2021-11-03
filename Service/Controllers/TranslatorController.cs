namespace StatisticsPoland.VtlProcessing.Service.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using StatisticsPoland.VtlProcessing.Service.Models;
    using StatisticsPoland.VtlProcessing.Service.Services;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    [ApiController]
    [Route("[controller]")]
    public class TranslatorController : ControllerBase
    {
        private readonly ILogger<TranslatorController> _logger;
        private readonly ITranslationService _translationService;

        public TranslatorController(ILogger<TranslatorController> logger, ITranslationService translationService)
        {
            this._logger = logger;
            this._translationService = translationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromBody][Required] TranslationParameters parameters, [FromHeader][Required] string accept)
        {
            TranslationResponse response = await Task.FromResult<TranslationResponse>(this._translationService.Tanslate(parameters));

            if (response.AreErrors)
                return BadRequest(error: response.Exceptions.Select(e => e.Message).ToArray());

            return accept switch
            {
                "text/plain" => File(GenerateStreamFromString(response.Result), "text/plain", "result.txt"),
                "application/json" => Ok(GenerateJsonFromString(response.Result)),
                _ => Ok(response.Result)
            };
        }

        private Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        private object GenerateJsonFromString(string s)
        {
            return new { Result = s };
        }
    }
}
