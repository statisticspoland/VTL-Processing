namespace StatisticsPoland.VtlProcessing.Service.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The parameters of a translation.
    /// </summary>
    public class TranslationParameters
    {
        /// <summary>
        /// Gets or sets the list of data sources.
        /// </summary>
        public List<DataSource> DataSources { get; set; }

        /// <summary>
        /// Gets or sets the mapping dictionary.
        /// </summary>
        public Dictionary<string, string> DataMappers { get; set; }

        /// <summary>
        /// Gets or sets the target language name.
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// Gets or sets the VTL 2.0 source expression.
        /// </summary>
        public string Experession { get; set; }

        /// <summary>
        /// Gets or sets the default VTL 2.0 namespace name.
        /// </summary>
        public string DefaultNamespace { get; set; }
    }
}
