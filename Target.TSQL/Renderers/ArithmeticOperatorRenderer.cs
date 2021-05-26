namespace StatisticsPoland.VtlProcessing.Target.TSQL.Renderers
{
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System.Linq;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Renderers.Interfaces;

    /// <summary>
    /// The TSQL code renderer for the "Arithmetic" operator.
    /// </summary>
    [OperatorRendererSymbol("+", "-", "*", "/")]
    internal sealed class ArithmeticOperatorRenderer : IOperatorRenderer
    {
        private readonly OperatorRendererResolver _opRendererResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArithmeticOperatorRenderer"/> class.
        /// </summary>
        /// <param name="opRendererResolver">The operator renderer resolver.</param>
        public ArithmeticOperatorRenderer(OperatorRendererResolver opRendererResolver)
        {
            this._opRendererResolver = opRendererResolver;
        }

        public string Render(IExpression expr, StructureComponent component)
        {
            if (!expr.IsScalar && !expr.IsApplyComponent && component == null) return this._opRendererResolver("overall").Render(expr, component);

            IExpression expr1 = expr.OperandsCollection.ToArray()[0];
            IExpression expr2 = expr.OperandsCollection.ToArray()[1];

            string op1 = this._opRendererResolver(expr1.OperatorSymbol).Render(expr1, component);
            string op2 = this._opRendererResolver(expr2.OperatorSymbol).Render(expr2, component);

            return $"{op1} {expr.OperatorSymbol} {op2}";
        }
    }
}
