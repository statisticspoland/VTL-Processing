namespace Target.TSQL.Renderers
{
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using Target.TSQL.Infrastructure.Attributes;
    using Target.TSQL.Renderers.Interfaces;

    /// <summary>
    /// The TSQL code renderer for the "Current date" operator.
    /// </summary>
    [OperatorRendererSymbol("current_date")]
    internal sealed class CurrentDateOperatorRenderer : IOperatorRenderer
    {
        public string Render(IExpression expr, StructureComponent component)
        {
            return "CONVERT(VARCHAR, GETDATE(), 23)";
        }
    }
}
