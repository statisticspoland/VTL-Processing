namespace StatisticsPoland.VtlProcessing.Target.TSQL.Preparers
{
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System.Linq;

    /// <summary>
    /// Mapper of VTL 2.0 to target names.
    /// </summary>
    public class Mapper : IMapper
    {
        public void MapNames(ITransformationSchema schema)
        {
            foreach (AssignmentObject assignmentObject in schema.AssignmentObjects)
            {
                this.MapName(assignmentObject.Expression, true);
            }
        }

        /// <summary>
        /// Maps name of an expression object.
        /// </summary>
        /// <param name="expr">The expression object.</param>
        /// <param name="isRoot">The value specifying if the expression is a root expression.</param>
        private void MapName(IExpression expr, bool isRoot)
        {
            foreach (IExpression op in expr.OperandsCollection)
            {
                this.MapName(op, false);
            }

            if (isRoot)
            {
                if (expr.IsScalar)
                {
                    expr.ResultMappedName = $"@{expr.ResultName}";
                }
                else
                {
                    string symbol = string.Empty;
                    if (expr.ContainingSchema.AssignmentObjects.FirstOrDefault(ao => ao.Name == expr.ResultName && !ao.IsPersistentAssignment) != null)
                    {
                        symbol = "#";
                    }

                    expr.ResultMappedName = $"{symbol}{expr.ResultName}";
                }
            }
            else if (expr.OperatorSymbol == "ref" && expr.ResultName != "Alias")
            {
                if (expr.IsScalar)
                {
                    expr.ResultMappedName = $"@{expr.ExpressionText}";
                }
                else
                {
                    string symbol = string.Empty;
                    if (expr.ContainingSchema.AssignmentObjects.FirstOrDefault(ao => ao.Name == expr.ExpressionText && !ao.IsPersistentAssignment) != null)
                    {
                        symbol = "#";
                    }

                    expr.ResultMappedName = $"{symbol}{expr.ExpressionText}";
                }
            }
            else expr.ResultMappedName = expr.ExpressionText;
        }
    }
}
