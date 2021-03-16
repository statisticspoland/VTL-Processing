namespace ExampleApp
{
    using ExampleApp.VtlSources;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using StatisticsPoland.VtlProcessing.Core.DataModelProviders;
    using StatisticsPoland.VtlProcessing.Core.DataModelProviders.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.DataModelProviders.Models;
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
                    { "Json", string.Empty },
                    { "Regular", string.Empty },
                    { "Namespace", "[DbSchema].[DbTable]." },
                });

            Translator translator = new Translator("Json");
            translator.DataModels.AddJsonModel($"{Directory.GetCurrentDirectory()}\\DataModel.json");
            translator.DataModels.AddRegularModel(RegularModel.ModelConfiguration, "Regular");

            translator.Targets.AddPlantUmlTarget((configure) =>
            {
                configure.AddDataStructureObject();
                configure.UseArrowFirstToLast();
                configure.ShowNumberLine();
                //configure.UseRuleExpressionsModel();
                //configure.UseArrowLastToFirst();
                //configure.UseHorizontalView();
            });

            translator.Targets.AddTsqlTarget((configure) =>
            {
                configure.AddEnvMapper(envMapper);
                configure.AddComments();
                //configure.SetAttributePropagationAlgorithm(new AttributePropagationAlgorithm());
            });

            translator.DataModels.Clear();

            // TODO: Error Logger

            string source = Example.Source;
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
