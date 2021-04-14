namespace StatisticsPoland.VtlProcessing.Target.TSQL.Renderers
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Renderers.Interfaces;

    /// <summary>
    /// The TSQL code renderer for the "Get" operator.
    /// </summary>
    [OperatorRendererSymbol("get")]
    internal sealed class GetOperatorRenderer : IOperatorRenderer
    {
        private readonly IEnvironmentMapper envMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetOperatorRenderer"/> class.
        /// </summary>
        /// <param name="envMapper">The environment names mapper.</param>
        public GetOperatorRenderer(IEnvironmentMapper envMapper)
        {
            this.envMapper = envMapper;
        }

        public string Render(IExpression expr, StructureComponent component)
        {
            if (component != null) return component.BaseComponentName;
            if (expr.ParentExpression == null) return $"SELECT * FROM {this.envMapper.Map(expr.ExpressionText)}";
            return this.envMapper.Map(expr.ExpressionText);
        }
    }
}
