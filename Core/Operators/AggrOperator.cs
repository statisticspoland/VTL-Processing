namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;

    /// <summary>
    /// The "Aggregation" operator definition.
    /// </summary>
    [OperatorSymbol("aggr")]
    public class AggrOperator : IOperatorDefinition
    {
        public string Name => "Aggregation";

        public string Symbol { get; set; } = "aggr";

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            return expression.Operands["calc"].Structure.GetCopy();
        }
    }
}
