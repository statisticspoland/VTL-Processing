namespace ExampleApp
{
    using ExampleApp.VtlSources;
    using Microsoft.Extensions.Logging;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using StatisticsPoland.VtlProcessing.Core.DataModelProviders;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.UserInterface;
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

            ITransformationSchema schema = translator.GetFrontEnd().BuildTransformationSchema(Example.Source);
            translator.GetMiddleEnd().Process(schema);

            if (translator.Errors.Count == 0)
            {
                ITargetRenderer plantUmlRenderer = translator.GetTargetRenderer("PlantUML");
                string plantUmlResult = plantUmlRenderer.Render(schema);
                PlantUmlUrlConverter converter = new PlantUmlUrlConverter(plantUmlResult);
                Process.Start("cmd.exe", $"/C start {converter.SVGUrl}");

                ITargetRenderer tsqlRenderer = translator.GetTargetRenderer("TSQL");
                Debug.WriteLine(tsqlRenderer.Render(schema));
            }
        }
    }
}
