namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System;
    using System.Linq;

    /// <summary>
    /// Reference operator definition class.
    /// </summary>
    [OperatorSymbol("ref")]
    public class ReferenceOperator : IOperatorDefinition
    {
        public string Name => "Reference";

        public string Symbol => "ref";

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            throw new NotImplementedException();
        }
    }
}
