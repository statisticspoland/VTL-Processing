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
        /// <value>
        /// The operator name.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Gets the operator symbol.
        /// </summary>
        /// <value>
        /// The operator symbol.
        /// </value>
        string Symbol { get; }

        /// <summary>
        /// Gets or sets the operator keyword.
        /// </summary>
        /// <value>
        /// The operator keyword.
        /// </value>
        public string Keyword { get; set; }

        /// <summary>
        /// Gets the structure of the resulting operator parameter for specified operands.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The dynamically defined structure of the output parameter for the given input parameters.</returns>
        IDataStructure GetOutputStructure(IExpression expression);
    }
}
