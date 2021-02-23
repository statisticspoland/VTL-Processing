namespace StatisticsPoland.VtlProcessing.Core.Models.Logical
{
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;

    /// <summary>
    /// The single VTL 2.0 ruie expression representation.
    /// </summary>
    public class RuleExpression : Expression, IRuleExpression
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="RuleExpression"/> class.
        /// </summary>
        /// <param name="expression">The base expression.</param>
        /// <param name="containingRuleset">The ruleset containing this rule expression.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorLevel">The error level.</param>
        public RuleExpression(IExpression expression, IRuleset containingRuleset, string errorCode = null, int? errorLevel = null) : base(expression.ParentExpression)
        {
            this.ContainingSchema = expression.ContainingSchema;
            this.ExpressionText = expression.ExpressionText;
            this.LineNumber = expression.LineNumber;
            this.OperandsCollection = expression.OperandsCollection;
            this.OperatorDefinition = expression.OperatorDefinition;
            this.ParamSignature = expression.ParamSignature;
            this.ReferenceExpression = expression.ReferenceExpression;
            this.ResultName = expression.ResultName;
            this.Structure = expression.Structure;

            this.ContainingRuleset = containingRuleset;
            this.ErrorCode = errorCode;
            this.ErrorLevel = errorLevel;
        }

        public IRuleset ContainingRuleset { get; set; }

        public string ErrorCode { get; set; }

        public int? ErrorLevel { get; set; }
    }
}
