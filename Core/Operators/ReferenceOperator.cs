namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;

    /// <summary>
    /// The "Reference" operator definition.
    /// </summary>
    [OperatorSymbol("ref")]
    public class ReferenceOperator : IOperatorDefinition
    {
        public string Name => "Reference";

        public string Symbol => "ref";

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            if (expression.IsScalar) throw new VtlOperatorError(expression, this.Name, $"Wrong use of scalar reference operator expression: {expression.ExpressionText}");

            return expression.ReferenceExpression?.Structure.GetCopy(true);
        }
    }
}
