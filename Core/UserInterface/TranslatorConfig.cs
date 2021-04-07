namespace StatisticsPoland.VtlProcessing.Core.UserInterface
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using StatisticsPoland.VtlProcessing.Core.UserInterface.Interfaces;
    using System;

    internal class TranslatorConfig : ITranslatorConfig
    {
        private readonly IServiceCollection _services;

        public TranslatorConfig(IServiceCollection services)
        {
            this._services = services;
            this.Targets = new TargetsCollection(services);
        }

        public string DefaultNamespace { get; set; }

        public TargetsCollection Targets { get; }

        public void AddLogging(Action<ILoggingBuilder> config) => this._services.AddLogging(config);

        public void AddTarget(Type targetType, IServiceCollection services = null) => this.Targets.AddTarget(targetType, services);

        public void RemoveTarget(ITargetRenderer target) => this.Targets.RemoveTarget(target);

        public void ClearTargets() => this.Targets.Clear();
    }
}
