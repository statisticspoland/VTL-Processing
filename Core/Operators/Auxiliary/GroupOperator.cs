namespace StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary.ComponentManagement;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System.Linq;

    /// <summary>
    /// The "Group" operator definition.
    /// </summary>
    [OperatorSymbol("group")]
    public class GroupOperator : CompCollectorOperator, IOperatorDefinition
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="GroupOperator"/> class.
        /// </summary>
        /// <param name="dsResolver">The data structure resolver.</param>
        public GroupOperator(DataStructureResolver dsResolver)
            : base(dsResolver, "Group")
        {
        }

        public string Name => "Group";

        public string Symbol => "group";

        public string Keyword { get; set; }

        public new IDataStructure GetOutputStructure(IExpression expression)
        {
            IDataStructure dataStructure = base.GetOutputStructure(expression);

            foreach (StructureComponent component in dataStructure.Components)
            {
                if (component.ComponentType != ComponentType.Identifier) throw new VtlOperatorError(expression, this.Name, "Expeceted identifier");
            }

            if (expression.OperatorDefinition.Keyword == "except")
            {
                IDataStructure mainStructure;
                if (expression.CurrentJoinExpr != null) mainStructure = expression.CurrentJoinExpr.GetSupersetAliasStructure().GetCopy();
                else mainStructure = expression.ParentExpression.Operands["ds_1"].Structure.GetCopy();

                if (mainStructure.Identifiers.Count == dataStructure.Identifiers.Count)
                    throw new VtlOperatorError(expression, this.Name, "Removing all identifiers from grouping clause is not valid operation.");
            }

            for (int i = 0; i < expression.OperandsCollection.Count; i++)
            {
                string exprText = expression.OperandsCollection.ToArray()[i].ExpressionText;
                if (exprText.Contains("#"))
                    dataStructure.Components.First(comp => comp.ComponentName == exprText.Split('#').Last()).ComponentName = exprText;
            }

            return dataStructure;
        }
    }
}
