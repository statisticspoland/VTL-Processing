using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
using StatisticsPoland.VtlProcessing.Target.PlantUML.Infrastructure;
using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure;
using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.IO;

namespace StatisticsPoland.VtlProcessing.Cli
{
    class Program
    {
        protected Program()
        {
        }

        static int Main(string[] args)
        {
            return BuildCommand().InvokeAsync(args).Result;
        }

        private static ServiceProvider ConfigureServices(TranslateOptions options)
        {
            var logConf = new LoggerConfiguration()
                .WriteTo.File("log.txt");

            if(options.Verbose)
            {
                logConf.WriteTo.Console();
            }

            if(options.Console)
            {
                logConf.MinimumLevel.Debug();
            }
            else
            {
                logConf.MinimumLevel.Information();
            }

            Log.Logger = logConf.CreateLogger();

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
                configure.AddSerilog();
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
                    description: "Default data model namespace"),
                new Option(
                    new string[] { "--verbose", "-v"},
                    description: "Print logs to console output"),
                new Option(
                    new string[] { "--console", "-c"},
                    description: "Logs debug information")
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
                logger.LogCritical(ex, "Translation error");
            }
        }
    }
}
