namespace StatisticsPoland.VtlProcessing.Target.PlantUML.Infrastructure
{
    using Interfaces;
    using Microsoft.Extensions.DependencyInjection;

    internal sealed class TargetBuilder : ITargetBuilder
    {
        private readonly ITargetConfiguration _configuration;

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
            this._configuration.Arrow = "-->";

            return this;
        }

        public ITargetBuilder UseArrowLastToFirst()
        {
            this._configuration.Arrow = "<--";

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

        public void UpdateServices(IServiceCollection services)
        {
            services.AddScoped(p => this._configuration);
        }
    }
}
