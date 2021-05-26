namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System.Linq;

    /// <summary>
    /// The "Reference" operator definition.
    /// </summary>
    [OperatorSymbol("ref")]
    public class ReferenceOperator : IOperatorDefinition
    {
        public string Name => "Reference";

        public string Symbol { get; set; } = "ref";

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            IExpression joinExpr = expression.CurrentJoinExpr;

            if (expression.IsScalar &&
                expression.GetFirstAncestorExpr("Apply") != null && 
                joinExpr.Operands["ds"].OperandsCollection.FirstOrDefault(alias => alias.ParamSignature == expression.ExpressionText) == null)
                    throw new VtlOperatorError(expression, this.Name, $"Wrong use of scalar reference operator expression: {expression.ExpressionText}");

            IDataStructure structure = expression.ReferenceExpression?.Structure.GetCopy(true);

            if (expression.GetFirstAncestorExpr("Apply") != null && structure != null)
            {
                structure.DatasetName = null;
                structure.Identifiers.Clear();
                structure.ViralAttributes.Clear();
                structure.NonViralAttributes.Clear();
            }

            return structure;
        }
    }
}
