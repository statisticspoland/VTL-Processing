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
        private readonly ServiceProvider _provider;

        public Translator(Action<ITranslatorConfig> configuration)
        {
            this.EnvironmentMapper = new EnvironmentMapper();
            this.DataModels = new DataModelAggregator(this.EnvironmentMapper);

            this._services = new ServiceCollection().AddVtlProcessing();
            this._services.AddSingleton(typeof(IDataModel), this.DataModels);
            this._services.AddSingleton(this.DataModels);
            this._services.AddSingleton(this.EnvironmentMapper);

            ITranslatorConfig translatorConfig = new TranslatorConfig(this._services);
            configuration(translatorConfig);

            this.Targets = translatorConfig.Targets;

            ErrorCollectorProvider errorCollectorProvider = new ErrorCollectorProvider();
            this._services.AddLogging((config) =>
            {
                config.AddProvider(errorCollectorProvider);
            });

            this.Errors = new ErrorsCollection(errorCollectorProvider);

            this._provider = this._services.BuildServiceProvider();
        }

        public IDataModelAggregator DataModels { get; }

        public TargetsCollection Targets { get; }

        public ErrorsCollection Errors { get; }

        public IEnvironmentMapper EnvironmentMapper { get; }

        public ITreeGenerator GetFrontEnd() => this._provider.GetFrontEnd();

        public ISchemaModifiersApplier GetMiddleEnd() => this._provider.GetMiddleEnd();

        public ITargetRenderer GetTargetRenderer(string name) => this._provider.GetTargetRenderer(name);
    }
}
