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

        private void MapName(IExpression operand, bool isRoot)
        {
            foreach (IExpression op in operand.OperandsCollection)
            {
                this.MapName(op, false);
            }

            if (isRoot)
            {
                if (operand.IsScalar)
                {
                    operand.ResultMappedName = $"@{operand.ResultName}";
                }
                else
                {
                    string symbol = string.Empty;
                    if (operand.ContainingSchema.AssignmentObjects.FirstOrDefault(ao => ao.Name == operand.ResultName && !ao.IsPersistentAssignment) != null)
                    {
                        symbol = "#";
                    }

                    operand.ResultMappedName = $"{symbol}{operand.ResultName}";
                }
            }
            else if (operand.OperatorSymbol == "ref" && operand.ResultName != "Alias")
            {
                if (operand.IsScalar)
                {
                    operand.ResultMappedName = $"@{operand.ExpressionText}";
                }
                else
                {
                    string symbol = string.Empty;
                    if (operand.ContainingSchema.AssignmentObjects.FirstOrDefault(ao => ao.Name == operand.ExpressionText && !ao.IsPersistentAssignment) != null)
                    {
                        symbol = "#";
                    }

                    operand.ResultMappedName = $"{symbol}{operand.ExpressionText}";
                }
            }
            else operand.ResultMappedName = operand.ExpressionText;
        }
    }
}
