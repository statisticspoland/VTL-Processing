namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary.ComponentManagement;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System.Linq;

    /// <summary>
    /// The "Change of Component name" operator definition.
    /// </summary>
    [OperatorSymbol("rename")]
    public class RenameOperator : CompCollectorOperator, IOperatorDefinition
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="RenameOperator"/> class.
        /// </summary>
        /// <param name="dsResolver">The data structure resolver.</param>
        public RenameOperator(DataStructureResolver dsResolver)
            : base(dsResolver, "Rename")
        {
        }

        public string Name => "Rename";

        public string Symbol { get; set; } = "rename";

        public string Keyword { get; set; }

        public new IDataStructure GetOutputStructure(IExpression expression)
        {
            IDataStructure structure = base.GetOutputStructure(expression);
            IJoinExpression joinExpr = expression.CurrentJoinExpr;
            
            if (joinExpr?.Operands.ContainsKey("calc") == true)
            {
                foreach (StructureComponent renameComponent in structure.Components)
                {
                    if (joinExpr.Operands["calc"].Structure.Components.FirstOrDefault(calcComponent => calcComponent.ComponentName == renameComponent.ComponentName) != null &&
                        joinExpr.GetSubsetAliasStructure().Components.FirstOrDefault(dsComponent => dsComponent.ComponentName == renameComponent.ComponentName) == null)
                        throw new VtlOperatorError(expression, this.Name, $"Rename branch of \"join\" operator expression cannot use component name {renameComponent.ComponentName} because it's in use in \"calc\" branch.");
                }
            }

            return structure;
        }
    }
}
