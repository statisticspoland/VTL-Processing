using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Threading.Tasks;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using Microsoft.Extensions.Hosting;
using System.CommandLine.Parsing;
using Microsoft.Extensions.DependencyInjection;
using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
using StatisticsPoland.VtlProcessing.Core.DataModelProviders;
using Microsoft.Extensions.Logging;
using StatisticsPoland.VtlProcessing.Core.ErrorHandling.Logging;
using System.Collections.Generic;
using StatisticsPoland.VtlProcessing.Target.PlantUML.Infrastructure;
using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure;
using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
using System.Linq;

namespace StatisticsPoland.VtlProcessing.Cli
{
    class Program
    {
        static int Main(string[] args)
        {
            return BuildCommand().InvokeAsync(args).Result;
        }

        private static ServiceProvider ConfigureServices(TranslateOptions options)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddVtlProcessing((configure) =>
            {
                
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
                configure.AddDebug();
                configure.AddProvider(new ErrorCollectorProvider());
            });

            services.AddSingleton<ITranslationService, TranslationService>();

            return services.BuildServiceProvider();
        }

        private static RootCommand BuildCommand()
        {
            var root = new RootCommand
            {
                new Option<FileInfo>(
                    new string[] {"--output", "-o" },
                    description: "Output file path"),
                new Option<string>(
                    new string[] {"--target", "-t" },
                    description: "Translation target language"),
                new Option<string>(
                    new string[] {"--model", "-m" },
                    description: "Data model source (connection string or JSON file path)"),
                new Option<string>(
                    new string[] {"--namespace-mapping", "-n" },
                    description: "Namespace mapping json file path"),
                new Option<string>(
                    new string[] { "--default-namespace", "-d" },
                    description: "Default data model namespace")
            };

            root.AddArgument(new Argument<FileInfo>("input"));
            root.Description = "VTL translator command line interface";

            root.Handler = CommandHandler.Create<TranslateOptions>(Run);
            return root;
        }

        private static void Run(TranslateOptions options)
        {
            var serviceProvider = ConfigureServices(options);
            var translator = serviceProvider.GetRequiredService<ITranslationService>();
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            var loggerProviders = serviceProvider.GetRequiredService<IEnumerable<ILoggerProvider>>();
            var errorCollectorProvider = (ErrorCollectorProvider)loggerProviders.SingleOrDefault(l => l.GetType() == typeof(ErrorCollectorProvider));

            logger.LogDebug("translating...");

            try
            {
                string result = translator.Translate(options);
                if (options.Output == null)
                {
                    Console.WriteLine(result);
                }
                else
                {
                    var file = options.Output.CreateText();
                    file.Write(result);
                    file.Close();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                var errors = errorCollectorProvider.ErrorCollectors.SelectMany(ec => ec.Errors).Where(e => e != null);
                var warnings = errorCollectorProvider.ErrorCollectors.SelectMany(ec => ec.Warnings).Where(e => e != null);

                foreach (var error in errors)
                {
                    Console.WriteLine(error.Message);
                }

                foreach (var warning in warnings)
                {
                    Console.WriteLine(warning.Message);
                }
            }

        }
    }
}
