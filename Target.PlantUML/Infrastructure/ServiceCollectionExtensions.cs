namespace StatisticsPoland.VtlProcessing.Target.PlantUML.Infrastructure
{
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using System;
    using Interfaces;

    /// <summary>
    /// The <see cref="IServiceCollection"/> extension methods whose add the PlantUML target renderer to it.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the PlantUML target renderer to a service collection.
        /// </summary>
        /// <param name="services">The instance of the service collection.</param>
        /// <returns>The service collection.</returns>
        internal static IServiceCollection AddPlantUmlTarget(this IServiceCollection services)
        {
            services.AddScoped<ITargetRenderer, PlantUmlTargetRenderer>();

            return services;
        }

        /// <summary>
        /// Adds the PlantUML target renderer to a service collection.
        /// </summary>
        /// <param name="services">The instance of the service collection.</param>
        /// <param name="config">The builder of the PlantUML target renderer.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddPlantUmlTarget(this IServiceCollection services, Action<ITargetBuilder> config)
        {
            services.AddPlantUmlTarget();

            TargetBuilder configuration = new TargetBuilder();
            config(configuration);

            configuration.UpdateServices(services);
            return services;
        }
    }
}
