namespace StatisticsPoland.VtlProcessing.Target.TSQL.Renderers
{
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Renderers.Interfaces;

    /// <summary>
    /// The TSQL code renderer for the "Get" operator.
    /// </summary>
    [OperatorRendererSymbol("get")]
    internal sealed class GetOperatorRenderer : IOperatorRenderer
    {
        private readonly ITargetConfiguration config;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetOperatorRenderer"/> class.
        /// </summary>
        /// <param name="config">The configuration of the target.</param>
        public GetOperatorRenderer(ITargetConfiguration config)
        {
            this.config = config;
        }

        public string Render(IExpression expr, StructureComponent component)
        {
            if (component != null) return component.BaseComponentName;
            if (expr.ParentExpression == null) return $"SELECT * FROM {this.config.EnvMapper.Map(expr.ExpressionText)}";
            return this.config.EnvMapper.Map(expr.ExpressionText);
        }
    }
}
