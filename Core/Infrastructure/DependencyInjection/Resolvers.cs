namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Logical;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Initializes new instance of the <see cref="IDataStructure"/> interface <b>for a single component structure</b>.
    /// </summary>
    /// <param name="compName">The name of component.</param>
    /// <param name="compType">The component type.</param>
    /// <param name="dataType">The data type of component.</param>
    public delegate IDataStructure DataStructureResolver(string compName = null, ComponentType? compType = null, BasicDataType? dataType = null);

    /// <summary>
    /// Initialises a new instance of the <see cref="IExpression"/> interface.
    /// </summary>
    /// <param name="parentExpr">The parent expression.</param>
    public delegate IExpression ExpressionResolver(IExpression parentExpr = null);

    /// <summary>
    /// Initialises a new instance of the <see cref="IJoinExpression"/> interface.
    /// </summary>
    /// <param name="expression">The base expression with a "join" operator.</param>
    public delegate IJoinExpression JoinExpressionResolver(IExpression expression);

    /// <summary>
    /// Initialises a new instance of the <see cref="ITransformationSchema"/> interface.
    /// </summary>
    public delegate ITransformationSchema TransformationSchemaResolver();

    /// <summary>
    /// Initializes a new instance of the <see cref="IOperatorDefinition"/> interface.
    /// </summary>
    /// <param name="key">Operator key.</param>
    public delegate IOperatorDefinition OperatorResolver(string key);

    /// <summary>
    /// The dependency injection resolvers class.
    /// </summary>
    public static class Resolvers
    {
        /// <summary>
        /// Adds resolvers to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <returns>The services collection.</returns>
        public static IServiceCollection AddResolvers(this IServiceCollection services)
        {
            services.AddTransient<DataStructureResolver>(ServiceProvider => (compName, compType, dataType) =>
            {
                if (compName == null && compType == null && dataType == null) return new DataStructure();
                else if (compName == null || compType == null || dataType == null) throw new Exception("DataStructureResolver expects 0 or 3 nullable arguments");
                return new DataStructure(compName, (ComponentType)compType, (BasicDataType)dataType);
            });

            services.AddTransient<ExpressionResolver>(ServiceProvider => parentExpr => 
            {
                return new Expression(parentExpr);
            });

            services.AddTransient<JoinExpressionResolver>(ServiceProvider => expression =>
            {
                return new JoinExpression(expression);
            });

            services.AddTransient<TransformationSchemaResolver>(ServiceProvider => () =>
            {
                return new TransformationSchema();
            });

            services.AddTransient<OperatorResolver>(ServiceProvider => key =>
            {
                Type type = Assembly.GetExecutingAssembly().GetTypes().SingleOrDefault(t => t.GetCustomAttribute<OperatorSymbol>(true)?.Symbols.Contains(key) == true);
                
                if (type == typeof(ArithmeticOperator)) return new ArithmeticOperator(ServiceProvider.GetService<IJoinApplyMeasuresOperator>(), key);
                if (type == typeof(BooleanOperator)) return new BooleanOperator(ServiceProvider.GetService<IJoinApplyMeasuresOperator>(), ServiceProvider.GetService<DataStructureResolver>(), key);
                if (type == typeof(ComparisonOperator)) return new ComparisonOperator(ServiceProvider.GetService<IJoinApplyMeasuresOperator>(), ServiceProvider.GetService<DataStructureResolver>(), key);
                if (type == typeof(KeepDropOperator)) return new KeepDropOperator(ServiceProvider.GetService<DataStructureResolver>(), key);
                if (type == typeof(NumericOperator)) return new NumericOperator(ServiceProvider.GetService<IJoinApplyMeasuresOperator>(), ServiceProvider.GetService<DataStructureResolver>(), key);
                if (type == typeof(StringOperator)) return new StringOperator(ServiceProvider.GetService<IJoinApplyMeasuresOperator>(), ServiceProvider.GetService<DataStructureResolver>(), key);
                if (type == typeof(UnaryArithmeticOperator)) return new UnaryArithmeticOperator(ServiceProvider.GetService<IJoinApplyMeasuresOperator>(), key);
                if (type == null) throw new NotImplementedException($"Operator {key} is not implemented.");
                return (IOperatorDefinition)ServiceProvider.GetService(type);
            });

            return services;
        }
    }
}
