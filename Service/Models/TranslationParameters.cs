using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatisticsPoland.VtlProcessing.Service.Models
{
    public class TranslationParameters
    {
        public Dictionary<string, string> DataSources { get; set; }
        public Dictionary<string, string> DataMappers { get; set; }
        public string Target { get; set; }
        public string Experession { get; set; }
    }
}
