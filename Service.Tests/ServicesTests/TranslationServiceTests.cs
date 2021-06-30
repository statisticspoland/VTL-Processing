namespace Service.Tests.ServicesTests
{
    using Microsoft.Extensions.Logging;
    using Moq;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling.Logging;
    using StatisticsPoland.VtlProcessing.Core.FrontEnd.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.MiddleEnd.Modifiers.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Service.Models;
    using StatisticsPoland.VtlProcessing.Service.Services;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class TranslationServiceTests
    {
        private TranslationService _translationService;

        public TranslationServiceTests()
        {
            List<ILoggerProvider> loggerProviders = new List<ILoggerProvider>();
            loggerProviders.Add(new ErrorCollectorProvider());

            var dataModelAggregatorMock = new Mock<IDataModelAggregator>();
            dataModelAggregatorMock.Setup(m => m.DataModels.Clear()).Verifiable();
            dataModelAggregatorMock.SetupSet(m => m.DefaultNamespace = It.IsAny<string>());
            dataModelAggregatorMock.SetupSet(m => m.EnvironmentMapper.Mapping = It.IsAny<Dictionary<string, string>>());
            
            var treeGeneratorMock = new Mock<ITreeGenerator>();
            treeGeneratorMock.Setup(m => m.BuildTransformationSchema(It.IsAny<string>())).Verifiable();

            var schemaModifiersApplierMock = new Mock<ISchemaModifiersApplier>();
            schemaModifiersApplierMock.Setup(m => m.Process(It.IsAny<ITransformationSchema>())).Verifiable();

            var loggerMock = new Mock<ILogger<TranslationService>>();

            var targetRenderers = new List<ITargetRenderer>();

            var targetRendererTSQL = new Mock<ITargetRenderer>();
            targetRendererTSQL.SetupGet(m => m.Name).Returns("tsql");
            targetRendererTSQL.Setup(m => m.Render(It.IsAny<ITransformationSchema>())).Verifiable();
            targetRenderers.Add(targetRendererTSQL.Object);

            var targetRendererPUML = new Mock<ITargetRenderer>();
            targetRendererPUML.SetupGet(m => m.Name).Returns("plantuml");
            targetRendererTSQL.Setup(m => m.Render(It.IsAny<ITransformationSchema>())).Verifiable();
            targetRenderers.Add(targetRendererPUML.Object);

            _translationService = new TranslationService(
                loggerMock.Object,
                treeGeneratorMock.Object,
                schemaModifiersApplierMock.Object,
                loggerProviders,
                targetRenderers,
                dataModelAggregatorMock.Object
                );
        }

        [Fact]
        public void TranslationService_DataSourceType_NotExist()
        {

            TranslationParameters parameters = new TranslationParameters()
            {
                DataMappers = new Dictionary<string, string>(),
                DefaultNamespace = string.Empty,
                DataSources = new List<DataSource>() { new DataSource() { Localazation = "test", Namespace = "", Type = "type" } },
                Experession = string.Empty,
                Target = string.Empty
            };

            TranslationResponse response = _translationService.Tanslate(parameters);

            Assert.True(response.AreErrors);
            Assert.Equal("Unexpected model type.", response.Exceptions.First().Message);
        }

        [Fact]
        public void TranslationService_Target_NotExist()
        {
            TranslationParameters parameters = new TranslationParameters()
            {
                DataMappers = new Dictionary<string, string>(),
                DefaultNamespace = string.Empty,
                DataSources = new List<DataSource>() { },
                Experession = string.Empty,
                Target = "test"
            };

            TranslationResponse response = _translationService.Tanslate(parameters);

            Assert.True(response.AreErrors);
            Assert.Equal("Unexpected target type.", response.Exceptions.First().Message);
        }
    }
}
