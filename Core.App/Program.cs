namespace Core.App
{
    using Core.App.VtlSources;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using StatisticsPoland.VtlProcessing.Core.DataModelProviders;
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling.Logging;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Target.PlantUML.Infrastructure;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    public static class Program
    {
        public static void Main(string[] args)
        {
            string source = Example.Source;
            string connectionString = @"Server=...;Trusted_Connection=True;";
            IServiceCollection services = new ServiceCollection();

            services.AddVtlProcessing((configure) =>
            {
                configure.DataModels.DefaultNamespace = "Json";
                configure.DataModels.AddSqlServerModel(connectionString);
                configure.DataModels.AddJsonModel($"{Directory.GetCurrentDirectory()}\\DataModel.json"); // namespace name is in a json file
                configure.DataModels.AddDictionaryModel((config) =>
                {
                    config
                    .AddDataSet(
                        "R1",
                        (ComponentType.Identifier, BasicDataType.Integer, "Id1"),
                        (ComponentType.Identifier, BasicDataType.Integer, "Id2"),
                        (ComponentType.Measure, BasicDataType.Integer, "Me1"),
                        (ComponentType.Measure, BasicDataType.Integer, "Me2"),
                        (ComponentType.NonViralAttribute, BasicDataType.String, "At1"),
                        (ComponentType.ViralAttribute, BasicDataType.Integer, "At2")
                        )
                    .AddDataSet(
                        "R2",
                        (ComponentType.Identifier, BasicDataType.Integer, "Id1"),
                        (ComponentType.Measure, BasicDataType.String, "Me1"),
                        (ComponentType.Measure, BasicDataType.Integer, "Me2")
                        )
                    .AddDataSet(
                        "R_num",
                        (ComponentType.Identifier, BasicDataType.Integer, "Id1"),
                        (ComponentType.Measure, BasicDataType.Number, "Me2")
                        );
                }
                , "def_ns");
                
                configure.EnvironmentMapper.Mapping = new Dictionary<string, string>()
                {
                    { "Json", string.Empty },
                    { "Regular", string.Empty },
                    { "Pivot", "[VtlProcessingTests].[Pivoting]." },
                };
            });
            
            services.AddPlantUmlTarget((configure) =>
            {
                configure.AddDataStructureObject();
                configure.UseArrowFirstToLast();
                configure.ShowNumberLine();
                configure.UseRuleExpressionsModel();
            });

            services.AddTsqlTarget((configure) =>
            {
                configure.AddComments();
            });

            services.AddLogging((configure) =>
            {
                configure.AddConsole();
                configure.AddDebug();
                configure.AddProvider(new ErrorCollectorProvider());
            });

            ServiceProvider provider = services.BuildServiceProvider();
            ErrorCollectorProvider errColector = provider.GetService<ILoggerProvider>() as ErrorCollectorProvider;

            ITransformationSchema schema = provider.GetFrontEnd().BuildTransformationSchema(source); // front-end

            provider.GetMiddleEnd().Process(schema); // middle-end

            bool areErrors = errColector.ErrorCollectors.Sum(counter => counter.Errors.Count) > 0;

            // back-end:
            ITargetRenderer plantUmlRenderer = provider.GetTargetRenderer("PlantUML");
            string plantUmlResult = plantUmlRenderer.Render(schema);
            PlantUmlUrlConverter converter = new PlantUmlUrlConverter(plantUmlResult);

            if (!areErrors)
            {
                ITargetRenderer tsqlRenderer = provider.GetTargetRenderer("TSQL");

                Process.Start("cmd.exe", $"/C start {converter.SVGUrl}");

                FilesManager.ResultToFile(plantUmlResult, "result.plantuml");
                FilesManager.ResultToFile(tsqlRenderer.Render(schema), "result.sql");
            }
            else Debug.WriteLine($"\n\n{converter.SVGUrl}\n\n");
        }
    }
}