namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.FrontEnd;
    using StatisticsPoland.VtlProcessing.Core.FrontEnd.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Transformations;
    using StatisticsPoland.VtlProcessing.Core.Transformations.Interfaces;
    using System;

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddVtlProcessing(this IServiceCollection services, Action<IVtlProcessingBuilder> configure)
        {
            services.AddSingleton<ITreeGenerator, TreeGenerator>();
            services.AddSingleton<ITreeTransformer, VisitorTransformer>();

            services.AddSingleton<ITransformationSchema, TransformationSchema>();
            services.AddSingleton<IExpression, Expression>();
            
            // additional configuration (e.g. register data model)
            IVtlProcessingBuilder configBuilder = new VtlProcessingBuilder(services);
            configure(configBuilder);

            return configBuilder.Services;
        }
    }
}
