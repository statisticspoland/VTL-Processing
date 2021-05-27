namespace StatisticsPoland.VtlProcessing.Target.TSQL.Renderers
{
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Renderers.Interfaces;

    /// <summary>
    /// The TSQL code renderer for the "Optional" operator.
    /// </summary>
    [OperatorRendererSymbol("opt")]
    internal sealed class OptionalOperatorRenderer : IOperatorRenderer
    {
        public string Render(IExpression expr, StructureComponent component = null)
        {
            return expr.ExpressionText;
        }
    }
}
