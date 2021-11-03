namespace StatisticsPoland.VtlProcessing.Service.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Logging;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using StatisticsPoland.VtlProcessing.Core.DataModelProviders;
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling.Logging;
    using StatisticsPoland.VtlProcessing.Core.FrontEnd.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.MiddleEnd.Modifiers.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Service.Models;

    public class TranslationService : ITranslationService
    {
        private readonly ILogger<TranslationService> _logger;
        private readonly ITreeGenerator _treeGenerator;
        private readonly ISchemaModifiersApplier _schemaModifiersApplier;
        private readonly ErrorCollectorProvider _errorCollectorProvider;
        private readonly IDataModelAggregator _dataModelAggregator;
        private readonly IEnumerable<ITargetRenderer> _targetRenderers;

        public TranslationService(ILogger<TranslationService> logger,
            ITreeGenerator treeGenerator,
            ISchemaModifiersApplier schemaModifiersApplier,
            IEnumerable<ILoggerProvider> loggerProviders,
            IEnumerable<ITargetRenderer> targetRenderers,
            IDataModelAggregator dataModelAggregator)
        {
            this._logger = logger;
            this._treeGenerator = treeGenerator;
            this._schemaModifiersApplier = schemaModifiersApplier;
            this._errorCollectorProvider = (ErrorCollectorProvider)loggerProviders.SingleOrDefault(l => l.GetType() == typeof(ErrorCollectorProvider));
            this._dataModelAggregator = dataModelAggregator;
            this._targetRenderers = targetRenderers;
        }
        public TranslationResponse Tanslate(TranslationParameters parameters)
        {
            this._dataModelAggregator.EnvironmentMapper.Mapping = parameters.DataMappers;
            this._dataModelAggregator.DefaultNamespace = parameters.DefaultNamespace;
            this._dataModelAggregator.DataModelsCollection.Clear();

            foreach (var dataSource in parameters.DataSources)
            {
                switch (dataSource.Type.ToLower())
                {
                    case "json":
                        this._dataModelAggregator.AddJsonModel(dataSource.Localization);
                        break;
                    case "sdmx":
                        this._dataModelAggregator.AddSdmxModel(dataSource.Localization, dataSource.Namespace);
                        break;
                    case "tsql":
                        this._dataModelAggregator.AddSqlServerModel(dataSource.Localization);
                        break;
                    default:
                        return new TranslationResponse(new ArgumentException("Unexpected model type."));
                }
            }

            ITransformationSchema schema = this._treeGenerator.BuildTransformationSchema(parameters.Experession);

            this._schemaModifiersApplier.Process(schema);

            bool areErrors = this._errorCollectorProvider.ErrorCollectors.Sum(counter => counter.Errors.Count) > 0;

            if(areErrors)
            {
                return new TranslationResponse(this._errorCollectorProvider.ErrorCollectors.SelectMany(ec => ec.Errors).ToList());
            }

            ITargetRenderer target = this._targetRenderers.FirstOrDefault(tr => tr.Name == parameters.Target);

            if (target == null)
                return new TranslationResponse(new ArgumentException("Unexpected target type."));

            return new TranslationResponse(target.Render(schema));
        }
    }
}
