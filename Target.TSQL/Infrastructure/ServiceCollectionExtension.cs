namespace Target.TSQL.Infrastructure
{
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Target.TSQL;
    using Target.TSQL.Infrastructure.Attributes;
    using Target.TSQL.Infrastructure.Interfaces;
    using Target.TSQL.Preparers;
    using Target.TSQL.Preparers.Interfaces;
    using Target.TSQL.Renderers;
    using Target.TSQL.Renderers.Interfaces;

    /// <summary>
    /// Initializes a new instance of the <see cref="IOperatorRenderer"/> interface.
    /// </summary>
    /// <param name="key">The operator key.</param>
    public delegate IOperatorRenderer OperatorRendererResolver(string key);

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddTsqlTarget(this IServiceCollection services)
        {
            services.AddSingleton<IMapper, Mapper>();
            services.AddTransient<IJoinSelectBuilder, JoinSelectBuilder>();
            services.AddSingleton<ITargetRenderer, TsqlTargetRenderer>();
            services.AddSingleton<IReferencesManager, ReferencesManager>();
            services.AddSingleton<ITargetConfiguration, TargetConfiguration>();
            services.AddSingleton<TsqlTargetRenderer>();
            services.AddSingleton<TemporaryTables>();
            services.AddSingleton<IAttributePropagationAlgorithm>(new AttributePropagationAlgorithm());

            Assembly executingAssembly = Assembly.GetExecutingAssembly();

            IEnumerable<Type> Operators = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(p => typeof(IOperatorRenderer).IsAssignableFrom(p) && p.IsClass);

            foreach (Type type in Operators)
            {
                services.AddTransient(type);
            }

            services.AddTransient<OperatorRendererResolver>(ServiceProvider => key =>
            {
                Type type = executingAssembly.GetTypes().SingleOrDefault(t => t.GetCustomAttribute<OperatorRendererSymbol>(true)?.Symbols.Contains(key) == true);

                if (type == null) return null;
                if (type == typeof(GetOperatorRenderer)) return new GetOperatorRenderer(ServiceProvider.GetService<ITargetConfiguration>());

                return (IOperatorRenderer)ServiceProvider
                .GetService(type);
            });

            return services;
        }

        public static IServiceCollection AddTsqlTarget(this IServiceCollection services, Action<ITargetBuilder> configure)
        {
            services.AddTsqlTarget();
            configure(new TargetBuilder(services));
            return services;
        }
    }
}
