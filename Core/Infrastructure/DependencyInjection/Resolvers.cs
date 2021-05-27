namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
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
    /// Initializes a new instance of the <see cref="IDataStructure"/> interface <b>for a single component structure</b>.
    /// </summary>
    /// <param name="compName">The name of component.</param>
    /// <param name="compType">The component type.</param>
    /// <param name="dataType">The data type of component.</param>
    public delegate IDataStructure DataStructureResolver(string compName = null, ComponentType? compType = null, BasicDataType? dataType = null);

    /// <summary>
    /// Initialises a new instance of the <see cref="IRuleset"/> interface.
    /// </summary>
    /// <param name="name">The name of the ruleset.</param>
    /// <param name="rulesetText">The text of the ruleset.</param>
    public delegate IRuleset DatapointRulesetResolver(string name, string rulesetText);

    /// <summary>
    /// Initialises a new instance of the <see cref="IExpression"/> interface.
    /// </summary>
    /// <param name="parentExpr">The parent expression.</param>
    public delegate IExpression ExpressionResolver(IExpression parentExpr = null);

    /// <summary>
    /// Initialises a new instance of the <see cref="IRuleExpression"/> interface.
    /// </summary>
    /// <param name="expression">The base expression.</param>
    /// <param name="containingRuleset">The ruleset containing this rule expression.</param>
    /// <param name="errorCode">The error code.</param>
    /// <param name="errorLevel">The error level.</param>
    public delegate IRuleExpression RuleExpressionResolver(IExpression expression, IRuleset containingRuleset, string errorCode = null, int? errorLevel = null);

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
    /// <param name="key">The operator key.</param>
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
            services.AddScoped<DataStructureResolver>(ServiceProvider => (compName, compType, dataType) =>
            {
                if (compName == null && compType == null && dataType == null) return new DataStructure(ServiceProvider.GetService<ILogger<IDataStructure>>());
                else if (compName == null || compType == null || dataType == null) throw new ArgumentNullException(string.Empty, "DataStructureResolver expects 0 or 3 nullable arguments");
                return new DataStructure(compName, (ComponentType)compType, (BasicDataType)dataType, ServiceProvider.GetService<ILogger<IDataStructure>>());
            });

            services.AddScoped<DatapointRulesetResolver>(ServiceProvider => (name, rulesetText) =>
            {
                return new DatapointRuleset(name, rulesetText, ServiceProvider.GetService<DataStructureResolver>());
            });

            services.AddScoped<ExpressionResolver>(ServiceProvider => parentExpr => 
            {
                return new Expression(parentExpr);
            });

            services.AddScoped<RuleExpressionResolver>(ServiceProvider => (expression, containingRuleset, errorCode, errorLevel) =>
            {
                return new RuleExpression(expression, containingRuleset, errorCode, errorLevel);
            });

            services.AddScoped<JoinExpressionResolver>(ServiceProvider => expression =>
            {
                return new JoinExpression(expression);
            });

            services.AddScoped<TransformationSchemaResolver>(ServiceProvider => () =>
            {
                return new TransformationSchema();
            });

            services.AddScoped<OperatorResolver>(ServiceProvider => key =>
            {
                Type type = Assembly.GetExecutingAssembly().GetTypes().SingleOrDefault(t => t.GetCustomAttribute<OperatorSymbol>(true)?.Symbols.Contains(key) == true);

                if (type == null) throw new NotImplementedException($"Operator {key} is not implemented.");
                
                IOperatorDefinition op = (IOperatorDefinition)ServiceProvider.GetService(type);
                if (op.Symbol == null) op.Symbol = key;
                return op;
            });

            return services;
        }
    }
}
