namespace StatisticsPoland.VtlProcessing.Target.PlantUML.Infrastructure
{
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using System;
    using Interfaces;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPlantUmlTarget(this IServiceCollection services)
        {
            services.AddSingleton<ITargetRenderer, PlantUmlTargetRenderer>();

            return services;
        }

        public static IServiceCollection AddPlantUmlTarget(this IServiceCollection services, Action<ITargetBuilder> configure)
        {
            services.AddPlantUmlTarget();
            configure(new TargetBuilder(services));
            return services;
        }
    }
}
