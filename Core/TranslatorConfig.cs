namespace StatisticsPoland.VtlProcessing.Core
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using StatisticsPoland.VtlProcessing.Core.Interfaces;
    using System;

    /// <summary>
    /// The VTL 2.0 translator representation configuration.
    /// </summary>
    internal class TranslatorConfig : ITranslatorConfig
    {
        private readonly IServiceCollection _services;

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslatorConfig"/> class.
        /// </summary>
        /// <param name="services">The translator service collection.</param>
        public TranslatorConfig(IServiceCollection services)
        {
            this._services = services;
            this.Targets = new TargetsCollection(services);
            this.RemoveDeadCode = false;
        }

        public bool RemoveDeadCode { get; set; }

        public TargetsCollection Targets { get; }

        public void AddLogging(Action<ILoggingBuilder> config) => this._services.AddLogging(config);

        public void AddTarget(Type targetType, IServiceCollection services = null) => this.Targets.AddTarget(targetType, services);

        public void RemoveTarget(ITargetRenderer target) => this.Targets.RemoveTarget(target);

        public void ClearTargets() => this.Targets.Clear();
    }
}
