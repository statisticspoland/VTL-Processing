namespace Target.TSQL.Renderers
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.ComponentTools;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using Target.TSQL.Infrastructure.Attributes;
    using Target.TSQL.Infrastructure.Interfaces;
    using Target.TSQL.Renderers.Interfaces;

    /// <summary>
    /// The TSQL code renderer for the "Reference" operator.
    /// </summary>
    [OperatorRendererSymbol("ref")]
    internal sealed class ReferenceOperatorRenderer : IOperatorRenderer
    {
        private readonly ITargetConfiguration config;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceOperatorRenderer"/> class.
        /// </summary>
        /// <param name="config">The configuration of the target.</param>
        public ReferenceOperatorRenderer(ITargetConfiguration config)
        {
            this.config = config;
        }

        public string Render(IExpression expr, StructureComponent component)
        {
            if (expr.ParamSignature == "<root>") return $"SELECT * FROM {this.config.EnvMapper.Map(expr.ReferenceExpression.ResultMappedName)}";
            if (expr.ResultName != "Alias" && expr.ParentExpression.OperatorSymbol == "isnull") return component?.ComponentName?.GetNameWithoutAlias() ?? expr.ResultMappedName;

            if (component == null) return expr.ResultMappedName;
            if (expr.GetFirstAncestorExpr("Apply") != null) return $"{expr.ExpressionText}.{component.ComponentName}";
            return $"{expr.ExpressionText}.{component.BaseComponentName.GetNameWithoutAlias()}";
        }
    }
}
