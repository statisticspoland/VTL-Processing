namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System;

    /// <summary>
    /// Component operator definition class.
    /// </summary>
    [OperatorSymbol("comp")]
    public class ComponentOperator : IOperatorDefinition
    {
        public string Name => "Component";

        public string Symbol => "comp";

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            throw new NotImplementedException();
        }
    }
}