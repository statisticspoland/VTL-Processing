namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System;

    /// <summary>
    /// Arithmetic operator definition class.
    /// </summary>
    [OperatorSymbol("+", "-", "*", "/")]
    public class ArithmeticOperator : IOperatorDefinition
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ArithmeticOperator"/> class.
        /// </summary>
        /// <param name="symbol">Symbol of the operator.</param>
        public ArithmeticOperator(string symbol)
        {
            this.Symbol = symbol;
        }

        public string Name => "Arithmetic";

        public string Symbol { get; }

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            throw new NotImplementedException();
        }
    }
}
