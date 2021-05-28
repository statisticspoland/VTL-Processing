namespace StatisticsPoland.VtlProcessing.Cli
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

    class TranslationService : ITranslationService
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
            _logger = logger;
            _treeGenerator = treeGenerator;
            _schemaModifiersApplier = schemaModifiersApplier;
            _errorCollectorProvider = (ErrorCollectorProvider)loggerProviders.SingleOrDefault(l => l.GetType() == typeof(ErrorCollectorProvider));
            _dataModelAggregator = dataModelAggregator;
            _targetRenderers = targetRenderers;
        }
        public string Translate(TranslateOptions parameters)
        {
            _dataModelAggregator.DefaultNamespace = parameters.DefaultNamespace;
            _dataModelAggregator.DataModels.Clear();
            _dataModelAggregator.AddJsonModel(parameters.Model);
            

            ITransformationSchema schema = _treeGenerator.BuildTransformationSchema(parameters.Input.OpenText().ReadToEnd());

            _schemaModifiersApplier.Process(schema);

            bool areErrors = _errorCollectorProvider.ErrorCollectors.Sum(counter => counter.Errors.Count) > 0;

            if (areErrors)
            {
                throw new InvalidOperationException("Translation error");
            }

            ITargetRenderer target = _targetRenderers.FirstOrDefault(tr => tr.Name.ToLower() == parameters.Target.ToLower());

            if (target == null)
                throw new ArgumentException("Unexpected target type.");

            return target.Render(schema);
        }
    }
}
