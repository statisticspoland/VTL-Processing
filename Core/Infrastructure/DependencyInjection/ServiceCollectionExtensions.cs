namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.FrontEnd;
    using StatisticsPoland.VtlProcessing.Core.FrontEnd.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.JoinBranches;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.JoinBranches.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.MiddleEnd.Modifiers;
    using StatisticsPoland.VtlProcessing.Core.MiddleEnd.Modifiers.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.MiddleEnd.Utilities;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Modifiers.Utilities.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary.ComponentManagement;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Transformations;
    using StatisticsPoland.VtlProcessing.Core.Transformations.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// The <see cref="IServiceCollection"/> extensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the VtlProcessing services collection.
        /// </summary>
        /// <param name="services">The service collection to add the VtlProcessing services collection to.</param>
        /// <returns>The service collection.</returns>
        internal static IServiceCollection AddVtlProcessing(this IServiceCollection services)
        {
            services.AddScoped<ITreeGenerator, TreeGenerator>();
            services.AddScoped<IExpressionTextGenerator, ExpressionTextGenerator>();
            services.AddScoped<IExpressionFactory, ExpressionFactory>();
            services.AddScoped<IJoinApplyMeasuresOperator, JoinApplyMeasuresOperator>();
            services.AddScoped<IComponentTypeInference, ComponentTypeInference>();
            services.AddScoped<IJoinBranch, ApplyBranch>();
            services.AddScoped<IJoinBranch, CalcBranch>();
            services.AddScoped<IJoinBranch, DsBranch>();
            services.AddScoped<IJoinBranch, RenameBranch>();
            services.AddScoped<IJoinBranch, UsingBranch>();

            services.AddTransient<ITreeTransformer, VisitorTransformer>();
            services.AddTransient<IJoinBuilder, JoinBuilder>();

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

            services.AddScoped<ISchemaModifiersApplier, SchemaModifiersApplier>();

            // middle end schema modifier chain
            services.AddScoped<ISchemaModifier, DeadCodeModifier>();
            services.AddScoped<ISchemaModifier, TypeInferenceModifier>();
            services.AddScoped<ISchemaModifier, JoinUsingFillingModifier>();
            services.AddScoped<ISchemaModifier, DsOperatorsToJoinsModifier>();

            return services;
        }

        /// <summary>
        /// Adds the VtlProcessing services collection.
        /// </summary>
        /// <param name="services">The service collection to add the VtlProcessing services collection to.</param>
        /// <param name="config">The configuration of a VtlProcessing VTL 2.0 translator.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddVtlProcessing(this IServiceCollection services, Action<IVtlProcessingConfig> config)
        {
            services.AddVtlProcessing();

            IVtlProcessingConfig configuration = new VtlProcessingConfig();
            if (config != null) config(configuration);

            IDataModelAggregator dataModelAggregator = configuration.DataModels;
            
            services.AddScoped(p => dataModelAggregator);
            services.AddScoped<IDataModelProvider>(p => dataModelAggregator);
            services.AddScoped(p => configuration.EnvironmentMapper);

            return services;
        }
    }
}
