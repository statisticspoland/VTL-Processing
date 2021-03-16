namespace StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure
{
    using Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.UserInterface;
    using System;

    public static class TargetCollectionExtensions
    {
        public static void AddTsqlTarget(this TargetCollection collection, Action<ITargetBuilder> configure)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTsqlTarget();
            configure(new TargetBuilder(services));

            collection.AddTarget(typeof(TsqlTargetRenderer), services);
        }
    }
}
