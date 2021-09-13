namespace StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure
{
    using Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Interfaces;
    using System;

    public static class TranslatorConfigExtensions
    {
        public static void AddTsqlTarget(this ITranslatorConfig config, Action<ITargetBuilder> configure = null)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTsqlTarget();

            TargetBuilder configuration = new TargetBuilder();
            if (configure != null) configure(configuration);

            configuration.UpdateServices(services);
            config.AddTarget(typeof(TsqlTargetRenderer), services);
        }
    }
}
