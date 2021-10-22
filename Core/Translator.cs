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

    /// <summary>
    /// The VTL 2.0 translator representation.
    /// </summary>
    public class Translator
    {
        private readonly ServiceProvider _provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="Translator"/> class.
        /// </summary>
        /// <param name="configuration">The configuration of the translator.</param>
        public Translator(Action<ITranslatorConfig> configuration)
        {
            this.DataModels = new DataModelAggregator(new EnvironmentMapper(this.DataModels));

            var services = new ServiceCollection().AddVtlProcessing();
            services.AddScoped(typeof(IDataModelProvider), p => this.DataModels);
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

        /// <summary>
        /// Gets the data models aggregator.
        /// </summary>
        public IDataModelAggregator DataModels { get; }

        /// <summary>
        /// Gets the targets collection.
        /// </summary>
        public TargetsCollection Targets { get; }

        /// <summary>
        /// Gets the translation errors collection.
        /// </summary>
        public ErrorsCollection Errors { get; }

        /// <summary>
        /// Gets the environment mapper.
        /// </summary>
        public IEnvironmentMapper EnvironmentMapper => this.DataModels.EnvironmentMapper;

        /// <summary>
        /// Creates and returns the transformation schema from a given VTL 2.0 source code.
        /// </summary>
        /// <param name="vtlSource">The VTL 2.0 source code.</param>
        /// <returns>The transformation schema.</returns>
        public ITransformationSchema CreateSchema(string vtlSource) => this._provider.GetFrontEnd().BuildTransformationSchema(vtlSource);

        /// <summary>
        /// Translates a given VTL 2.0 source code to target's language code whose target name is given.
        /// </summary>
        /// <param name="vtlSource">The VTL 2.0 source code.</param>
        /// <param name="targetName">The name of a target.</param>
        /// <returns></returns>
        public string Translate(string vtlSource, string targetName)
        {
            ITransformationSchema schema = this.CreateSchema(vtlSource);

            return this.Translate(schema, targetName);
        }

        /// <summary>
        /// Translates a given transformation schema to target's language code whose target name is given.
        /// </summary>
        /// <param name="schema">The transformation schema.</param>
        /// <param name="targetName">The name of a target.</param>
        /// <param name="schemaProcessed">Specifies if the schema has been processed by middle-end operations (ex. type inference).</param>
        /// <returns></returns>
        public string Translate(ITransformationSchema schema, string targetName, bool schemaProcessed = false)
        {
            ITargetRenderer targetRenderer = this._provider.GetTargetRenderer(targetName);

            if (!schemaProcessed) this._provider.GetMiddleEnd().Process(schema);
            if (targetRenderer == null) throw new NullReferenceException($"Target renderer named \"{targetName}\" has been not found.");

            return this._provider.GetTargetRenderer(targetName).Render(schema);
        }
    }
}
