namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;

    /// <summary>
    /// The expression factory interface.
    /// </summary>
    public interface IExpressionFactory
    {
        /// <summary>
        /// Gets the expression resolver.
        /// </summary>
        ExpressionResolver ExprResolver { get; }

        /// <summary>
        /// Gets the rule expression resolver.
        /// </summary>
        RuleExpressionResolver RuleExprResolver { get; }

        /// <summary>
        /// Gets the join expression resolver.
        /// </summary>
        JoinExpressionResolver JoinExprResolver { get; }

        /// <summary>
        /// Gets the operator resolver.
        /// </summary>
        OperatorResolver OperatorResolver { get; }

        /// <summary>
        /// Gets a new <see cref="IExpression"/> object with an assigned field.
        /// </summary>
        /// <param name="name">The value to assign.</param>
        /// <param name="field">The field to assign the value to.</param>
        /// <returns>The new expression object.</returns>
        IExpression GetExpression(string name, ExpressionFactoryNameTarget field);
    }
}
