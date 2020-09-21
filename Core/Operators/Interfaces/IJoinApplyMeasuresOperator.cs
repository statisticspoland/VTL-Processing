namespace StatisticsPoland.VtlProcessing.Core.Operators.Interfaces
{
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;

    /// <summary>
    /// Definition of a component version of any operator interface.
    /// </summary>
    public interface IJoinApplyMeasuresOperator
    {
        /// <summary>
        /// Gets the structure of the resulting component version of an operator parameter for a given expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>A dynamically defined structure.</returns>
        IDataStructure GetMeasuresStructure(IExpression expression);
    }
}
