namespace StatisticsPoland.VtlProcessing.Target.TSQL.Renderers
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System.Linq;
    using System.Text;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Renderers.Interfaces;

    /// <summary>
    /// The TSQL code renderer for the "Element of" operator.
    /// </summary>
    [OperatorRendererSymbol("in", "not_in")]
    internal sealed class InOperatorRenderer :  IOperatorRenderer
    {
        private readonly OperatorRendererResolver opRendererResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="InOperatorRenderer"/> class.
        /// </summary>
        /// <param name="opRendererResolver">The operator renderer resolver.</param>
        public InOperatorRenderer(OperatorRendererResolver opRendererResolver)
        {
            this.opRendererResolver = opRendererResolver;
        }

        public string Render(IExpression expr, StructureComponent component)
        {
            if (!expr.IsScalar && component == null) return this.opRendererResolver("overall").Render(expr, component);

            StringBuilder sb = new StringBuilder();

            IExpression expr1 = expr.OperandsCollection.ToArray()[0];
            IExpression expr2 = expr.OperandsCollection.ToArray()[1];

            string op1 = this.opRendererResolver(expr1.OperatorSymbol).Render(expr1, component);

            string result = string.Empty;
            string symbol = expr.OperatorSymbol == "in" ? "IN" : "NOT IN";
            string prefix = "(\n";

            sb.Append($"{op1} {symbol} ");
            foreach (IExpression item in expr2.OperandsCollection)
            {
                string itemText = this.opRendererResolver(item.OperatorSymbol).Render(item, component);
                if (itemText.First() == '"' && itemText.Last() == '"') 
                    itemText = $"'{itemText.Remove(itemText.Length - 1).Remove(0, 1)}'";

                sb.AppendLine($"{prefix}SELECT {itemText}");
                prefix = "UNION ";
            }

            sb.Append(")");
            result = sb.ToString();

            if (expr.ParamSignature != "filter" && (expr.ParentExpression == null || (!expr.ParentExpression.OperatorSymbol.In("and", "or", "xor", "not") && !expr.ParentExpression.ParamSignature.In("if", "subspace"))))
                result = $"IIF({op1} IS NULL, NULL,\nIIF({result}, 1, 0))";

            return result;
        }
    }
}
