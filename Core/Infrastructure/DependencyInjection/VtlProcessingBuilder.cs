namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;

    public class VtlProcessingBuilder : IVtlProcessingBuilder
    {
        public VtlProcessingBuilder(IServiceCollection services)
        {
            this.Services = services;
        }

        public IServiceCollection Services { get; }

        public IVtlProcessingBuilder Empty()
        {
            return this;
        }
    }
}
