namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System;

    /// <summary>
    /// Get operator definition class.
    /// </summary>
    [OperatorSymbol("get")]
    public class GetOperator : IOperatorDefinition
    {
        public string Name => "Get";

        public string Symbol => "get";

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            throw new NotImplementedException();
        }
    }
}
