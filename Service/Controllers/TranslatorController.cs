using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StatisticsPoland.VtlProcessing.Service.Models;
using StatisticsPoland.VtlProcessing.Service.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StatisticsPoland.VtlProcessing.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TranslatorController : ControllerBase
    {
        private readonly ILogger<TranslatorController> _logger;
        private readonly ITranslationService _translationService;

        public TranslatorController(ILogger<TranslatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromBody][Required] TranslationParameters parameters)
        {


            return Ok(string.Empty);
        }
    }
}
