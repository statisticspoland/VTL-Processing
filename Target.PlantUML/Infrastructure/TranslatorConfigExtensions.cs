namespace StatisticsPoland.VtlProcessing.Target.PlantUML.Infrastructure
{
    using Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Interfaces;
    using System;

    public static class TranslatorConfigExtensions
    {
        public static void AddPlantUmlTarget(this ITranslatorConfig config, Action<ITargetBuilder> configure = null)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddPlantUmlTarget();

            TargetBuilder configuration = new TargetBuilder();
            if (configure != null) configure(configuration);

            configuration.UpdateServices(services);
            config.AddTarget(typeof(PlantUmlTargetRenderer), services);
        }
    }
}
