namespace StatisticsPoland.VtlProcessing.Target.PlantUML.Infrastructure
{
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using System;
    using Interfaces;

    public static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddPlantUmlTarget(this IServiceCollection services)
        {
            services.AddScoped<ITargetRenderer, PlantUmlTargetRenderer>();

            return services;
        }

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
