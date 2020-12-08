namespace Target.TSQL.Renderers
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
    /// The TSQL code renderer for the "Stock to flow" operator.
    /// </summary>
    [OperatorRendererSymbol("stock_to_flow")]
    internal sealed class StockToFlowOperatorRenderer : IOperatorRenderer
    {
        private readonly OperatorRendererResolver opRendererResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="StockToFlowOperatorRenderer"/> class.
        /// </summary>
        /// <param name="opRendererResolver">The operator renderer resolver.</param>
        public StockToFlowOperatorRenderer(OperatorRendererResolver opRendererResolver)
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

            result.AppendLine($"ds.{measureName} - ISNULL((");
            result.AppendLine($"SELECT {measureName} FROM (");
            result.AppendLine($"SELECT TOP 1 {measureName} FROM {datasetName}");
            result.AppendLine("WHERE");

            bool removed = false;
            for (int i = 1; i <= 2; i++)
            {
                foreach (StructureComponent identifier in expr.Structure.Identifiers)
                {
                    if (i == 1)
                    {
                        char symbol = '=';
                        if (identifier.ValueDomain.DataType.In(BasicDataType.Time, BasicDataType.Date, BasicDataType.TimePeriod)) symbol = '<';
                        result.AppendLine($"{identifier.ComponentName} {symbol} ds.{identifier.ComponentName} AND");
                    }
                    else result.Append($"{identifier.ComponentName}, ");
                }

                if (!removed)
                {
                    result = new StringBuilder(result.ToString().Remove(result.ToString().Length - 6)); // usunięcie " AND\n"
                    result.AppendLine();
                    result.AppendLine("ORDER BY ");
                    removed = true;
                }
            }

            result = new StringBuilder(result.ToString().Remove(result.ToString().Length - 2)); // usunięcie ", "
            result.AppendLine(" DESC");
            result.Append($") AS t), 0)");

            return result.ToString();
        }
    }
}
