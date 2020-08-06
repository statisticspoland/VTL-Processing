namespace Target.PlantUML.Infrastructure
{
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using System;
    using Target.PlantUML.Infrastructure.Interfaces;

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddPlantUmlTarget(this IServiceCollection services)
        {
            services.AddSingleton<ITargetRenderer, PlantUmlTargetRenderer>();
            services.AddSingleton<PlantUmlTargetRenderer>();

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
