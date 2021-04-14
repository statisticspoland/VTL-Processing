namespace StatisticsPoland.VtlProcessing.Core.UserInterface.Interfaces
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using System;

    public interface ITranslatorConfig
    {
        /// <summary>
        /// Gets the targets collection.
        /// </summary>
        TargetsCollection Targets { get; }

        /// <summary>
        /// Adds logging services.
        /// </summary>
        /// <param name="config">The ILoggingBuilder configuration delegate.</param>
        void AddLogging(Action<ILoggingBuilder> config);

        /// <summary>
        /// Adds a target to the target collection and injects its depentent services.
        /// </summary>
        /// <param name="targetType">The target type.</param>
        /// <param name="services">The services to inject.</param>
        void AddTarget(Type targetType, IServiceCollection services = null);

        /// <summary>
        /// Removes a target from the collection but not removes its dependent sevices.
        /// </summary>
        ///<param name="target">The target to remove.</param>
        void RemoveTarget(ITargetRenderer target);

        /// <summary>
        /// Clears the target collection but not removes dependent sevices of these targets.
        /// </summary>
        void ClearTargets();
    }
}
