using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StatisticsPoland.VtlProcessing.Core.DataModelProviders;
using StatisticsPoland.VtlProcessing.Core.ErrorHandling.Logging;
using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
using StatisticsPoland.VtlProcessing.Service;
using StatisticsPoland.VtlProcessing.Service.Services;
using StatisticsPoland.VtlProcessing.Target.PlantUML.Infrastructure;
using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            string connectionString = @"Server=...;Trusted_Connection=True;";

            services.AddVtlProcessing((configure) => 
            {
                //configure.DataModels.DefaultNamespace = "Json";
                //configure.DataModels.AddSqlServerModel(connectionString);
                //configure.DataModels.AddJsonModel($"{Directory.GetCurrentDirectory()}\\DataModel.json"); // namespace name is in a json file
                ////configure.DataModels.AddRegularModel(RegularModel.ModelConfiguration, "Regular");
                //configure.EnvironmentMapper.Mapping = new Dictionary<string, string>()
                //{
                //    { "Json", string.Empty },
                //    { "Regular", string.Empty },
                //    { "Pivot", "[VtlProcessingTests].[Pivoting]." },
                //};
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
                configure.AddConsole();
                configure.AddDebug();
                configure.AddProvider(new ErrorCollectorProvider());
            });

            services.AddTransient<ITranslationService, TranslationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
