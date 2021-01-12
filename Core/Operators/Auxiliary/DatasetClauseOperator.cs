namespace StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The "Dataset clause" operator definition.
    /// </summary>
    [OperatorSymbol("datasetClause")]
    public class DatasetClauseOperator : IOperatorDefinition
    {
        /// <summary>
        /// Gets non-root result names of expressions using in a dataset clause operator.
        /// </summary>
        public static string[] ClauseNames => new string[] { "Calc", "Filter", "Keep", "Drop", "Rename", "Aggregation", "Pivot", "Unpivot", "Subspace" };

        public string Name => "Dataset clause";

        public string Symbol => "datasetClause";

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            IExpression expr1 = expression.OperandsCollection.ToArray()[0];
            IExpression expr2 = expression.OperandsCollection.ToArray()[1];

            IDataStructure structure = expr1.Structure.GetCopy();
            if (expr2.ResultName.In("Calc", "Aggregation")) structure.AddStructure(expr2.Structure);
            else if (expr2.ResultName.In("Keep", "Drop")) this.ProcessKeepDropClause(structure, expr2);
            else if (expr2.ResultName == "Rename") this.ProcessRenameClause(structure, expr2);
            else if (expr2.ResultName == "Pivot") this.ProcessPivotClause(structure, expr2);
            else if (expr2.ResultName == "Unpivot") this.ProcessUnpivotClause(structure, expr2);
            else if (expr2.ResultName == "Subspace") this.ProcessSubspaceClause(structure, expr2);

            return structure;
        }

        /// <summary>
        /// Precesses keeping/dropping operations at a given data structure.
        /// </summary>
        /// <param name="structure">The data structure.</param>
        /// <param name="keepDropExpr">The keep/drop expression.</param>
        private void ProcessKeepDropClause(IDataStructure structure, IExpression keepDropExpr)
        {
            List<string> compNames = new List<string>();
            foreach (StructureComponent measure in keepDropExpr.Structure.Measures)
            {
                compNames.Add(measure.ComponentName);
            }

            foreach (StructureComponent attribute in keepDropExpr.Structure.Components.Where(comp => comp.ComponentType.In(ComponentType.NonViralAttribute, ComponentType.ViralAttribute)))
            {
                compNames.Add(attribute.ComponentName);
            }

            bool isDrop = keepDropExpr.ResultName == "Drop";
            (structure.Measures as List<StructureComponent>).RemoveAll(me => me.ComponentName.In(compNames.ToArray()) == isDrop);
            (structure.ViralAttributes as List<StructureComponent>).RemoveAll(at => at.ComponentName.In(compNames.ToArray()) == isDrop);
            (structure.NonViralAttributes as List<StructureComponent>).RemoveAll(at => at.ComponentName.In(compNames.ToArray()) == isDrop);
        }

        /// <summary>
        /// Precesses renaming operations at a given data structure.
        /// </summary>
        /// <param name="structure">The data structure.</param>
        /// <param name="renameExpr">The rename expression.</param>
        private void ProcessRenameClause(IDataStructure structure, IExpression renameExpr)
        {
            foreach (StructureComponent identifier in renameExpr.Structure.Identifiers)
            {
                StructureComponent comp = structure.Identifiers.FirstOrDefault(id => id.BaseComponentName == identifier.BaseComponentName);
                comp.ComponentName = identifier.ComponentName;
            }

            foreach (StructureComponent measure in renameExpr.Structure.Measures)
            {
                StructureComponent comp = structure.Measures.FirstOrDefault(me => me.BaseComponentName == measure.BaseComponentName);
                comp.ComponentName = measure.ComponentName;
            }

            foreach (StructureComponent attribute in renameExpr.Structure.ViralAttributes)
            {
                StructureComponent comp = structure.ViralAttributes.FirstOrDefault(at => at.BaseComponentName == attribute.BaseComponentName);
                comp.ComponentName = attribute.ComponentName;
            }

            foreach (StructureComponent attribute in renameExpr.Structure.NonViralAttributes)
            {
                StructureComponent comp = structure.NonViralAttributes.FirstOrDefault(at => at.BaseComponentName == attribute.BaseComponentName);
                comp.ComponentName = attribute.ComponentName;
            }
        }

        /// <summary>
        /// Precesses pivoting operations at a given data structure.
        /// </summary>
        /// <param name="structure">The data structure.</param>
        /// <param name="pivotExpr">The pivot expression.</param>
        private void ProcessPivotClause(IDataStructure structure, IExpression pivotExpr)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Precesses unpivoting operations at a given data structure.
        /// </summary>
        /// <param name="structure">The data structure.</param>
        /// <param name="unpivotExpr">The unpivot expression.</param>
        private void ProcessUnpivotClause(IDataStructure structure, IExpression unpivotExpr)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Precesses subspacing operations at a given data structure.
        /// </summary>
        /// <param name="structure">The data structure.</param>
        /// <param name="subExpr">The subspace expression.</param>
        private void ProcessSubspaceClause(IDataStructure structure, IExpression subExpr)
        {
            string[] toRemove = subExpr.Structure.Identifiers.Select(s => s.ComponentName).ToArray();
            (structure.Identifiers as List<StructureComponent>).RemoveAll(r => r.ComponentName.In(toRemove));
        }
    }
}