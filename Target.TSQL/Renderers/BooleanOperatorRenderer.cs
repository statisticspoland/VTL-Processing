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
    /// The TSQL code renderer for the "Boolean" operator.
    /// </summary>
    [OperatorRendererSymbol("and", "or", "xor", "not")]
    internal sealed class BooleanOperatorRenderer : IOperatorRenderer
    {
        private readonly OperatorRendererResolver opRendererResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanOperatorRenderer"/> class.
        /// </summary>
        /// <param name="opRendererResolver">The operator renderer resolver.</param>
        public BooleanOperatorRenderer(OperatorRendererResolver opRendererResolver)
        {
            this.opRendererResolver = opRendererResolver;
        }

        public string Render(IExpression expr, StructureComponent component)
        {
            if (!expr.IsScalar && !expr.IsApplyComponent && component == null) return this.opRendererResolver("overall").Render(expr, component);

            IExpression expr1 = expr.OperandsCollection.ToArray()[0];

            bool isRootExpr = 
                !expr.ParamSignature.In("filter", "having") && 
                (expr.ParentExpression == null || (!expr.ParentExpression.OperatorSymbol.In("and", "or", "xor", "not") && expr.ParentExpression.ParamSignature != "if")) &&
                expr.ContainingSchema.Rulesets.FirstOrDefault(ruleset => ruleset.RulesCollection.Contains(expr.GetFirstAncestorExpr() ?? expr)) == null;

            string symbol = expr.OperatorSymbol.ToUpper();
            string op1 = this.opRendererResolver(expr1.OperatorSymbol).Render(expr1, component);

            if (expr.OperandsCollection.ToArray()[0].OperatorSymbol.In("const", "get", "ref", "comp", "#")) op1 += " = 1";

            if (symbol != "NOT")
            {
                IExpression expr2 = expr.OperandsCollection.ToArray()[1];

                string result = string.Empty;
                string op2 = this.opRendererResolver(expr2.OperatorSymbol).Render(expr2, component);

                if (expr.OperandsCollection.ToArray()[1].OperatorSymbol.In("const", "ref", "comp")) op2 += " = 1";

                if (symbol != "XOR") result = $"({op1})\n{symbol} ({op2})";
                else result = $"({op1} AND NOT {op2})\nOR (NOT {op1} AND {op2})";

                if (isRootExpr) return $"IIF({result}, 1, 0)";
                return result;
            }
            else
            {
                if (isRootExpr) return $"IIF(NOT {op1}, 1, 0)";
                return $"{symbol} ({op1})";
            }
        }
    }
}
