namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.MiddleEnd.Utilities;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System.Linq;

    /// <summary>
    /// The "Unary arithmetic" operator definition.
    /// </summary>
    [OperatorSymbol("minus", "plus")]
    public class UnaryArithmeticOperator : IOperatorDefinition
    {
        private readonly IJoinApplyMeasuresOperator _joinApplyMeasuresOp;

        /// <summary>
        /// Initialises a new instance of the <see cref="UnaryArithmeticOperator"/> class.
        /// </summary>
        /// <param name="joinApplyMeasuresOp">The join apply measure operator.</param>
        /// <param name="symbol">The symbol of the operator.</param>
        public UnaryArithmeticOperator(IJoinApplyMeasuresOperator joinApplyMeasuresOp)
        {
            this._joinApplyMeasuresOp = joinApplyMeasuresOp;
        }

        public string Name => "Unary arithmetic";

        public string Symbol { get; set; }

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            if (expression.IsApplyComponent) return this._joinApplyMeasuresOp.GetMeasuresStructure(expression);

            IExpression expr = expression.OperandsCollection.ToArray()[0];

            if (!expr.Structure.IsNumericStructure(true)) throw new VtlOperatorError(expression, this.Name, "Excpected numeric expression.");

            return expr.Structure.GetCopy();
        }
    }
}
