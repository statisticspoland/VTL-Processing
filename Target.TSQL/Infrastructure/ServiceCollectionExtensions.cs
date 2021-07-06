namespace StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure
{
    using Attributes;
    using Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using Preparers;
    using Preparers.Interfaces;
    using Renderers.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Initializes a new instance of the <see cref="IOperatorRenderer"/> interface.
    /// </summary>
    /// <param name="key">The operator key.</param>
    public delegate IOperatorRenderer OperatorRendererResolver(string key);

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTsqlTarget(this IServiceCollection services, Action<ITargetBuilder> config = null)
        {
            services.AddScoped<ITargetRenderer, TsqlTargetRenderer>();
            services.AddScoped<IMapper, Mapper>();
            services.AddScoped<IReferencesManager, ReferencesManager>();
            services.AddScoped<TemporaryTables>();

            services.AddTransient<IJoinSelectBuilder, JoinSelectBuilder>();

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
                Type type = executingAssembly.GetTypes().SingleOrDefault(t => t.GetCustomAttribute<OperatorRendererSymbolAttribute>(true)?.Symbols.Contains(key) == true);

                if (type == null) return null;

                return (IOperatorRenderer)ServiceProvider.GetService(type);
            });

            TargetBuilder configuration = new TargetBuilder();
            if (config != null) config(configuration);

            configuration.UpdateServices(services);

            return services;
        }
    }
}
