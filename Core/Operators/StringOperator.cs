namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System;

    /// <summary>
    /// String operator definition class.
    /// </summary>
    [OperatorSymbol("||", "trim", "rtrim", "ltrim", "upper", "lower", "substr", "replace", "instr", "length")]
    public class StringOperator : IOperatorDefinition
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="StringOperator"/> class.
        /// </summary>
        /// <param name="symbol">Symbol of the operator.</param>
        public StringOperator(string symbol)
        {
            this.Symbol = symbol;
        }

        public string Name => "String";

        public string Symbol { get; }

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            throw new NotImplementedException();
        }
    }
}
