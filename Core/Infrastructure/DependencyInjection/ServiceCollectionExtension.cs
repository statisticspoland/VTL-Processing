namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.FrontEnd;
    using StatisticsPoland.VtlProcessing.Core.FrontEnd.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.MiddleEnd.Modifiers;
    using StatisticsPoland.VtlProcessing.Core.MiddleEnd.Modifiers.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Transformations;
    using StatisticsPoland.VtlProcessing.Core.Transformations.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddVtlProcessing(this IServiceCollection services, Action<IVtlProcessingConfig> configure)
        {
            services.AddSingleton<ITreeGenerator, TreeGenerator>();
            services.AddSingleton<ITreeTransformer, VisitorTransformer>();
            services.AddSingleton<IExpressionFactory, ExpressionFactory>();
            services.AddSingleton<IDataModelAggregator>(new DataModelAggregator(null, null));

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

            services.AddSingleton<ISchemaModifiersApplier, SchemaModifiersApplier>();

            // middle end schema modifier chain
            services.AddSingleton<ISchemaModifier, DeadCodeModifier>();
            services.AddSingleton<ISchemaModifier, TypeInferenceModifier>();

            //additional configuration (e.g. register data model)
            IVtlProcessingConfig configBuilder = new VtlProcessingConfig(services);
            configure(configBuilder);

            return configBuilder.Services;
        }
    }
}
