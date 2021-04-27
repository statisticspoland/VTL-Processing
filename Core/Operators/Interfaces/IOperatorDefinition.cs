using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;

namespace StatisticsPoland.VtlProcessing.Core.Operators.Interfaces
{
    /// <summary>
    /// The operator definition interface.
    /// </summary>
    public interface IOperatorDefinition
    {
        /// <summary>
        /// Gets the name of operator.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets or sets the operator symbol.
        /// </summary>
        string Symbol { get; set; }

        /// <summary>
        /// Gets or sets the operator keyword.
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// Gets the structure of the resulting operator parameter for a given expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>A dynamically defined structure.</returns>
        IDataStructure GetOutputStructure(IExpression expression);
    }
}
