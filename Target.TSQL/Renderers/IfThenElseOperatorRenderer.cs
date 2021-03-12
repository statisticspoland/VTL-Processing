namespace StatisticsPoland.VtlProcessing.Target.TSQL.Renderers
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System.Linq;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Renderers.Interfaces;

    /// <summary>
    /// The TSQL code renderer for the "If-then-else" operator.
    /// </summary>
    [OperatorRendererSymbol("if")]
    internal sealed class IfThenElseOperatorRenderer : IOperatorRenderer
    {
        private readonly OperatorRendererResolver opRendererResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="IfThenElseOperatorRenderer"/> class.
        /// </summary>
        /// <param name="opRendererResolver">The operator renderer resolver.</param>
        public IfThenElseOperatorRenderer(OperatorRendererResolver opRendererResolver)
        {
            this.opRendererResolver = opRendererResolver;
        }

        public string Render(IExpression expr, StructureComponent component)
        {
            if (!expr.IsScalar && component == null) return this.opRendererResolver("overall").Render(expr, component);

            string ifExprRenderSuffix = string.Empty;
            if (expr.Operands["if"].OperandsCollection.First().OperatorSymbol.In("ref", "const", "#", "comp")) ifExprRenderSuffix = " = 1";
            return $"IIF({this.RenderBranch(expr.Operands["if"], expr.Operands["if"].Structure.Measures[0])}{ifExprRenderSuffix}, {this.RenderBranch(expr.Operands["then"], component)}, {this.RenderBranch(expr.Operands["else"], component)})";
        }

        /// <summary>
        /// Renders a branch of the "if-then-else" operator TSQL translated code.
        /// </summary>
        /// <param name="expr">The expression which parameters shall be used to render.</param>
        /// <param name="component">The selected component to assign in the translated code.</param>
        /// <returns>The TSQL translated code.</returns>
        private string RenderBranch(IExpression expr, StructureComponent component)
        {
            if (expr.IsScalar)
            {
                if (expr.ResultName == "If" && expr.CurrentJoinExpr != null) component = null;
                return this.opRendererResolver(expr.Operands["ds_1"].OperatorSymbol).Render(expr.Operands["ds_1"], component);
            }

            return $"{expr.OperandsCollection.First().ExpressionText}.{component.ComponentName}";
        }
    }
}
