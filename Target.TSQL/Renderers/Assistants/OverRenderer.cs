namespace StatisticsPoland.VtlProcessing.Target.TSQL.Renderers
{
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System.Text;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Renderers.Interfaces;

    /// <summary>
    /// The TSQL code renderer for the over clause.
    /// </summary>
    [OperatorRendererSymbol("over")]
    internal sealed class OverRenderer : IOperatorRenderer
    {
        private readonly OperatorRendererResolver opRendererResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="OverRenderer"/> class.
        /// </summary>
        /// <param name="opRendererResolver">The operator renderer resolver.</param>
        public OverRenderer(OperatorRendererResolver opRendererResolver)
        {
            this.opRendererResolver = opRendererResolver;
        }

        public string Render(IExpression expr, StructureComponent component)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" OVER (");

            if (expr.Operands.ContainsKey("partition"))
            {
                sb.AppendLine("PARTITION BY ");
                foreach (IExpression compExpr in expr.Operands["partition"].OperandsCollection)
                {
                    sb.Append($"{this.opRendererResolver(compExpr.OperatorSymbol).Render(compExpr)}, ");
                }

                sb = new StringBuilder(sb.ToString().Remove(sb.ToString().Length - 2)); // usunięcie ", " 
                sb.AppendLine();
            }

            if (expr.Operands.ContainsKey("order"))
            {
                sb.AppendLine("ORDER BY");
                foreach (IExpression compExpr in expr.Operands["order"].OperandsCollection)
                {
                    sb.Append($"{this.opRendererResolver(compExpr.OperatorSymbol).Render(compExpr)}, ");
                }
            }

            sb = new StringBuilder(sb.ToString().Remove(sb.ToString().Length - 2)); // usunięcie ", " 
            sb.AppendLine();

            if (expr.Operands.ContainsKey("window"))
            {
                sb.Append(expr.Operands["window"].ExpressionText.ToUpper().Replace("DATA POINTS", "ROWS"));
            }

            sb.Append(")");
            return sb.ToString();
        }
    }
}
