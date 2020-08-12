namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System.Linq;

    /// <summary>
    /// The configuration of the VTL 2.0 translator.
    /// </summary>
    public class VtlProcessingConfig : IVtlProcessingConfig
    {
        private IDataModelAggregator dataModelAggr;

        /// <summary>
        /// Initializes a new instance of nthe <see cref="VtlProcessingConfig"/> class.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public VtlProcessingConfig(IServiceCollection services)
        {
            this.Services = services;
            this.dataModelAggr = this.Services.BuildServiceProvider().GetService<IDataModelAggregator>();
        }

        public IServiceCollection Services { get; }

        public string DefaultNamespace {get; set;}

        public IVtlProcessingConfig AddModel(IDataModel dataModel)
        {
            if (this.dataModelAggr.DataModels != null) dataModelAggr.DataModels.Add(dataModel);        
            else this.dataModelAggr = new DataModelAggregator(this.DefaultNamespace, dataModel);

            this.UseModel(this.dataModelAggr);
            return this;
        }

        /// <summary>
        /// Specifies data models to be remembered by the translator.
        /// </summary>
        /// <param name="dataModel">The data model aggregator.</param>
        private void UseModel(IDataModelAggregator dataModel)
        {
            this.Services.Remove(this.Services.FirstOrDefault(service => service.ServiceType == typeof(IDataModelAggregator)));
            this.Services.Remove(this.Services.FirstOrDefault(service => service.ServiceType == typeof(IDataModel)));
            this.Services.AddSingleton(dataModel);
            this.Services.AddSingleton((IDataModel)dataModel);
        }
    }
}
