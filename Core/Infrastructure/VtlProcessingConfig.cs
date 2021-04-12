namespace StatisticsPoland.VtlProcessing.Core.Infrastructure
{
    using StatisticsPoland.VtlProcessing.Core.DataModelProviders.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;

    internal class VtlProcessingConfig : IVtlProcessingConfig
    {
        public VtlProcessingConfig()
        {
            this.EnvironmentMapper = new EnvironmentMapper();
            this.DataModels = new DataModelAggregator(this.EnvironmentMapper);
        }

        public IDataModelAggregator DataModels { get; }

        public IEnvironmentMapper EnvironmentMapper { get; }
    }
}
