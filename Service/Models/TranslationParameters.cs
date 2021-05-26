namespace StatisticsPoland.VtlProcessing.Service.Models
{
    using System.Collections.Generic;

    public class TranslationParameters
    {
        public List<DataSource> DataSources { get; set; }
        public Dictionary<string, string> DataMappers { get; set; }
        public string Target { get; set; }
        public string Experession { get; set; }
        public string DefaultNamespace { get; set; }
    }
}
