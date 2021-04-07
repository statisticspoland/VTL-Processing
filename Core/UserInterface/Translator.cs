namespace StatisticsPoland.VtlProcessing.Core.UserInterface
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using StatisticsPoland.VtlProcessing.Core.DataModelProviders.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling.Logging;
    using StatisticsPoland.VtlProcessing.Core.FrontEnd.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.MiddleEnd.Modifiers.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.UserInterface.Interfaces;
    using System;

    public class Translator
    {
        private readonly IServiceCollection _services;
        private ServiceProvider provider;

        public Translator(Action<ITranslatorConfig> configuration)
        {
            this.DataModels = new DataModelAggregator(() => { return this.DefaultNamespace; });

            this._services = new ServiceCollection().AddVtlProcessing();
            this._services.AddSingleton(this.DataModels);
            this._services.AddSingleton(typeof(IDataModel), this.DataModels);

            ErrorCollectorProvider errorCollectorProvider = new ErrorCollectorProvider();
            this._services.AddLogging((config) =>
            {
                config.AddProvider(errorCollectorProvider);
            });

            this.Errors = new ErrorsCollection(errorCollectorProvider);

            ITranslatorConfig translatorConfig = new TranslatorConfig(this._services);
            configuration(translatorConfig);

            this.DefaultNamespace = translatorConfig.DefaultNamespace;
            this.Targets = translatorConfig.Targets;
            this.EnvironmentMapper = new DictionaryEnvMapper();

            this.provider = this._services.BuildServiceProvider();
        }

        public string DefaultNamespace { get; set; }

        public IDataModelAggregator DataModels { get; }

        public TargetsCollection Targets { get; }

        public ErrorsCollection Errors { get; }

        public IEnvironmentMapper EnvironmentMapper { get; }

        public ITreeGenerator GetFrontEnd() => this.provider.GetFrontEnd();

        public ISchemaModifiersApplier GetMiddleEnd() => this.provider.GetMiddleEnd();

        public ITargetRenderer GetTargetRenderer(string name) => this.provider.GetTargetRenderer(name);
    }
}
