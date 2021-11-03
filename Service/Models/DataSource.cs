namespace StatisticsPoland.VtlProcessing.Service.Models
{
    /// <summary>
    /// The source of a VTL 2.0 dataset.
    /// </summary>
    public class DataSource
    {
        /// <summary>
        /// Gets or sets the type of the data source.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the localization of the data source.
        /// </summary>
        public string Localization { get; set; }

        /// <summary>
        /// Gets or sets the namespace of a dataset from the source.
        /// </summary>
        public string Namespace { get; set; }
    }
}
