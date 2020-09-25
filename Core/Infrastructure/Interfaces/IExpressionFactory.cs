namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;

    /// <summary>
    /// Expression factory interface.
    /// </summary>
    public interface IExpressionFactory
    {
        /// <summary>
        /// Gets expression resolver.
        /// </summary>
        public ExpressionResolver ExprResolver { get; }

        /// <summary>
        /// Gets the join expression resolver.
        /// </summary>
        JoinExpressionResolver JoinExprResolver { get; }

        /// <summary>
        /// Gets operator resolver.
        /// </summary>
        OperatorResolver OperatorResolver { get; }

        /// <summary>
        /// Gets a new <see cref="IExpression"/> object with assigned field.
        /// </summary>
        /// <param name="name">Value to assign.</param>
        /// <param name="field">Field to assign the value to.</param>
        /// <returns>New expression object.</returns>
        IExpression GetExpression(string name, ExpressionFactoryNameTarget field);
    }
}
