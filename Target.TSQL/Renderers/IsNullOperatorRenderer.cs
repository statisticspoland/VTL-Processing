namespace StatisticsPoland.VtlProcessing.Target.TSQL.Renderers
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.ComponentTools;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System.Linq;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Renderers.Interfaces;

    /// <summary>
    /// The TSQL code renderer for "Isnull" operator.
    /// </summary>
    [OperatorRendererSymbol("isnull")]
    internal sealed class IsNullOperatorRenderer : IOperatorRenderer
    {
        private readonly OperatorRendererResolver opRendererResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="IsNullOperatorRenderer"/> class.
        /// </summary>
        /// <param name="opRendererResolver">The operator renderer resolver.</param>
        public IsNullOperatorRenderer(OperatorRendererResolver opRendererResolver)
        {
            this.opRendererResolver = opRendererResolver;
        }

        public string Render(IExpression expr, StructureComponent component)
        {
            if (!expr.IsScalar && component == null) return this.opRendererResolver("overall").Render(expr, component);

            IExpression operand = expr.OperandsCollection.First();

            string op = this.opRendererResolver(operand.OperatorSymbol).Render(operand, component);

            string result = $"{op} IS NULL";

            if (expr.ParamSignature != "filter" &&
                (expr.ParentExpression == null || (!expr.ParentExpression.OperatorSymbol.In("and", "or", "xor", "not") && expr.ParentExpression.ParamSignature != "if")) &&
                expr.ContainingSchema.Rulesets.FirstOrDefault(ruleset => ruleset.RulesCollection.Contains(expr.GetFirstAncestorExpr() ?? expr)) == null)
            {
                result = $"IIF({result}, 1, 0)";
            }

            return result;
        }
    }
}
