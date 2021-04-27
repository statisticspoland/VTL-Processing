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
    /// The TSQL code renderer for the "Aggregation function" operator.
    /// </summary>
    [OperatorRendererSymbol("count", "min", "max", "median", "sum", "avg", "stddev_pop", "stddev_samp", "var_pop", "var_samp")]
    internal sealed class AggrFunctionOperatorRenderer : IOperatorRenderer
    {
        private readonly OperatorRendererResolver _opRendererResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="AggrFunctionOperatorRenderer"/> class.
        /// </summary>
        /// <param name="opRendererResolver">The operator renderer resolver.</param>
        public AggrFunctionOperatorRenderer(OperatorRendererResolver opRendererResolver)
        {
            this._opRendererResolver = opRendererResolver;
        }

        public string Render(IExpression expr, StructureComponent component)
        {
            string opSymbol = expr.OperatorSymbol.ToUpper();
            string compName = "*";
            if (expr.OperandsCollection.Count != 0)
            {
                if (component == null) compName = this._opRendererResolver(expr.OperandsCollection.ToArray()[0].OperatorSymbol).Render(expr.OperandsCollection.ToArray()[0]);
                else compName = $"{this._opRendererResolver(expr.OperandsCollection.ToArray()[0].OperatorSymbol).Render(expr.OperandsCollection.ToArray()[0])}.{component.ComponentName}";
            }

            if (!opSymbol.In("COUNT", "SUM")
                && ((component != null && component.ValueDomain.DataType.In(BasicDataType.Integer, BasicDataType.Number))
                    || (component == null && expr.OperandsCollection.ToArray()[0].Structure.Components[0].ValueDomain.DataType.In(BasicDataType.Integer, BasicDataType.Number))))
                compName = $"CAST({compName} AS DECIMAL(28,9))";

            string over = string.Empty;
            if (expr.Operands.ContainsKey("over")) over = this._opRendererResolver("over").Render(expr.Operands["over"]);

            if (opSymbol == "STDDEV_POP") opSymbol = "STDEVP";
            else if (opSymbol == "STDDEV_SAMP") opSymbol = "STDEV";
            else if (opSymbol == "VAR_POP") opSymbol = "VARP";
            else if (opSymbol == "VAR_SAMP") opSymbol = "VAR";
            else if (opSymbol == "MEDIAN")
            {
                return $"SUM({compName}) / 2{over}";
            }

            return $"{opSymbol}({compName}){over}";
        }
    }
}
