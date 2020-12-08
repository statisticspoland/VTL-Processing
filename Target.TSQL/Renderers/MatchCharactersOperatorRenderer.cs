namespace Target.TSQL.Renderers
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System.Linq;
    using Target.TSQL.Infrastructure;
    using Target.TSQL.Infrastructure.Attributes;
    using Target.TSQL.Renderers.Interfaces;

    /// <summary>
    /// The TSQL code renderer for the "Match characters" operator.
    /// </summary>
    [OperatorRendererSymbol("match_characters")]
    internal sealed class MatchCharactersOperatorRenderer : IOperatorRenderer
    {
        private readonly OperatorRendererResolver opRendererResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="MatchCharactersOperatorRenderer"/> class.
        /// </summary>
        /// <param name="opRendererResolver">The operator renderer resolver.</param>
        public MatchCharactersOperatorRenderer(OperatorRendererResolver opRendererResolver)
        {
            this.opRendererResolver = opRendererResolver;
        }

        public string Render(IExpression expr, StructureComponent component)
        {
            if (!expr.IsScalar && component == null) return this.opRendererResolver("overall").Render(expr, component);

            IExpression expr1 = expr.OperandsCollection.First();
            IExpression expr2 = expr.Operands["ds_2"];

            string op1 = this.opRendererResolver(expr1.OperatorSymbol).Render(expr1, component);

            string result = string.Empty;
            string symbol = expr.OperatorSymbol;

            if (symbol == "<>") symbol = "!=";

            result = $"{op1} LIKE '%{expr2.ExpressionText.Split("\"")[1]}%'";

            if (expr.ParamSignature != "filter" &&
                (expr.ParentExpression == null || (!expr.ParentExpression.OperatorSymbol.In("and", "or", "xor", "not") && expr.ParentExpression.ParamSignature != "if")))
            {
                result = $"IIF({op1} IS NULL, NULL,\nIIF({result}, 1, 0))";
            }

            return result;
        }
    }
}
