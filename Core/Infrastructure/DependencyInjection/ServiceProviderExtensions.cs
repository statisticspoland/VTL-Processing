namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using StatisticsPoland.VtlProcessing.Core.FrontEnd.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using System.Linq;

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

        public static ITargetRenderer GetTargetRenderer(this ServiceProvider provider, string name)
        {
            return provider.GetServices<ITargetRenderer>().FirstOrDefault(tr => tr.Name == name);
        }
    }
}
