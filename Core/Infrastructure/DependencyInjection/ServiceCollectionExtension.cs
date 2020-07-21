namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.FrontEnd;
    using StatisticsPoland.VtlProcessing.Core.FrontEnd.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Transformations;
    using StatisticsPoland.VtlProcessing.Core.Transformations.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddVtlProcessing(this IServiceCollection services, Action<IVtlProcessingBuilder> configure)
        {
            services.AddSingleton<ITreeGenerator, TreeGenerator>();
            services.AddSingleton<ITreeTransformer, VisitorTransformer>();
            services.AddSingleton<IExpressionFactory, ExpressionFactory>();

            services.AddResolvers();

            IEnumerable<Type> Operators =
                Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(p => typeof(IOperatorDefinition).IsAssignableFrom(p) && p.IsClass);

            foreach (Type type in Operators)
            {
                services.AddTransient(type);
            }

            //additional configuration (e.g. register data model)
            IVtlProcessingBuilder configBuilder = new VtlProcessingBuilder(services);
            configure(configBuilder);

            return configBuilder.Services;
        }
    }
}
