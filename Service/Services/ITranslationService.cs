namespace StatisticsPoland.VtlProcessing.Service.Services
{
    using StatisticsPoland.VtlProcessing.Service.Models;

    public interface ITranslationService
    {
        TranslationResponse Tanslate(TranslationParameters parameters);
    }
}
