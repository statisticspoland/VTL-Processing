namespace StatisticsPoland.VtlProcessing.Target.PlantUML.Infrastructure
{
    using Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Interfaces;
    using System;

    /// <summary>
    /// The <see cref="ITranslatorConfig"/> extension methods whose add the PlantUML target renderer to it.
    /// </summary>
    public static class TranslatorConfigExtensions
    {
        /// <summary>
        /// Adds the PlantUML target renderer to a translator configuration.
        /// </summary>
        /// <param name="config">The instance of the translator configuration.</param>
        /// <param name="configure">The builder of the PlantUML target renderer.</param>
        /// <returns>The service collection.</returns>
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
