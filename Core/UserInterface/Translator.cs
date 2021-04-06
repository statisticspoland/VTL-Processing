namespace StatisticsPoland.VtlProcessing.Core.UserInterface
{
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using StatisticsPoland.VtlProcessing.Core.FrontEnd.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.MiddleEnd.Modifiers.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System;

    public class Translator
    {
        private readonly IServiceCollection _services;
        private ServiceProvider provider;

        public Translator(string defaultNamespace)
        {
            this.DefaultNamespace = defaultNamespace;
            this.DataModels = new DataModelAggregator(() => { return this.DefaultNamespace; });

            this._services = new ServiceCollection().AddVtlProcessing();
            this._services.AddSingleton(this.DataModels);
            this._services.AddSingleton(typeof(IDataModel), this.DataModels);

            this.Targets = new TargetsCollection(this._services, this.BuildProvider);
        }

        public string DefaultNamespace { get; set; }

        public IDataModelAggregator DataModels { get; }

        public TargetsCollection Targets { get; }

        public ITreeGenerator GetFrontEnd()
        {
            if (!this.Targets.Confirmed) throw new Exception("The collection of targets has been not confirmed.");
            return this.provider.GetFrontEnd();
        }

        public ISchemaModifiersApplier GetMiddleEnd()
        {
            if (!this.Targets.Confirmed) throw new Exception("The collection of targets has been not confirmed.");
            return this.provider.GetMiddleEnd();
        }

        public ITargetRenderer GetTargetRenderer(string name)
        {
            if (!this.Targets.Confirmed) throw new Exception("The collection of targets has been not confirmed.");
            return this.provider.GetTargetRenderer(name);
        }

        private void BuildProvider()
        {
            this.provider = this._services.BuildServiceProvider();
        }
    }
}
