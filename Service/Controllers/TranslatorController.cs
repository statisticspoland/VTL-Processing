namespace StatisticsPoland.VtlProcessing.Service.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json.Linq;
    using StatisticsPoland.VtlProcessing.Service.Models;
    using StatisticsPoland.VtlProcessing.Service.Services;

    [ApiController]
    [Route("[controller]")]
    public class TranslatorController : ControllerBase
    {
        private readonly ILogger<TranslatorController> _logger;
        private readonly ITranslationService _translationService;

        public TranslatorController(ILogger<TranslatorController> logger, ITranslationService translationService)
        {
            _logger = logger;
            _translationService = translationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromBody][Required] TranslationParameters parameters, [FromHeader][Required] string accept)
        {
            TranslationResponse response = await Task.FromResult<TranslationResponse>(_translationService.Tanslate(parameters));

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

        private JObject GenerateJsonFromString(string s)
        {
            dynamic jsonObject = new JObject();
            jsonObject.result = s;
            return jsonObject;
        }
    }
}
