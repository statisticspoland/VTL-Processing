namespace StatisticsPoland.VtlProcessing.Core.Infrastructure
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;

    internal class VtlProcessingConfig : IVtlProcessingConfig
    {
        public VtlProcessingConfig()
        {
            this.DataModels = new DataModelAggregator();
        }

        public string DefaultNamespace { get; set; }

        public IDataModelAggregator DataModels { get; }
    }
}
