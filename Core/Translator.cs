namespace StatisticsPoland.VtlProcessing.Core
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using StatisticsPoland.VtlProcessing.Core.DataModelProviders.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling.Logging;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Interfaces;
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

        public ITransformationSchema CreateSchema(string vtlSource) => this._provider.GetFrontEnd().BuildTransformationSchema(vtlSource);

        public string Translate(string vtlSource, string targetName)
        {
            ITransformationSchema schema = this.CreateSchema(vtlSource);

            return this.Translate(schema, targetName);
        }

        public string Translate(ITransformationSchema schema, string targetName, bool schemaProcessed = false)
        {
            ITargetRenderer targetRenderer = this._provider.GetTargetRenderer(targetName);

            if (!schemaProcessed) this._provider.GetMiddleEnd().Process(schema);
            if (targetRenderer == null) throw new NullReferenceException($"Target renderer named \"{targetName}\" has been not found.");

            return this._provider.GetTargetRenderer(targetName).Render(schema);
        }
    }
}
