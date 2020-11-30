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
    /// The TSQL code renderer for the "Between" operator.
    /// </summary>
    [OperatorRendererSymbol("between")]
    internal sealed class BetweenOperatorRenderer : IOperatorRenderer
    {
        private readonly OperatorRendererResolver opRendererResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="BetweenOperatorRenderer"/> class.
        /// </summary>
        /// <param name="opRendererResolver">The operator renderer resolver.</param>
        public BetweenOperatorRenderer(OperatorRendererResolver opRendererResolver)
        {
            this.opRendererResolver = opRendererResolver;
        }

        public string Render(IExpression expr, StructureComponent component)
        {
            if (!expr.IsScalar && component == null) return this.opRendererResolver("overall").Render(expr, component);

            IExpression expr1 = expr.OperandsCollection.First();
            IExpression expr2 = expr.Operands["ds_2"];
            IExpression expr3 = expr.Operands["ds_3"];

            string op1 = this.opRendererResolver(expr1.OperatorSymbol).Render(expr1, component);
            string op2 = this.opRendererResolver(expr2.OperatorSymbol).Render(expr2, component);
            string op3 = this.opRendererResolver(expr3.OperatorSymbol).Render(expr3, component);

            string result =  $"{op1} BETWEEN {op2} AND {op3}";

            if (expr.ParamSignature != "filter" &&
                (expr.ParentExpression == null || (!expr.ParentExpression.OperatorSymbol.In("and", "or", "xor", "not") && expr.ParentExpression.ParamSignature != "if")))
            {
                result = $"IIF({op1} IS NULL OR {op2} IS NULL OR {op3} IS NULL, NULL,\nIIF({result}, 1, 0))";
            }

            return result;
        }
    }
}
