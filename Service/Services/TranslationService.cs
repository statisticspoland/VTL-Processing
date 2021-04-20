using Microsoft.Extensions.Logging;
using StatisticsPoland.VtlProcessing.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatisticsPoland.VtlProcessing.Service.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly ILogger<TranslationService> _logger;
        public TranslationService(ILogger<TranslationService> logger)
        {
            _logger = logger;
        }
        public Response Tanslate(TranslationParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
