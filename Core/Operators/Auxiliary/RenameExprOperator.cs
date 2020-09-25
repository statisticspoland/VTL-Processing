namespace StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System.Linq;

    /// <summary>
    /// The "Rename expression" operator definition.
    /// </summary>
    [OperatorSymbol("renameExpr")]
    public class RenameExprOperator : IOperatorDefinition
    {
        private readonly DataStructureResolver dsResolver;

        /// <summary>
        /// Initialises a new instance of the <see cref="RenameExprOperator"/> class.
        /// </summary>
        /// <param name="dsResolver">The data structure resolver.</param>
        public RenameExprOperator(DataStructureResolver dsResolver)
        {
            this.dsResolver = dsResolver;
        }

        public string Name => "Rename expression";

        public string Symbol => "renameExpr";

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            IExpression ds1 = expression.Operands["ds_1"];
            IExpression ds2 = expression.Operands["ds_2"];
            StructureComponent component = ds1.Structure.GetCopy().Components[0];

            component.ComponentName = ds2.ExpressionText;
            if (ds1.OperatorSymbol == "#")
            {
                component.BaseComponentName = $"{ds1.OperandsCollection.ToArray()[0].ExpressionText}#{ds1.Operands["ds_2"].ExpressionText}";
            }

            IDataStructure structure = this.dsResolver(string.Empty, component.ComponentType, component.ValueDomain.DataType);
            if (structure.Identifiers.Count > 0) structure.Identifiers[0] = component;
            if (structure.Measures.Count > 0) structure.Measures[0] = component;
            if (structure.NonViralAttributes.Count > 0) structure.NonViralAttributes[0] = component;
            if (structure.ViralAttributes.Count > 0) structure.ViralAttributes[0] = component;

            if (expression.CurrentJoinExpr != null)
            {
                foreach (IExpression alias in expression.CurrentJoinExpr.Operands["ds"].OperandsCollection)
                {
                    if (alias.Structure.Components.FirstOrDefault(comp => comp.ComponentName == component.ComponentName) != null)
                        throw new VtlOperatorError(expression, this.Name, $"Component named {structure.Components[0].ComponentName} already exists.");
                }
            }
            else if (expression.GetFirstAncestorExpr("Rename").ParentExpression.Operands["ds_1"].Structure.Components.FirstOrDefault(comp => comp.ComponentName == component.ComponentName) != null)
                throw new VtlOperatorError(expression, this.Name, $"Component named {structure.Components[0].ComponentName} already exists.");

            structure.Components[0].BaseComponentName = component.BaseComponentName;
            
            return structure;
        }
    }
}