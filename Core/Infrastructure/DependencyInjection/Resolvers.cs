﻿namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection
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
    /// Initialises a new instance of the <see cref="ITransformationSchema"/> interface.
    /// </summary>
    public delegate ITransformationSchema TransformationSchemaResolver();

    /// <summary>
    /// Initializes a new instance of the <see cref="IOperatorDefinition"/> interface.
    /// </summary>
    /// <param name="key">Operator key.</param>
    public delegate IOperatorDefinition OperatorResolver(string key);

    /// <summary>
    /// The DI resolvers class.
    /// </summary>
    public static class Resolvers
    {
        /// <summary>
        /// Adds a resolvers to the specified <see cref="IServiceCollection"/>.
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

            services.AddTransient<TransformationSchemaResolver>(ServiceProvider => () =>
            {
                return new TransformationSchema();
            });

            services.AddTransient<OperatorResolver>(ServiceProvider => key =>
            {
                Type type = Assembly.GetExecutingAssembly().GetTypes().SingleOrDefault(t => t.GetCustomAttribute<OperatorSymbol>(true)?.Symbols.Contains(key) == true);
                
                if (type == typeof(ArithmeticOperator)) return new ArithmeticOperator(key);
                if (type == typeof(NumericOperator)) return new NumericOperator(key);
                if (type == typeof(StringOperator)) return new StringOperator(key);
                if (type == typeof(UnaryArithmeticOperator)) return new UnaryArithmeticOperator(key);
                if (type == null) throw new NotImplementedException($"Operator {key} is not implemented.");
                return (IOperatorDefinition)ServiceProvider.GetService(type);
            });

            return services;
        }
    }
}