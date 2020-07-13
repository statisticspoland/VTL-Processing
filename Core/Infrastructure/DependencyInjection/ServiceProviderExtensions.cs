namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.FrontEnd.Interfaces;

    public static class ServiceProviderExtensions
    {
        public static ITreeGenerator GetFrontEnd(this ServiceProvider provider)
        {
            return provider.GetService<ITreeGenerator>();
        }
    }
}
