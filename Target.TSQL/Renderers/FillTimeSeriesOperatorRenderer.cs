namespace StatisticsPoland.VtlProcessing.Target.TSQL.Renderers
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.ComponentTools;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using System.Linq;
    using System.Text;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Renderers.Interfaces;

    /// <summary>
    /// The TSQL code renderer for "Fill time series" operator.
    /// </summary>
    [OperatorRendererSymbol("fill_time_series")]
    internal sealed class FillTimeSeriesOperatorRenderer : IOperatorRenderer
    {
        private readonly OperatorRendererResolver _opRendererResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="FillTimeSeriesOperatorRenderer"/> class.
        /// </summary>
        /// <param name="opRendererResolver">The operator renderer resolver.</param>
        public FillTimeSeriesOperatorRenderer(OperatorRendererResolver opRendererResolver)
        {
            this._opRendererResolver = opRendererResolver;
        }

        public string Render(IExpression expr, StructureComponent component)
        {
            StringBuilder result = new StringBuilder();
            StructureComponent timeId = expr.Structure.Identifiers.First(comp => comp.ValueDomain.DataType.In(BasicDataType.Time, BasicDataType.Date, BasicDataType.TimePeriod));
            StructureComponent[] identifiers = expr.Structure.Identifiers.ToArray();
            StructureComponent[] measures = expr.Structure.Measures.ToArray();
            StructureComponent[] attributes = expr.Structure.ViralAttributes.ToArray();
            IExpression datasetExpr = expr.OperandsCollection.ToArray()[0];
            string datasetName = this._opRendererResolver(datasetExpr.OperatorSymbol).Render(datasetExpr);
            string timeIdName = timeId.ComponentName.GetNameWithoutAlias();

            result.AppendLine("SELECT");

            foreach (StructureComponent identifier in identifiers)
            {
                result.AppendLine($"ds1.{identifier.ComponentName.GetNameWithoutAlias()},");
            }

            foreach (StructureComponent measure in measures)
            {
                result.AppendLine($"ds2.{measure.ComponentName.GetNameWithoutAlias()},");
            }

            if (attributes.Length > 0)
            {
                foreach (StructureComponent attribute in attributes)
                {
                    result.AppendLine($"ds2.{attribute.ComponentName.GetNameWithoutAlias()},");
                }
            }
            else
            {
                result = new StringBuilder(result.ToString().Remove(result.ToString().Length - 3)); // usunięcie ",\n"
                result.AppendLine();
            }
            result.AppendLine("FROM (");
            result.AppendLine("SELECT * FROM (");
            result.AppendLine("(SELECT");
            
            for (int i = 1; i <= 2; i++)
            {
                foreach (StructureComponent identifier in identifiers.Where(id => id != timeId))
                {
                    if (i == 1) result.AppendLine($"{identifier.ComponentName.GetNameWithoutAlias()},");
                    else result.Append($"{identifier.ComponentName.GetNameWithoutAlias()}, ");
                }

                if (i == 1)
                {
                    if (expr.OperatorDefinition.Keyword == "single")
                    {
                        result.AppendLine($"MIN(SUBSTRING({timeIdName}, 1, 4)) AS ymin,");
                        result.AppendLine($"MAX(SUBSTRING({timeIdName}, 1, 4)) AS ymax");
                    }
                    else
                    {
                        result = new StringBuilder(result.ToString().Remove(result.ToString().Length - 3)); // usunięcie ",\n"
                        result.AppendLine();
                    }

                    result.AppendLine($"FROM {datasetName} GROUP BY");
                }
                else result = new StringBuilder(result.ToString().Remove(result.ToString().Length - 2)); // usunięcie ", "
            }

            result.AppendLine(") AS ds1");
            result.AppendLine("CROSS JOIN (");
            result.AppendLine("SELECT");
            result.AppendLine("num,");
            
            switch (timeId.ValueDomain.DataType)
            {
                case BasicDataType.Time:
                    result.AppendLine("CONCAT(");
                    result.AppendLine("num,");
                    result.AppendLine("(SELECT month1 FROM Info),");
                    result.AppendLine("(SELECT day1 FROM Info), '/',");
                    result.AppendLine("(SELECT num + ydiff FROM Info),");
                    result.AppendLine("(SELECT month2 FROM Info),");
                    result.AppendLine("(SELECT day2 FROM Info)");
                    break;
                case BasicDataType.Date:
                    result.AppendLine("CONCAT(");
                    result.AppendLine("num, '-',");
                    result.AppendLine("(SELECT month FROM Split), '-',");
                    result.AppendLine("(SELECT day FROM Split)");
                    break;
                case BasicDataType.TimePeriod:
                    result.AppendLine("CONCAT(");
                    result.AppendLine("num,");
                    result.AppendLine("(SELECT period FROM Period)");
                    break;
                default: throw new VtlTargetError(expr, $"Unsupported data type: {timeId.ValueDomain.DataType}.");

            }

            result.AppendLine($") AS {timeIdName}");
            result.AppendLine("FROM (");
            result.AppendLine($"SELECT ROW_NUMBER() OVER (ORDER BY object_id) AS num");
            result.AppendLine("FROM [sys].all_objects) AS t");
            result.AppendLine(") AS ds2)");
            result.AppendLine("WHERE num BETWEEN");
            if (expr.OperatorDefinition.Keyword == "all")
            {
                result.AppendLine("(SELECT MIN(year) FROM Years) AND");
                result.AppendLine("(SELECT MAX(year) FROM Years)");
            }
            else result.Append("ymin AND ymax");
            
            result.AppendLine(") AS ds1");
            result.AppendLine($"LEFT JOIN {datasetName} AS ds2 ON");

            for (int i = 1; i <= 2; i++)
            {
                foreach (StructureComponent identifier in identifiers)
                {
                    string idName = identifier.ComponentName.GetNameWithoutAlias();
                    if (i == 1) result.AppendLine($"ds1.{idName} = ds2.{idName} AND");
                    else if (identifier != timeId) result.AppendLine($"ds1.{idName},");
                }

                if (i == 1)
                {
                    result = new StringBuilder(result.ToString().Remove(result.ToString().Length - 5)); // usunięcie "AND\n"
                    result.AppendLine();
                    result.AppendLine($"ORDER BY");
                }
            }

            result.AppendLine($"ds1.{timeIdName}");
            result.AppendLine("OFFSET 0 ROWS");
            return result.ToString();
        }

        /// <summary>
        /// Renders a "WITH" TSQL code prefix for the expression.
        /// </summary>
        /// <param name="expr">The expression which parameters shall be used to render.</param>
        /// <returns>The TSQL translated code.</returns>
        public string RenderWith(IExpression expr)
        {
            StringBuilder result = new StringBuilder();
            StructureComponent timeId = expr.Structure.Identifiers.First(comp => comp.ValueDomain.DataType.In(BasicDataType.Time, BasicDataType.Date, BasicDataType.TimePeriod));
            StructureComponent[] identifiers = expr.Structure.Identifiers.ToArray();
            IExpression datasetExpr = expr.OperandsCollection.ToArray()[0];
            string datasetName = this._opRendererResolver(datasetExpr.OperatorSymbol).Render(datasetExpr);
            string timeIdName = timeId.ComponentName.GetNameWithoutAlias();

            switch (timeId.ValueDomain.DataType)
            {
                case BasicDataType.Time:
                    result.AppendLine(";WITH Info AS (");
                    result.AppendLine("SELECT TOP 1");
                    result.AppendLine($"{this.RenderDatePartSelector(timeIdName, "month1")} AS month1,");
                    result.AppendLine($"{this.RenderDatePartSelector(timeIdName, "month2")} AS month2,");
                    result.AppendLine($"{this.RenderDatePartSelector(timeIdName, "day1")} AS day1,");
                    result.AppendLine($"{this.RenderDatePartSelector(timeIdName, "day2")} AS day2,");
                    result.AppendLine("CONVERT(INT, MIN(closing_year)) -");
                    result.AppendLine($"CONVERT(INT, MIN(SUBSTRING(ds1.{timeIdName}, 1, 4))) AS ydiff");
                    result.AppendLine($"FROM {datasetName} AS ds1 INNER JOIN (");
                    result.AppendLine("SELECT TOP 1");

                    foreach (StructureComponent identifier in identifiers)
                    {
                        result.AppendLine($"{identifier.ComponentName.GetNameWithoutAlias()},");
                    }

                    result.AppendLine("CASE WHEN (");
                    result.AppendLine($"SELECT COUNT(*) FROM STRING_SPLIT({timeIdName}, '-')) > 1");
                    result.AppendLine("THEN CASE WHEN (");
                    result.AppendLine($"SELECT COUNT(*) FROM STRING_SPLIT({timeIdName}, '-')) > 4");
                    result.AppendLine($"THEN SUBSTRING({timeIdName}, 12, 4)");
                    result.AppendLine($"ELSE SUBSTRING({timeIdName}, 9, 4)");
                    result.AppendLine("END");
                    result.AppendLine($"ELSE SUBSTRING({timeIdName}, 6, 4)");
                    result.AppendLine("END AS closing_year");
                    result.AppendLine($"FROM {datasetName}) AS ds2");
                    result.AppendLine("ON ");

                    bool removed = false;
                    for (int i = 1; i <= 2; i++)
                    {
                        foreach (StructureComponent identifier in identifiers)
                        {
                            string idName = identifier.ComponentName.GetNameWithoutAlias();

                            if (i == 1) result.AppendLine($"ds1.{idName} = ds2.{idName} AND");
                            else result.AppendLine($"ds1.{identifier.ComponentName},");
                        }

                        if (!removed)
                        {
                            result = new StringBuilder(result.ToString().Remove(result.ToString().Length - 6)); // usunięcie " AND\n"
                            result.AppendLine();
                            result.AppendLine("GROUP BY ");
                            removed = true;
                        }
                    }

                    result = new StringBuilder(result.ToString().Remove(result.ToString().Length - 3)); // usunięcie ",\n"
                    result.AppendLine();
                    result.Append(")");
                    break;
                case BasicDataType.Date:
                    result.AppendLine(";WITH Split AS (");
                    result.AppendLine("SELECT TOP 1");
                    result.AppendLine($"SUBSTRING({timeIdName}, 6, 2) AS month,");
                    result.AppendLine($"SUBSTRING({timeIdName}, 9, 2) AS day");
                    result.Append($"FROM {datasetName})");
                    break;
                case BasicDataType.TimePeriod:
                    result.AppendLine(";WITH Period AS (");
                    result.AppendLine("SELECT TOP 1");
                    result.AppendLine("CONCAT(");
                    result.AppendLine($"SUBSTRING({timeIdName}, 5, 1),");
                    result.AppendLine($"SUBSTRING({timeIdName}, 6, LEN({timeIdName})- 5)");
                    result.AppendLine(") AS period");
                    result.Append($"FROM {datasetName})");
                    break;
                default: throw new VtlTargetError(expr, $"Unsupported data type: {timeId.ValueDomain.DataType}.");
            }

            if (expr.OperatorDefinition.Keyword == "all")
            {
                result.AppendLine(",");
                result.AppendLine("Years AS (");
                result.AppendLine($"SELECT (SELECT SUBSTRING({timeIdName}, 1, 4)) AS year");
                result.Append($"FROM {datasetName})");
            }
            else result.AppendLine();

            return result.ToString();
        }

        /// <summary>
        /// Renders a select of a date part TSQL translated code.
        /// </summary>
        /// <param name="timeIdName">The name of an id of the time.</param>
        /// <param name="partName">The name of a part of the time.</param>
        /// <returns>The TSQL translated code.</returns>
        private string RenderDatePartSelector(string timeIdName, string partName)
        {
            StringBuilder sb = new StringBuilder();
            int[] indexes = new int[] { 1, 0 };

            switch (partName)
            {
                case "month1": indexes = new int[] { 1, 6 }; break;
                case "day1": indexes = new int[] { 4, 9 }; break;
                case "day2": indexes = new int[] { 4, 20 }; break;
                default: break;
            }
            
            // SQL IF
            sb.Append("IIF(");
            sb.AppendLine($"(SELECT COUNT(*) FROM STRING_SPLIT(ds1.{timeIdName}, '-')) > {indexes[0]},");
            
            // SQL THEN
            if (partName != "month2")
            {
                sb.Append($"CONCAT('-', SUBSTRING(ds1.{timeIdName}, {indexes[1]}, 2))");
            }
            else
            {
                sb.Append("IIF(");
                sb.AppendLine($"(SELECT COUNT(*) FROM STRING_SPLIT(ds1.{timeIdName}, '-')) > 4,"); // IF EXPRESSION
                sb.AppendLine($"CONCAT('-', SUBSTRING(ds1.{timeIdName}, 17, 2)),"); // THEN
                sb.Append($"CONCAT('-', SUBSTRING(ds1.{timeIdName}, 14, 2)))"); //ELSE
            }

            // SQL ELSE
            sb.Append(", NULL)");
            
            return sb.ToString();
        }
    }
}
