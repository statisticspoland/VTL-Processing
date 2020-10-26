namespace Target.TSQL.Renderers
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System.Linq;
    using Target.TSQL.Infrastructure;
    using Target.TSQL.Infrastructure.Attributes;
    using Target.TSQL.Renderers.Interfaces;

    /// <summary>
    /// The TSQL code renderer for the "Unary arithmetic" operator.
    /// </summary>
    [OperatorRendererSymbol("minus", "plus")]
    internal class UnaryArithmeticOperatorRenderer : IOperatorRenderer
    {
        private readonly OperatorRendererResolver opRendererResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnaryArithmeticOperatorRenderer"/> class.
        /// </summary>
        /// <param name="opRendererResolver">The operator renderer resolver.</param>
        public UnaryArithmeticOperatorRenderer(OperatorRendererResolver opRendererResolver)
        {
            this.opRendererResolver = opRendererResolver;
        }

        public string Render(IExpression expr, StructureComponent component)
        {
            if (!expr.IsScalar && component == null) return this.opRendererResolver("overall").Render(expr, component);

            IExpression operand = expr.OperandsCollection.First();

            string op = this.opRendererResolver(operand.OperatorSymbol).Render(operand, component);
            string symbol = expr.OperatorSymbol == "minus" ? "-" : string.Empty;
            string result = $"{symbol}{op}";

            if (symbol == "-" && !operand.OperatorSymbol.In("ref", "comp", "#")) result = $"{symbol}({op})";

            return result;
        }
    }
}
