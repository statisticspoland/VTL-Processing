﻿namespace Target.TSQL.Renderers
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.ComponentTools;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using System.Linq;
    using System.Text;
    using Target.TSQL.Infrastructure;
    using Target.TSQL.Infrastructure.Attributes;
    using Target.TSQL.Renderers.Interfaces;

    /// <summary>
    /// The TSQL code renderer for the "Flow to stock" operator.
    /// </summary>
    [OperatorRendererSymbol("flow_to_stock")]
    internal sealed class FlowToStockOperatorRenderer : IOperatorRenderer
    {
        private readonly OperatorRendererResolver opRendererResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlowToStockOperatorRenderer"/> class.
        /// </summary>
        /// <param name="opRendererResolver">The operator renderer resolver.</param>
        public FlowToStockOperatorRenderer(OperatorRendererResolver opRendererResolver)
        {
            this.opRendererResolver = opRendererResolver;
        }

        public string Render(IExpression expr, StructureComponent component)
        {
            if (!expr.IsScalar && component == null) return this.opRendererResolver("overall").Render(expr, component);

            StringBuilder result = new StringBuilder();
            IExpression datasetExpr = expr.OperandsCollection.ToArray()[0];
            string datasetName = this.opRendererResolver(datasetExpr.OperatorSymbol).Render(datasetExpr);
            string measureName = component.ComponentName.GetNameWithoutAlias();

            result.AppendLine($"(SELECT SUM({measureName}) FROM (");
            result.AppendLine($"SELECT {measureName} FROM {datasetName}");
            result.AppendLine($"WHERE");

            foreach (StructureComponent identifier in expr.Structure.Identifiers)
            {
                string symbol = "=";
                if (identifier.ValueDomain.DataType.In(BasicDataType.Time, BasicDataType.Date, BasicDataType.TimePeriod)) symbol = "<=";
                result.AppendLine($"{identifier.ComponentName} {symbol} ds.{identifier.ComponentName} AND");
            }

            result = new StringBuilder(result.ToString().Remove(result.ToString().Length - 6)); // usunięcie " AND\n"
            result.Append(") AS t)");

            return result.ToString();
        }
    }
}