namespace Target.PlantUML.Infrastructure
{
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;
    using Target.PlantUML.Infrastructure.Interfaces;

    public sealed class TargetBuilder : ITargetBuilder
    {
        private TargetConfiguration configuration;

        public TargetBuilder(IServiceCollection services)
        {
            this.Services = services;
            this.configuration = new TargetConfiguration();
        }

        public IServiceCollection Services { get; }

        public ITargetBuilder AddDataStructureObject()
        {
            this.configuration.ShowDataStructure = true;
            this.ReloadTargetService();

            return this;
        }

        public ITargetBuilder UseHorizontalView()
        {
            this.configuration.UseHorizontalView = true;
            this.ReloadTargetService();

            return this;
        }

        public ITargetBuilder UseArrowFirstToLast()
        {
            this.configuration.Arrow = "-->";
            this.ReloadTargetService();

            return this;
        }

        public ITargetBuilder UseArrowLastToFirst()
        {
            this.configuration.Arrow = "<--";
            this.ReloadTargetService();

            return this;
        }
        public ITargetBuilder ShowNumberLine()
        {
            this.configuration.ShowNumberLine = true;
            this.ReloadTargetService();

            return this;
        }

        public ITargetBuilder UseRuleExpressionsModel()
        {
            this.configuration.ExprType = ExpressionsType.Rulesets;
            this.ReloadTargetService();

            return this;
        }

        private void ReloadTargetService()
        {
            this.Services.Remove(this.Services.FirstOrDefault(service => service.ServiceType == typeof(TargetConfiguration)));
            this.Services.AddSingleton<ITargetConfiguration, TargetConfiguration>(p => this.configuration);
        }
    }
}
