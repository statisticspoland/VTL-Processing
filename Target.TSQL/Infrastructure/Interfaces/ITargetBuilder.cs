﻿namespace Target.TSQL.Infrastructure.Interfaces
{
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;

    /// <summary>
    /// The target builder interface.
    /// </summary>
    public interface ITargetBuilder
    {
        /// <summary>
        /// Gets the service collection.
        /// </summary>
        IServiceCollection Services { get; }

        /// <summary>
        /// Adds the environment names mapper to the target configuration.
        /// </summary>
        /// <param name="envMapper">The environment names mapper.</param>
        /// <returns>Instance of the builder.</returns>
        ITargetBuilder AddEnvMapper(IEnvironmentMapper envMapper);

        /// <summary>
        /// Adds comments to the target translated TSQL code.
        /// </summary>
        /// <returns>Instance of the builder.</returns>
        ITargetBuilder AddComments();

        /// <summary>
        /// Sets an attribute propagation algorithm used by the target.
        /// </summary>
        /// <param name="propagationAlgorithm">The attribute propagation algorithm.</param>
        /// <returns>Instance of the builder.</returns>
        ITargetBuilder SetAttributePropagationAlgorithm(IAttributePropagationAlgorithm propagationAlgorithm);
    }
}