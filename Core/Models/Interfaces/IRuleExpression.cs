namespace StatisticsPoland.VtlProcessing.Core.Models.Interfaces
{
    /// <summary>
    /// The single VTL 2.0 ruie expression representation interface.
    /// </summary>
    public interface IRuleExpression : IExpression
    {
        /// <summary>
        /// Gets or sets the ruleset containing this rule expression.
        /// </summary>
        IRuleset ContainingRuleset { get; set; }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        string ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the error level.
        /// </summary>
        int? ErrorLevel { get; set; }
    }
}
