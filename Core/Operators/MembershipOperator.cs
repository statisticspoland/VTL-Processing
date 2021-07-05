namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.ComponentTools;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The "Membership" operator definition.
    /// </summary>
    [OperatorSymbolAttribute("#")]
    public class MembershipOperator : IOperatorDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MembershipOperator"/> class.
        /// </summary>
        public MembershipOperator()
        {
            this.Keyword = "Standard";
        }

        public string Name => "Membership";

        public string Symbol { get; set; } = "#";

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            IExpression expr1 = expression.OperandsCollection.ToArray()[0];
            IExpression expr2 = expression.Operands["ds_2"];

            IDataStructure structure = expr1.Structure.GetCopy();

            if (this.Keyword == "Standard" || (this.Keyword == "DatasetClause" && expression.ParentExpression?.OperatorSymbol == "datasetClause"))
            {
                StructureComponent component = expr2.Structure.Components[0];

                this.TransformStructrure(structure, component);
                if (structure.IsSingleComponent) throw new VtlOperatorError(expression, this.Name, $"Expected dataset as first parameter.");
                structure.Measures[0].BaseComponentName = component.BaseComponentName;

                if (expr1.OperatorSymbol == "join") this.RemoveAliases(structure);
            }
            else if (this.Keyword.In("Component", "DatasetClause"))
            {
                structure = expr2.Structure.GetCopy();
                expr2.Structure.DatasetName = structure.DatasetName;
            }
            else throw new VtlOperatorError(expression, this.Name, $"Unknown operator keyword: {this.Keyword}");

            return structure;
        }

        /// <summary>
        /// Transforms a given structure into a standard membership structure.
        /// </summary>
        /// <param name="expr">The membership operator expression.</param>
        /// <param name="structure">The data structure./param>
        /// <param name="component">The membership component.</param>
        private void TransformStructrure(IDataStructure structure, StructureComponent component)
        {
            ComponentType componentType = component.ComponentType;
            string componentName = component.ComponentName;

            component = new StructureComponent(component.ValueDomain.DataType, component.ComponentName);

            (structure.Measures as List<StructureComponent>).RemoveAll(
                me => me.ComponentName != (componentType == ComponentType.Measure ? componentName : string.Empty));

            structure.NonViralAttributes.Remove(
                structure.Measures.FirstOrDefault(me => me.ComponentName == (componentType == ComponentType.NonViralAttribute ? componentName : string.Empty)));

            structure.ViralAttributes.Remove(
                structure.Measures.FirstOrDefault(me => me.ComponentName == (componentType == ComponentType.ViralAttribute ? componentName : string.Empty)));

            if (componentType != ComponentType.Measure)
            {
                component.ComponentName = component.ValueDomain.DataType.GetVariableName();
                structure.Measures.Add(component);
            }
        }

        /// <summary>
        /// Removes aliases from components names of a given structure.
        /// </summary>
        /// <param name="structure">The data structure.</param>
        private void RemoveAliases(IDataStructure structure)
        {
            foreach (StructureComponent component in structure.Components)
            {
                component.ComponentName = component.ComponentName.GetNameWithoutAlias();
            }
        }
    }
}
