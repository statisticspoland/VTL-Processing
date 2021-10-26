namespace StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure
{
    using Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Interfaces;
    using System;

    /// <summary>
    /// The <see cref="ITranslatorConfig"/> extension methods whose add the TSQL target renderer to it.
    /// </summary>
    public static class TranslatorConfigExtensions
    {
        /// <summary>
        /// Adds the TSQL target renderer to a translator configuration.
        /// </summary>
        /// <param name="config">The instance of the translator configuration.</param>
        /// <param name="configure">The builder of the TSQL target renderer.</param>
        /// <returns>The service collection.</returns>
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
