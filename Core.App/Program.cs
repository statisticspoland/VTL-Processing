namespace Core.App
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling.Logging;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System.Diagnostics;
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
            string source = "DS1 := A + B; DS2 := A#Me_1 * -B#Me_2;";
            
            IServiceCollection services = new ServiceCollection();
            services.AddVtlProcessing((configure) =>
            {
                configure.Empty();
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