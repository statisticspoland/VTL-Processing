namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.FrontEnd.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;

    public static class ServiceProviderExtensions
    {
        public static ITreeGenerator GetFrontEnd(this ServiceProvider provider)
        {
            return provider.GetService<ITreeGenerator>();
        }

        public static IExpressionFactory GetExpressionFactory(this ServiceProvider provider)
        {
            return provider.GetService<IExpressionFactory>();
        }
    }
}
