﻿namespace StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure
{
    using Infrastructure.Interfaces;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// The target builder.
    /// </summary>
    internal sealed class TargetBuilder : ITargetBuilder
    {
        private readonly ITargetConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetBuilder"/> class.
        /// </summary>
        public TargetBuilder()
        {
            this._configuration = new TargetConfiguration();
        }

        public ITargetBuilder AddComments()
        {
            this._configuration.UseComments = true;

            return this;
        }

        public ITargetBuilder SetAttributePropagationAlgorithm(IAttributePropagationAlgorithm propagationAlgorithm)
        {
            this._configuration.AttributePropagationAlgorithm = propagationAlgorithm;

            return this;
        }

        public void UpdateServices(IServiceCollection services)
        {
            services.AddScoped(p => this._configuration);
            services.AddScoped(p => this._configuration.AttributePropagationAlgorithm);
        }
    }
}
