namespace Target.TSQL.Renderers
{
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using Target.TSQL.Infrastructure.Attributes;
    using Target.TSQL.Renderers.Interfaces;

    /// <summary>
    /// The TSQL code renderer for the "Optional" operator.
    /// </summary>
    [OperatorRendererSymbol("opt")]
    internal sealed class OptionalOperatorRenderer : IOperatorRenderer
    {
        public string Render(IExpression expr, StructureComponent component)
        {
            return expr.ExpressionText;
        }
    }
}
