namespace StatisticsPoland.VtlProcessing.Core.UserInterface
{
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using StatisticsPoland.VtlProcessing.Core.FrontEnd.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.MiddleEnd.Modifiers.Interfaces;

    public class Translator
    {
        private readonly IServiceCollection _services;
        private readonly IDataModelAggregator _dataModelAggregator;
        private ServiceProvider provider;

        public Translator(string defaultNamespace)
        {
            this.DefaultNamespace = defaultNamespace;

            this._services = new ServiceCollection();
            this._services.AddSingleton<IDataModelAggregator>(new DataModelAggregator(defaultNamespace, null));
            this._services.AddVtlProcessing((configure) => configure = new VtlProcessingConfig(this._services));

            this.BuildProvider();
            this._dataModelAggregator = this.provider.GetService<IDataModelAggregator>();

            this.Targets = new TargetCollection(this._services, this.BuildProvider);
            this.DataModels = new DataModelCollection(this._dataModelAggregator, this._services, this.BuildProvider);
        }

        public string DefaultNamespace { get; set; } // TODO

        public DataModelCollection DataModels { get; }

        public TargetCollection Targets { get; }

        public ITreeGenerator GetFrontEnd()
        {
            return this.provider.GetFrontEnd();
        }

        public ISchemaModifiersApplier GetMiddleEnd()
        {
            return this.provider.GetMiddleEnd();
        }

        public ITargetRenderer GetTargetRenderer(string name)
        {
            return this.provider.GetTargetRenderer(name);
        }

        private void BuildProvider()
        {
            this.provider = this._services.BuildServiceProvider();
        }
    }
}
