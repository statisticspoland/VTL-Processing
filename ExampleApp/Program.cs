namespace ExampleApp
{
    using Microsoft.Extensions.Logging;
    using StatisticsPoland.VtlProcessing.Core.DataModelProviders;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure;
    using System.IO;
    using System;

    public static class Program
    {
        static void Main(string[] args)
        {
            Translator translator = new Translator((configure) =>
            {
                configure.AddTsqlTarget();

                configure.AddLogging((config) =>
                {
                    config.AddConsole();
                });
            });

            translator.DataModels.AddJsonModel($"{Directory.GetCurrentDirectory()}\\DataModel.json");
            translator.DataModels.DefaultNamespace = "Json";

            string source =
                @"result := inner_join(table1 as ds1, table2 as ds2 
                            calc ok := ds1#Me1 <= 100);";

            ITransformationSchema schema = translator.CreateSchema(source);

            if (translator.Errors.Count == 0)
            {
                string tsqlResult = translator.Translate(schema, "TSQL");
                Console.WriteLine(tsqlResult);
            }
        }
    }
}
