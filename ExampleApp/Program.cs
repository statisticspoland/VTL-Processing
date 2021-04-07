namespace ExampleApp
{
    using ExampleApp.VtlSources;
    using Microsoft.Extensions.Logging;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using StatisticsPoland.VtlProcessing.Core.DataModelProviders;
    using StatisticsPoland.VtlProcessing.Core.DataModelProviders.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.UserInterface;
    using StatisticsPoland.VtlProcessing.Target.PlantUML.Infrastructure;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;

    public class Program
    {
        static void Main(string[] args)
        {
            IEnvironmentMapper envMapper = new DictionaryEnvMapper(
                new Dictionary<string, string>()
                {
                    { "Json", "ABC." },
                    { "Json2", "XYZ." },
                    { "Regular", string.Empty },
                    { "Namespace", "[DbSchema].[DbTable]." },
                });

            Translator translator = new Translator((configure) =>
            {
                configure.DefaultNamespace = "Json";
                configure.AddPlantUmlTarget((config) =>
                {
                    config.AddDataStructureObject();
                    config.UseArrowFirstToLast();
                    config.ShowNumberLine();
                });

                configure.AddTsqlTarget((config) =>
                {
                    config.AddEnvMapper(envMapper);
                    config.AddComments();
                });

                configure.AddLogging((config) =>
                {
                    config.AddConsole();
                    config.AddDebug();
                });
            });

            translator.DataModels.AddJsonModel($"{Directory.GetCurrentDirectory()}\\DataModel.json");
            translator.DataModels.AddJsonModel($"{Directory.GetCurrentDirectory()}\\DataModel2.json");
            translator.DataModels.AddRegularModel(RegularModel.ModelConfiguration, "Regular");

            translator.DefaultNamespace = "Json2";
            string source = "P := Regular\\R1 + Json\\Y - Y;";

            ITransformationSchema schema = translator.GetFrontEnd().BuildTransformationSchema(source);
            translator.GetMiddleEnd().Process(schema);

            ITargetRenderer plantUmlRenderer = translator.GetTargetRenderer("PlantUML");
            string plantUmlResult = plantUmlRenderer.Render(schema);
            PlantUmlUrlConverter converter = new PlantUmlUrlConverter(plantUmlResult);
            Process.Start("cmd.exe", $"/C start {converter.SVGUrl}");

            ITargetRenderer tsqlRenderer = translator.GetTargetRenderer("TSQL");
            Debug.WriteLine(tsqlRenderer.Render(schema));
        }
    }
}
