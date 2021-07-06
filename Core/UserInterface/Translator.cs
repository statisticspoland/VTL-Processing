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
        private readonly ServiceProvider _provider;

        public Translator(Action<ITranslatorConfig> configuration)
        {
            this.EnvironmentMapper = new EnvironmentMapper();
            this.DataModels = new DataModelAggregator(this.EnvironmentMapper);

            var services = new ServiceCollection().AddVtlProcessing();
            services.AddScoped(typeof(IDataModel), p => this.DataModels);
            services.AddScoped(p => this.DataModels);
            services.AddScoped(p => this.EnvironmentMapper);

            ITranslatorConfig translatorConfig = new TranslatorConfig(services);
            configuration(translatorConfig);

            this.Targets = translatorConfig.Targets;

            ErrorCollectorProvider errorCollectorProvider = new ErrorCollectorProvider();
            services.AddLogging((config) =>
            {
                config.AddProvider(errorCollectorProvider);
            });

            this.Errors = new ErrorsCollection(errorCollectorProvider);

            this._provider = services.BuildServiceProvider();
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
