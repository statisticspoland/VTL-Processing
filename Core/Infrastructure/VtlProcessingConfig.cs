namespace StatisticsPoland.VtlProcessing.Core.Infrastructure
{
    using StatisticsPoland.VtlProcessing.Core.DataModelProviders.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;

    /// <summary>
    /// The configuration of a VTL 2.0 translator.
    /// </summary>
    internal class VtlProcessingConfig : IVtlProcessingConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VtlProcessingConfig"/> class.
        /// </summary>
        public VtlProcessingConfig()
        {
            this.DataModels = new DataModelAggregator(new EnvironmentMapper(this.DataModels));
            this.RemoveDeadCode = false;
        }

        public bool RemoveDeadCode { get; set; }

        public IDataModelAggregator DataModels { get; }

        public IEnvironmentMapper EnvironmentMapper => this.DataModels.EnvironmentMapper;

    }
}
