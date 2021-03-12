namespace StatisticsPoland.VtlProcessing.Target.TSQL.Renderers
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using System.Linq;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Renderers.Interfaces;

    /// <summary>
    /// The TSQL code renderer for the "Analytic function" operator.
    /// </summary>
    [OperatorRendererSymbol("first_value", "last_value", "lag", "rank", "ratio_to_report", "lead")]
    internal sealed class AnalyticFunctionOperatorRenderer : IOperatorRenderer
    {
        private readonly OperatorRendererResolver opRendererResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticFunctionOperatorRenderer"/> class.
        /// </summary>
        /// <param name="opRendererResolver">The operator renderer resolver.</param>
        public AnalyticFunctionOperatorRenderer(OperatorRendererResolver opRendererResolver)
        {
            this.opRendererResolver = opRendererResolver;
        }

        public string Render(IExpression expr, StructureComponent component)
        {
            string opSymbol = expr.OperatorSymbol;
            string compName = expr.OperandsCollection.ToArray()[0].OperatorSymbol != null ?
                this.opRendererResolver(expr.OperandsCollection.ToArray()[0].OperatorSymbol).Render(expr.OperandsCollection.ToArray()[0]) :
                string.Empty;

            if (compName != string.Empty && component != null) compName += $".{component.ComponentName}";

            string over = string.Empty;
            if (expr.Operands.ContainsKey("over")) over = this.opRendererResolver("over").Render(expr.Operands["over"]);

            if (opSymbol == "ratio_to_report" 
                && ((component != null && component.ValueDomain.DataType.In(BasicDataType.Integer, BasicDataType.Number))
                || (component == null && expr.OperandsCollection.ToArray()[0].Structure.Components[0].ValueDomain.DataType.In(BasicDataType.Integer, BasicDataType.Number)))) 
                    return $"CAST({compName} AS DECIMAL(28,9)) / SUM({compName}){over}";

            return $"{opSymbol.ToUpper()}({compName}){over}";
        }
    }
}
