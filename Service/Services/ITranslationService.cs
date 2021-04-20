using StatisticsPoland.VtlProcessing.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatisticsPoland.VtlProcessing.Service.Services
{
    public interface ITranslationService
    {
        Response Tanslate(TranslationParameters parameters);
    }
}
