namespace StatisticsPoland.VtlProcessing.Target.PlantUML.Infrastructure
{
    using Interfaces;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// The builder of the PlantUML target renderer.
    /// </summary>
    internal sealed class TargetBuilder : ITargetBuilder
    {
        private readonly ITargetConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetConfiguration"/> class.
        /// </summary>
        public TargetBuilder()
        {
            this._configuration = new TargetConfiguration();
        }

        public ITargetBuilder AddDataStructureObject()
        {
            this._configuration.ShowDataStructure = true;

            return this;
        }

        public ITargetBuilder UseHorizontalView()
        {
            this._configuration.UseHorizontalView = true;

            return this;
        }

        public ITargetBuilder UseArrowFirstToLast()
        {
            this._configuration.LineConnection = "-->";

            return this;
        }

        public ITargetBuilder UseArrowLastToFirst()
        {
            this._configuration.LineConnection = "<--";

            return this;
        }
        public ITargetBuilder ShowNumberLine()
        {
            this._configuration.ShowNumberLine = true;

            return this;
        }

        public ITargetBuilder UseRuleExpressionsModel()
        {
            this._configuration.ExprType = ExpressionsType.Rulesets;

            return this;
        }

        /// <summary>
        /// Updates a service collection by adding the target configuration set by the builder.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public void UpdateServices(IServiceCollection services)
        {
            services.AddScoped(p => this._configuration);
        }
    }
}
