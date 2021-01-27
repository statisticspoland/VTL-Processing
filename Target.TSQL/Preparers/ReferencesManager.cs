namespace Target.TSQL.Preparers
{
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Target.TSQL.Infrastructure;
    using Target.TSQL.Infrastructure.Interfaces;
    using Target.TSQL.Preparers.Interfaces;
    using Target.TSQL.Renderers;

    /// <summary>
    /// The references manager.
    /// </summary>
    public class ReferencesManager : IReferencesManager
    {
        private readonly OperatorRendererResolver opRendererResolver;
        private readonly ITargetConfiguration conf;
        private Dictionary<IExpression, string> nonPersistentExprs;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferencesManager"/> class.
        /// </summary>
        /// <param name="opRendererResolver">The operator renderer resolver.</param>
        /// <param name="configuration">The configuration of the target.</param>
        public ReferencesManager(OperatorRendererResolver opRendererResolver, ITargetConfiguration configuration)
        {
            this.opRendererResolver = opRendererResolver;
            this.conf = configuration;
            this.nonPersistentExprs = new Dictionary<IExpression, string>();
        }

        public void TakeNonPersistentExprs(ITransformationSchema schema)
        {
            this.nonPersistentExprs.Clear();
            foreach (AssignmentObject assignmentObject in schema.AssignmentObjects)
            {
                IExpression expr = assignmentObject.Expression;
                if (!assignmentObject.IsPersistentAssignment)
                {
                    string renderResult = this.opRendererResolver(expr.OperatorSymbol).Render(expr);

                    if (expr.IsScalar)
                        this.nonPersistentExprs.Add(assignmentObject.Expression, $"DECLARE @{assignmentObject.Name} {this.GetType(expr)} = {renderResult};");
                    else
                    {
                        StringBuilder result = new StringBuilder();
                        if (expr.OperatorSymbol == "fill_time_series")
                            result.AppendLine((this.opRendererResolver(expr.OperatorSymbol) as FillTimeSeriesOperatorRenderer).RenderWith(expr));
                        result.AppendLine($"SELECT * INTO #{assignmentObject.Name} FROM (\n{renderResult}) AS t");

                        if (expr.OperatorSymbol == "exists_in" && expr.OperatorDefinition.Keyword == "false") result.Append($" WHERE {expr.Structure.Measures[0].ComponentName} = 0");

                        this.nonPersistentExprs.Add(assignmentObject.Expression, result.ToString());
                    }
                }
            }
        }

        public string RenderNonPersistentExpr(IExpression expr)
        {
            StringBuilder sb = new StringBuilder();
            string result = this.nonPersistentExprs.First(nonPersExpr => nonPersExpr.Key == expr).Value;

            if (this.conf.UseComments) sb.AppendLine($"-- Raw: {expr.ResultName} := {expr.ExpressionText}");
            sb.AppendLine(result);

            return sb.ToString();
        }

        public string RenderDroppingOfTables()
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<IExpression, string> expr in this.nonPersistentExprs)
            {
                if (!expr.Key.IsScalar)
                {
                    // Sprawdzanie, czy istnieją i usuwanie tabel tymczasowych
                    sb.AppendLine($"IF OBJECT_ID (N'tempdb..{expr.Key.ResultMappedName}', N'U') IS NOT NULL");
                    sb.AppendLine($"DROP TABLE {expr.Key.ResultMappedName}\n");
                }
            }

            return sb.ToString();
        }

        public bool ContainsExpression(IExpression expr)
        {
            return this.nonPersistentExprs.ContainsKey(expr);
        }

        /// <summary>
        /// Gets a data type of an expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The data type.</returns>
        private string GetType(IExpression expression)
        {
            switch (expression.Structure.Measures.First().ValueDomain.DataType)
            {
                case BasicDataType.Boolean: return "BIT";
                case BasicDataType.Integer:
                case BasicDataType.None:
                    return "INT";
                case BasicDataType.Number: return "DECIMAL(28,9)";
                case BasicDataType.String:
                case BasicDataType.Time:
                case BasicDataType.Date:
                case BasicDataType.TimePeriod:
                case BasicDataType.Duration:
                    return "VARCHAR(MAX)";
                default: throw new NotImplementedException(); // TODO: Reszta typów bazowych   
            }
        }
    }
}
