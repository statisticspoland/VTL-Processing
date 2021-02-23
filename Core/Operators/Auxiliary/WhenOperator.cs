namespace StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;

    /// <summary>
    /// The "When" operator definition.
    /// </summary>
    [OperatorSymbol("when")]
    public class WhenOperator : IOperatorDefinition
    {
        public string Name => "When";

        public string Symbol => "when";

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            foreach (IExpression expr in expression.OperandsCollection)
            {
                if (!expr.IsScalar) throw new VtlOperatorError(expression, this.Symbol, "Expected scalar operands.");
                if (expr.Structure.Components[0].ValueDomain.DataType != BasicDataType.Boolean) throw new VtlOperatorError(expression, this.Symbol, "Expected boolean operands.");
            }

            return expression.Operands["when"].Structure.GetCopy();
        }
    }
}
