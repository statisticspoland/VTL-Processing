namespace StatisticsPoland.VtlProcessing.Core.Models.Types
{
    /// <summary>
    /// The expression factory target where the name should be assigned to.
    /// </summary>
    public enum ExpressionFactoryNameTarget
    {
        /// <summary>
        /// The assignation to the expression's result name.
        /// </summary>
        ResultName = 0,

        /// <summary>
        /// The assignation to the expression's operator symbol.
        /// </summary>
        OperatorSymbol = 1
    }
}
