namespace StatisticsPoland.VtlProcessing.Target.PlantUML.Infrastructure
{
    using Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.UserInterface;
    using System;

    public static class TargetCollectionExtensions
    {
        public static void AddPlantUmlTarget(this TargetCollection collection, Action<ITargetBuilder> configure)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddPlantUmlTarget();
            configure(new TargetBuilder(services));

            collection.AddTarget(typeof(PlantUmlTargetRenderer), services);
        }
    }
}
