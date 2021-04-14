namespace StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure
{
    using Infrastructure.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;

    /// <summary>
    /// The target builder.
    /// </summary>
    public class TargetBuilder : ITargetBuilder
    {
        private TargetConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetBuilder"/> class.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public TargetBuilder(IServiceCollection services)
        {
            this.Services = services;
            this.configuration = new TargetConfiguration();
        }

        public IServiceCollection Services { get; }

        public ITargetBuilder AddComments()
        {
            this.configuration.UseComments = true;
            this.ReloadTargetService();

            return this;
        }

        public ITargetBuilder SetAttributePropagationAlgorithm(IAttributePropagationAlgorithm propagationAlgorithm)
        {
            this.Services.Remove(this.Services.FirstOrDefault(service => service.ServiceType == typeof(IAttributePropagationAlgorithm)));
            this.Services.AddSingleton(propagationAlgorithm);

            this.ReloadTargetService();

            return this;
        }

        /// <summary>
        /// Reloads the target's service collection.
        /// </summary>
        private void ReloadTargetService()
        {
            this.Services.Remove(this.Services.FirstOrDefault(service => service.ServiceType == typeof(ITargetConfiguration)));
            this.Services.AddSingleton<ITargetConfiguration, TargetConfiguration>(p => this.configuration);
        }
    }
}
