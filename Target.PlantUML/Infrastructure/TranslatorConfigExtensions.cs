namespace StatisticsPoland.VtlProcessing.Target.PlantUML.Infrastructure
{
    using Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.UserInterface.Interfaces;
    using System;

    public static class TranslatorConfigExtensions
    {
        public static void AddPlantUmlTarget(this ITranslatorConfig config, Action<ITargetBuilder> configure)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddPlantUmlTarget();
            configure(new TargetBuilder(services));

            config.AddTarget(typeof(PlantUmlTargetRenderer), services);
        }
    }
}
