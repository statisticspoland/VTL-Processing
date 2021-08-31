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
        private readonly ILogger<TranslationService> logger;
        private readonly ITreeGenerator treeGenerator;
        private readonly ISchemaModifiersApplier schemaModifiersApplier;
        //private readonly ErrorCollectorProvider errorCollectorProvider;
        private readonly IDataModelAggregator dataModelAggregator;
        private readonly IEnvironmentMapper mapper;
        private readonly IEnumerable<ITargetRenderer> targetRenderers;

        public TranslationService(ILogger<TranslationService> logger,
            ITreeGenerator treeGenerator,
            ISchemaModifiersApplier schemaModifiersApplier,
            IEnumerable<ILoggerProvider> loggerProviders,
            IEnumerable<ITargetRenderer> targetRenderers,
            IDataModelAggregator dataModelAggregator,
            IEnvironmentMapper mapper)
        {
            this.logger = logger;
            this.treeGenerator = treeGenerator;
            this.schemaModifiersApplier = schemaModifiersApplier;
            this.dataModelAggregator = dataModelAggregator;
            this.mapper = mapper;
            this.targetRenderers = targetRenderers;
        }
        public string Translate(TranslateOptions parameters)
        {
            dataModelAggregator.DefaultNamespace = parameters.DefaultNamespace;
            dataModelAggregator.DataModels.Clear();
            dataModelAggregator.AddJsonModel(parameters.Model);

            if (parameters.NamespaceMapping != null && parameters.NamespaceMapping != string.Empty)
            {
                // example mapping: "Json;Regular;Namespace=[DbSchema].[DbTable]."
                foreach (string map in parameters.NamespaceMapping.Split(';'))
                {
                    var sp = map.Split('=');
                    switch(sp.Length)
                    {
                        case 1:
                            this.mapper.Mapping.Add(sp[0], string.Empty);
                            break;
                        case 2:
                            this.mapper.Mapping.Add(sp[0], sp[1]);
                            break;
                        default:
                            throw new ArgumentException("Invalid format of namespace mapping argument");
                    }
                }
            }
            else
            {
                this.mapper.Mapping.Add(parameters.DefaultNamespace, string.Empty);
            }

            ITransformationSchema schema = treeGenerator.BuildTransformationSchema(parameters.Input.OpenText().ReadToEnd());

            schemaModifiersApplier.Process(schema);

            ITargetRenderer target = targetRenderers.FirstOrDefault(tr => tr.Name.ToLower() == parameters.Target.ToLower());

            if (target == null)
                throw new ArgumentException("Unexpected target type.");

            return target.Render(schema);
        }
    }
}
