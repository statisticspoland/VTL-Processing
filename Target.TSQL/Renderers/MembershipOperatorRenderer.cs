namespace Target.TSQL.Renderers
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.ComponentTools;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System.Linq;
    using System.Text;
    using Target.TSQL.Infrastructure;
    using Target.TSQL.Infrastructure.Attributes;
    using Target.TSQL.Renderers.Interfaces;

    /// <summary>
    /// The TSQL code renderer for  the "Membership" operator.
    /// </summary>
    [OperatorRendererSymbol("#")]
    internal sealed class MembershipOperatorRenderer : IOperatorRenderer
    {
        private readonly OperatorRendererResolver opRendererResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="MembershipOperatorRenderer"/> class.
        /// </summary>
        /// <param name="opRendererResolver">The operator renderer resolver.</param>
        public MembershipOperatorRenderer(OperatorRendererResolver opRendererResolver)
        {
            this.opRendererResolver = opRendererResolver;
        }

        public string Render(IExpression expr, StructureComponent component)
        {
            IExpression expr1 = expr.OperandsCollection.ToArray()[0];
            IExpression expr2 = expr.OperandsCollection.ToArray()[1];

            if (expr.OperatorDefinition.Keyword == "Standard") return this.RenderStandard(expr);
            return $"{expr1.ExpressionText}.{expr2.ExpressionText}";
        }

        /// <summary>
        /// Renders a standard membership TSQL translated code.
        /// </summary>
        /// <param name="expr">The expression which parameters shall be used to render.</param>
        /// <returns>The TSQL translated code.</returns>
        private string RenderStandard(IExpression expr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT");

            foreach (StructureComponent component in expr.Structure.Components)
            {
                if (!expr.Structure.NonViralAttributes.Contains(component))
                {
                    if (component.BaseComponentName.GetNameWithoutAlias() == component.ComponentName.GetNameWithoutAlias())
                        sb.AppendLine($"{component.ComponentName.GetNameWithoutAlias()},");
                    else
                        sb.AppendLine($"{component.BaseComponentName.GetNameWithoutAlias()} AS {component.ComponentName},");
                }
            }

            sb = new StringBuilder(sb.ToString().Remove(sb.ToString().Length - 3)); // usunięcie ",\n"
            sb.AppendLine();

            string[] closingKeywords = new string[] { "(", ")", " AS t" };
            if (expr.Operands["ds_1"].OperatorSymbol.In("get", "ref")) closingKeywords = new string[] { string.Empty, string.Empty, string.Empty };

            sb.AppendLine($"FROM {closingKeywords[0]}{this.opRendererResolver(expr.Operands["ds_1"].OperatorSymbol).Render(expr.Operands["ds_1"], null)}{closingKeywords[1]}{closingKeywords[2]}");
            return sb.ToString();
        }
    }
}
