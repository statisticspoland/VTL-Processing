namespace StatisticsPoland.VtlProcessing.Target.TSQL.Renderers
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.ComponentTools;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Renderers.Interfaces;

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
            if (expr.ResultName == "Alias")
            {
                // join "if-then-else" operator support:
                if (component == null && (expr.GetFirstAncestorExpr("If") != null || expr.GetFirstAncestorExpr("Then") != null || expr.GetFirstAncestorExpr("Else") != null))
                {
                    component = expr.CurrentJoinExpr.GetAliasExpression(expr.ExpressionText).Structure.Measures[0];
                    return $"{expr.ExpressionText}.{component.ComponentName}";
                }
                else if (expr.GetFirstAncestorExpr("If") != null) return $"{expr.ExpressionText}.{component.ComponentName}";
            }
            else if (expr.ParentExpression.OperatorSymbol == "isnull") return component?.ComponentName?.GetNameWithoutAlias() ?? expr.ResultMappedName;
            else return expr.ResultMappedName;

            if (component == null) return expr.ResultMappedName;
            if (expr.GetFirstAncestorExpr("Apply") != null) return $"{expr.ExpressionText}.{component.ComponentName}";
            return $"{expr.ExpressionText}.{component.BaseComponentName.GetNameWithoutAlias()}";
        }
    }
}
