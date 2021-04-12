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
    using StatisticsPoland.VtlProcessing.Target.PlantUML.Infrastructure;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    public sealed class Program
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
                configure.DataModels.AddRegularModel(RegularModel.ModelConfiguration, "Regular");
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
                //configure.UseArrowLastToFirst();
                //configure.UseHorizontalView();
            });

            services.AddTsqlTarget((configure) =>
            {
                configure.AddComments();
                //configure.SetAttributePropagationAlgorithm(new AttributePropagationAlgorithm());
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

            //var vtlErrors = errColector.GetErrorsOfType<IVtlError>().ToList()
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