namespace StatisticsPoland.VtlProcessing.Core.Models.Types
{
    /// <summary>
    /// The expression factory target where a name should be assigned to.
    /// </summary>
    public enum ExpressionFactoryNameTarget
    {
        /// <summary>
        /// The assignation to an expression's result name.
        /// </summary>
        ResultName = 0,

        /// <summary>
        /// The assignation to an expression's operator symbol.
        /// </summary>
        OperatorSymbol = 1
    }
}
