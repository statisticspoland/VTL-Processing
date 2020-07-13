namespace Core.App
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Logging;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System;
    using System.Linq;

    public sealed class Program
    {
        public static void Main(string[] args)
        {
            string source = "DS := A + B";
            
            IServiceCollection services = new ServiceCollection();
            services.AddVtlProcessing((configure) =>
            {
                configure.Empty();
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

            var vtlError = errColector.GetOfType<IVtlError>().ToList();
            var critError = errColector.GetOfType<Exception>().ToList();
        }
    }
}