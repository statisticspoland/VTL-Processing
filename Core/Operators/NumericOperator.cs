namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System;

    /// <summary>
    /// Numeric scalar operator definition class.
    /// </summary>
    [OperatorSymbol("ceil", "floor", "abs", "exp", "ln", "sqrt", "mod", "round", "power", "log", "trunc")]
    public class NumericOperator : IOperatorDefinition
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="NumericOperator"/> class.
        /// </summary>
        /// <param name="symbol">Symbol of the operator.</param>
        public NumericOperator(string symbol)
        {
            this.Symbol = symbol;
        }

        public string Name => "Numeric";

        public string Symbol { get; }

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            throw new NotImplementedException();
        }
    }
}
