namespace ExampleApp
{
    using ExampleApp.VtlSources;
    using Microsoft.Extensions.Logging;
    using StatisticsPoland.VtlProcessing.Core.DataModelProviders;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core;
    using StatisticsPoland.VtlProcessing.Target.PlantUML.Infrastructure;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;

    public static class Program
    {
        static void Main(string[] args)
        {
            Translator translator = new Translator((configure) =>
            {
                configure.AddPlantUmlTarget((config) =>
                {
                    config.AddDataStructureObject();
                    config.UseArrowFirstToLast();
                    config.ShowNumberLine();
                });

                configure.AddTsqlTarget((config) =>
                {
                    config.AddComments();
                });

                configure.AddLogging((config) =>
                {
                    config.AddConsole();
                    config.AddDebug();
                });
            });

            translator.EnvironmentMapper.Mapping = new Dictionary<string, string>()
                {
                    { "Json", string.Empty },
                    { "Regular", string.Empty },
                    { "Namespace", "[DbSchema].[DbTable]." },
                };

            translator.DataModels.AddJsonModel($"{Directory.GetCurrentDirectory()}\\DataModel.json");
            translator.DataModels.AddRegularModel(RegularModel.ModelConfiguration, "Regular");
            translator.DataModels.DefaultNamespace = "Json";

            ITransformationSchema schema = translator.CreateSchema(Example.Source);

            if (translator.Errors.Count == 0)
            {
                string plantUmlResult = translator.Translate(schema, "PlantUML");
                PlantUmlUrlConverter converter = new PlantUmlUrlConverter(plantUmlResult);
                Process.Start("cmd.exe", $"/C start {converter.SVGUrl}");

                string tsqlResult = translator.Translate(schema, "TSQL");
                Debug.WriteLine(tsqlResult);
            }
        }
    }
}
