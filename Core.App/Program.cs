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
            VtlProgram().Wait();
        }

        private static async Task VtlProgram()
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

            ITargetRenderer plantUml = provider.GetService<PlantUmlTargetRenderer>();

            bool areErrors = errColector.ErrorCollectors.Sum(counter => counter.Errors.Count) > 0;
            
            // back-end:
            string result = await PlantUmlVisualizer.PlantUmlPostAsync(plantUml.Render(schema), !areErrors);
            //string result = await PlantUmlVisualizer.PlantUmlPostAsync(schema.RenderToTarget("PlantUML", schema.AssignmentObjects.ToArray()[0].Expression), !areErrors);

            FilesManager.ResultToFile(provider.GetTargetRenderer("PlantUML").Render(schema), "result.plantuml");

            Debug.WriteLine($"\n\n{result}\n\n");
        }
    }
}