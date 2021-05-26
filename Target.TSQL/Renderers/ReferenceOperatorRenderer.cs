namespace StatisticsPoland.VtlProcessing.Target.TSQL.Renderers
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.ComponentTools;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Renderers.Interfaces;

    /// <summary>
    /// The TSQL code renderer for the "Reference" operator.
    /// </summary>
    [OperatorRendererSymbol("ref")]
    internal sealed class ReferenceOperatorRenderer : IOperatorRenderer
    {
        private readonly IEnvironmentMapper _envMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceOperatorRenderer"/> class.
        /// </summary>
        /// <param name="envMapper">The environment names mapper.</param>
        public ReferenceOperatorRenderer(IEnvironmentMapper envMapper)
        {
            this._envMapper = envMapper;
        }

        public string Render(IExpression expr, StructureComponent component)
        {
            if (expr.ParamSignature == "<root>") return $"SELECT * FROM {this._envMapper.Map(expr.ReferenceExpression.ResultMappedName)}";
            if (expr.ResultName == "Alias")
            {
                // join "if-then-else" operator support:
                if (component == null && (expr.GetFirstAncestorExpr("If") != null || expr.GetFirstAncestorExpr("Then") != null || expr.GetFirstAncestorExpr("Else") != null))
                {
                    component = expr.CurrentJoinExpr.GetAliasExpression(expr.ExpressionText).Structure.Measures[0];
                    return $"{expr.ExpressionText}.{component.ComponentName}";
                }
                else if (expr.GetFirstAncestorExpr("If") != null) return component != null ? $"{expr.ExpressionText}.{component.ComponentName}" : expr.ResultMappedName;
            }
            else if (expr.ParentExpression.OperatorSymbol == "isnull") return component?.ComponentName?.GetNameWithoutAlias() ?? expr.ResultMappedName;
            else return expr.ResultMappedName;

            if (component == null) return expr.ResultMappedName;
            if (expr.GetFirstAncestorExpr("Apply") != null) return $"{expr.ExpressionText}.{component.ComponentName}";
            return $"{expr.ExpressionText}.{component.BaseComponentName.GetNameWithoutAlias()}";
        }
    }
}
