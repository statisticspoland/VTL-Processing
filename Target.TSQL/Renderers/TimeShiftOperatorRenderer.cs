namespace Target.TSQL.Renderers
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
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
    /// The TSQL code renderer for the "Timeshift" operator.
    /// </summary>
    [OperatorRendererSymbol("timeshift")]
    internal sealed class TimeShiftOperatorRenderer : IOperatorRenderer
    {
        private readonly OperatorRendererResolver opRendererResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeShiftOperatorRenderer"/> class.
        /// </summary>
        /// <param name="opRendererResolver">The operator renderer resolver.</param>
        public TimeShiftOperatorRenderer(OperatorRendererResolver opRendererResolver)
        {
            this.opRendererResolver = opRendererResolver;
        }

        public string Render(IExpression expr, StructureComponent component)
        {
            if (!expr.IsScalar && component == null) return this.opRendererResolver("overall").Render(expr, component);

            StringBuilder result = new StringBuilder();
            IExpression datasetExpr = expr.OperandsCollection.ToArray()[0];
            string datasetName = this.opRendererResolver(datasetExpr.OperatorSymbol).Render(datasetExpr);
            string identifierName = component.ComponentName.GetNameWithoutAlias();
            string modifier = expr.Operands["ds_2"].ExpressionText;

            result.Append($"(SELECT {identifierName} FROM (");

            switch(component.ValueDomain.DataType)
            {
                case BasicDataType.Time:
                    string valueFromStrSpl = $"VALUE FROM STRING_SPLIT(ds.{identifierName}, '/')";

                    result.AppendLine($"SELECT CONCAT(");
                    result.AppendLine($"STUFF((SELECT TOP 1 {valueFromStrSpl}),");
                    result.AppendLine("1, 4, SUBSTRING(");
                    result.AppendLine($"(SELECT TOP 1 {valueFromStrSpl}), 1, 4) + ({modifier})),");
                    result.AppendLine($"'/', STUFF((SELECT {valueFromStrSpl} ORDER BY VALUE OFFSET 1 ROW),");
                    result.AppendLine("1, 4, SUBSTRING(");
                    result.AppendLine($"(SELECT {valueFromStrSpl} ORDER BY VALUE OFFSET 1 ROW), 1, 4) + ({modifier}))");
                    result.AppendLine($") AS {identifierName}");
                    break;
                case BasicDataType.Date:
                    result.AppendLine($"SELECT STUFF(ds.{identifierName}, 1, 4, SUBSTRING(ds.{identifierName}, 1, 4) + ({modifier})) AS {identifierName}");
                    break;
                case BasicDataType.TimePeriod:
                    result.AppendLine("SELECT (");
                    result.AppendLine($"CASE SUBSTRING(ds.{identifierName}, 5, 1)");
                    result.AppendLine(this.RenderTimePeriodWHEN('A', identifierName, modifier));
                    result.AppendLine(this.RenderTimePeriodWHEN('S', identifierName, modifier));
                    result.AppendLine(this.RenderTimePeriodWHEN('Q', identifierName, modifier));
                    result.AppendLine(this.RenderTimePeriodWHEN('M', identifierName, modifier));
                    result.AppendLine(this.RenderTimePeriodWHEN('W', identifierName, modifier));
                    result.AppendLine(this.RenderTimePeriodWHEN('D', identifierName, modifier));
                    result.AppendLine($"END) AS {identifierName}");
                    break;
                default: throw new VtlTargetError(expr, $"Unsupported data type: {component.ValueDomain.DataType}.");
            }

            result.AppendLine($"FROM {datasetName}");
            result.AppendLine($"WHERE");

            foreach (StructureComponent id in expr.Structure.Identifiers)
            {
                result.AppendLine($"{id.ComponentName} = ds.{id.ComponentName} AND");
            }

            result = new StringBuilder(result.ToString().Remove(result.ToString().Length - 6)); // usunięcie " AND\n"
            result.Append(") AS t)");

            return result.ToString();
        }

        /// <summary>
        /// Renders a when clause of the time period TSQL translated code.
        /// </summary>
        /// <param name="symbol">The symbol of a duration.</param>
        /// <param name="identifierName">The identifier name.</param>
        /// <param name="modifier">The time modifier.</param>
        /// <returns>The TSQL translated code.</returns>
        private string RenderTimePeriodWHEN(char symbol, string identifierName, string modifier)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine($"WHEN '{symbol}' THEN");
            
            if (symbol == 'A')
            {
                result.Append($"CONCAT(SUBSTRING(ds.{identifierName}, 1, 4) + ({modifier}), SUBSTRING(ds.{identifierName}, 5, 1))");
            }
            else
            {
                int max = 0;
                string periodSubstr = $"SUBSTRING(ds.{identifierName}, 6, 1)";

                if (!symbol.In('S', 'Q')) periodSubstr = $"SUBSTRING(ds.{identifierName}, 6, LEN(ds.{identifierName}) - 5)";
                switch (symbol)
                {
                    case 'S': max = 2; break;
                    case 'Q': max = 4; break;
                    case 'M': max = 12; break;
                    case 'W': max = 53; break;
                    case 'D': max = 365; break;
                }

                result.AppendLine("CONCAT(");
                result.AppendLine("--Concat arg1");
                result.AppendLine($"SUBSTRING(ds.{identifierName}, 1, 4) + ({modifier} / {max}) +");
                result.AppendLine($"IIF(NOT {modifier} % {max} = 0 AND {periodSubstr} + {modifier} > {max}, 1, --If Then");
                result.AppendLine($"IIF(NOT {modifier} % {max} = 0 AND {periodSubstr} + {modifier} < 1, -1, 0)), --Else If Then Else");
                result.AppendLine("--Concat arg2");
                result.AppendLine($"SUBSTRING(ds.{identifierName}, 5, 1),");
                result.AppendLine("--Concat arg3");
                result.AppendLine($"IIF(({periodSubstr} + ({modifier})) % {max} <= 0, --If");
                result.AppendLine($"{max} + ({periodSubstr} + ({modifier})) % {max}, --Then");
                result.Append($"({periodSubstr} + ({modifier})) % {max})) --Else");
            }

            return result.ToString();
        }
    }
}
