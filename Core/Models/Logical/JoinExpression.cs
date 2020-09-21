namespace StatisticsPoland.VtlProcessing.Core.Models.Logical
{
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;

    public class JoinExpression : Expression, IJoinExpression
    {
        public JoinExpression(IExpression expression) : base(expression.ParentExpression)
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
        }
    }
}
