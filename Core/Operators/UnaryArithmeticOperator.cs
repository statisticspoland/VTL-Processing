namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.MiddleEnd.Tools;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System.Linq;

    /// <summary>
    /// The "Unary arithmetic" operator definition.
    /// </summary>
    [OperatorSymbol("minus", "plus")]
    public class UnaryArithmeticOperator : IOperatorDefinition
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="UnaryArithmeticOperator"/> class.
        /// </summary>
        /// <param name="symbol">The symbol of the operator.</param>
        public UnaryArithmeticOperator(string symbol)
        {
            this.Symbol = symbol;
        }

        public string Name => "Unary arithmetic";

        public string Symbol { get; }

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            IExpression expr = expression.OperandsCollection.ToArray()[0];

            if (!NumericStructure.IsNumericStructure(expr.Structure, true)) throw new VtlOperatorError(expression, this.Name, "Excpected numeric expression.");

            return expr.Structure.GetCopy();
        }
    }
}
