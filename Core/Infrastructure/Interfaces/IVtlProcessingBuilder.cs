namespace StatisticsPoland.VtlProcessing.Core.Infrastructure
{
    using Microsoft.Extensions.DependencyInjection;

    public interface IVtlProcessingBuilder
    {
        IServiceCollection Services { get; }

        IVtlProcessingBuilder Empty();
    }
}