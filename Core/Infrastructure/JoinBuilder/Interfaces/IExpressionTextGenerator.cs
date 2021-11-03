namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.Interfaces
{
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;

    /// <summary>
    /// Expression text generator interface for expressions rebuilded in a join builder.
    /// </summary>
    public interface IExpressionTextGenerator
    {
        /// <summary>
        /// Generates an expression text for a given expression.
        /// </summary>
        /// <param name="expr">The expression to generate an expression text for.</param>
        void Generate(IExpression expr);

        /// <summary>
        /// Generates an expression text for a given expression and its descendant expressions.
        /// </summary>
        /// <param name="expr">The expression to generate an expression text for.</param>
        void GenerateRecursively(IExpression expr);
    }
}
