namespace Core.App
{
    using Core.App.VtlSources;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling.Logging;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.DataModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Target.PlantUML;
    using Target.PlantUML.Infrastructure;

    public sealed class Program
    {
        public static void Main(string[] args)
        {
            string source = Example.Source;

            IServiceCollection services = new ServiceCollection();
            services.AddVtlProcessing((configure) =>
            {
                configure.DefaultNamespace = "Json";
                configure.AddJsonModel($"{Directory.GetCurrentDirectory()}\\DataModel.json");
                configure.AddRegularModel(RegularModel.ModelConfiguration);
            });

            services.AddPlantUmlTarget((configure) =>
            {
                configure.AddDataStructureObject();
                configure.UseArrowFirstToLast();
                configure.ShowNumberLine();
                //configure.UseArrowLastToFirst();
                //configure.UseHorizontalView();
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

            Debug.WriteLine($"\n\n{converter.SVGUrl}\n\n");

            if (!areErrors)
            {
                Process.Start("cmd.exe", $"/C start {converter.SVGUrl}");
                FilesManager.ResultToFile(plantUmlResult, "result.plantuml");
            }
        }
    }
}