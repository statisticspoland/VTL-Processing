namespace StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure
{
    using Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.UserInterface.Interfaces;
    using System;

    public static class TranslatorConfigExtensions
    {
        public static void AddTsqlTarget(this ITranslatorConfig config, Action<ITargetBuilder> configure)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTsqlTarget();
            configure(new TargetBuilder(services));

            config.AddTarget(typeof(TsqlTargetRenderer), services);
        }
    }
}
