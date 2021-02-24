namespace Target.TSQL.Renderers
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using System.Linq;
    using System.Text;
    using Target.TSQL.Infrastructure;
    using Target.TSQL.Infrastructure.Attributes;
    using Target.TSQL.Renderers.Interfaces;

    /// <summary>
    /// The TSQL code renderer for overall "non-join" operators.
    /// </summary>
    [OperatorRendererSymbol("overall")]
    internal class OperatorRenderer : IOperatorRenderer
    {
        private readonly OperatorRendererResolver opRendererResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorRenderer"/> class.
        /// </summary>
        /// <param name="opRendererResolver">The operator renderer resolver.</param>
        public OperatorRenderer(OperatorRendererResolver opRendererResolver)
        {
            this.opRendererResolver = opRendererResolver;
        }

        public string Render(IExpression expr, StructureComponent component)
        {
            if (component != null) return this.opRendererResolver(expr.OperatorSymbol).Render(expr, component);

            IExpression sourceExpr = expr.GetDescendantExprs("Get").FirstOrDefault() ?? expr.GetDescendantExprs("Reference").FirstOrDefault();
            if (sourceExpr == null) throw new VtlTargetError(expr, "Source expression has been not found.");

            string asAlias = string.Empty;
            if (expr.OperatorSymbol.In("flow_to_stock", "stock_to_flow", "timeshift")) asAlias = " AS ds";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT");

            foreach (StructureComponent identifier in expr.Structure.Identifiers)
            {
                if (expr.OperatorSymbol == "timeshift" && identifier.ValueDomain.DataType.In(BasicDataType.Time, BasicDataType.Date, BasicDataType.TimePeriod)) 
                    sb.AppendLine($"{this.opRendererResolver(expr.OperatorSymbol).Render(expr, identifier)} AS {identifier.ComponentName},");
                else sb.AppendLine($"{identifier.ComponentName},");
            }

            if (expr.Structure.Measures.Count == 0)
            {
                sb = new StringBuilder(sb.ToString().Remove(sb.ToString().Length - 3)); // usunięcie ",\n" 
                sb.AppendLine();
            }

            foreach (StructureComponent measure in expr.Structure.Measures)
            {
                if (expr.OperatorSymbol == "isnull") sb.AppendLine($"{this.opRendererResolver(expr.OperatorSymbol).Render(expr, expr.Operands["ds_1"].Structure.Measures[0])} AS {measure.ComponentName},");
                else if (expr.OperatorSymbol != "timeshift") sb.AppendLine($"{this.opRendererResolver(expr.OperatorSymbol).Render(expr, measure)} AS {measure.ComponentName},");
                else sb.AppendLine($"{measure.ComponentName},");
            }

            if (expr.Structure.ViralAttributes.Count == 0)
            {
                sb = new StringBuilder(sb.ToString().Remove(sb.ToString().Length - 3)); // usunięcie ",\n" 
                sb.AppendLine();
            }

            foreach (StructureComponent attribute in expr.Structure.ViralAttributes)
            {
                sb.AppendLine($"{attribute.ComponentName},");
            }

            if (expr.Structure.ViralAttributes.Count > 0)
            {
                sb = new StringBuilder(sb.ToString().Remove(sb.ToString().Length - 3)); // usunięcie ",\n" 
                sb.AppendLine();
            }
            
            sb.AppendLine($"FROM {this.opRendererResolver(sourceExpr.OperatorSymbol).Render(sourceExpr)}{asAlias}");
            return sb.ToString();
        }
    }
}
