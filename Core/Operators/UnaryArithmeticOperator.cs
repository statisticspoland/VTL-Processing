namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System;

    /// <summary>
    /// Unary arithmetic operator definition class.
    /// </summary>
    [OperatorSymbol("minus", "plus")]
    public class UnaryArithmeticOperator : IOperatorDefinition
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="UnaryArithmeticOperator"/> class.
        /// </summary>
        /// <param name="symbol">Symbol of the operator.</param>
        public UnaryArithmeticOperator(string symbol)
        {
            this.Symbol = symbol;
        }

        public string Name => "Unary Arithmetic";

        public string Symbol { get; }

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            throw new NotImplementedException();
        }
    }
}
