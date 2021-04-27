namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.ComponentTools;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Modifiers.Utilities.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System.Linq;

    /// <summary>
    /// The "Component" operator definition.
    /// </summary>
    [OperatorSymbol("comp")]
    public class ComponentOperator : IOperatorDefinition
    {
        private readonly DataStructureResolver dsResolver;
        private readonly IComponentTypeInference compTypeInference;

        /// <summary>
        /// Initialises a new instance of the <see cref="ComponentOperator"/> class.
        /// </summary>
        /// <param name="dsResolver">The data structure resolver.</param>
        /// <param name="compTypeInference">The component type inferencer.</param>
        public ComponentOperator(DataStructureResolver dsResolver, IComponentTypeInference compTypeInference)
        {
            this.dsResolver = dsResolver;
            this.compTypeInference = compTypeInference;
        }

        public string Name => "Component";

        public string Symbol { get; set; } = "comp";

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            IDataStructure structure = this.dsResolver();
            IExpression parentExpr = expression.ParentExpression;

            if (parentExpr.OperatorSymbol == "#")
            {
                StructureComponent component = parentExpr.Operands["ds_1"].Structure.Components.FirstOrDefault(comp => comp.ComponentName.GetNameWithoutAlias() == expression.ExpressionText);
                if (component == null) throw new VtlOperatorError(expression, this.Name, $"Component {expression.ExpressionText} has been not found in dataset {parentExpr.Operands["ds_1"].ExpressionText}.");
 
                structure = this.dsResolver(component.ComponentName.GetNameWithoutAlias(), component.ComponentType, component.ValueDomain.DataType);
            }
            else if (parentExpr.OperatorSymbol == "unpivot")
            {
                if (expression.ParamSignature == "ds_1")
                {
                    structure = this.dsResolver(expression.ExpressionText, ComponentType.Identifier, BasicDataType.String); //string as nominal data
                }
                else
                {
                    //unpivot > datasetClause > dataset
                    if (parentExpr.ParentExpression?.Operands["ds_1"]?.Structure?.Measures.First() != null)
                    {
                        BasicDataType type = parentExpr.ParentExpression.Operands["ds_1"].Structure.Measures.First().ValueDomain.DataType;
                        structure = this.dsResolver(expression.ExpressionText, ComponentType.Measure, type);
                    }
                    structure = this.dsResolver(expression.ExpressionText, ComponentType.Measure, BasicDataType.None);
                }
            }
            else if ((parentExpr.OperatorSymbol != "calcExpr" || parentExpr.Operands["ds_1"] != expression) &&
                (parentExpr.OperatorSymbol != "renameExpr" || parentExpr.Operands["ds_2"] != expression))
            {
                this.compTypeInference.InferTypeOfComponent(expression);
                structure = expression.Structure.GetCopy();
            }
            else
            {
                BasicDataType dataType;
                ComponentType compType;
                if (parentExpr.OperatorSymbol == "calcExpr")
                {
                    dataType = BasicDataType.None;
                    switch (parentExpr.OperatorDefinition.Keyword)
                    {
                        case "identifier": compType = ComponentType.Identifier; break;
                        case "measure": compType = ComponentType.Measure; break;
                        case "attribute": compType = ComponentType.NonViralAttribute; break;
                        case "viral attribute": compType = ComponentType.ViralAttribute; break;
                        default: throw new VtlOperatorError(expression, this.Name, $"Unknown operator keyword: {parentExpr.OperatorDefinition.Keyword}");
                    }

                    structure = this.dsResolver(expression.ExpressionText, compType, dataType);
                }
                else if (parentExpr.OperatorSymbol == "renameExpr")
                {
                    StructureComponent baseComp = parentExpr.Operands["ds_1"].Structure.Components[0];
                    dataType = baseComp.ValueDomain.DataType;
                    compType = baseComp.ComponentType;

                    structure = this.dsResolver(expression.ExpressionText, compType, dataType);
                }
                else throw new VtlOperatorError(expression, this.Symbol, "Unknown component type.");
            }

            IRuleset ruleset = expression.ContainingSchema?.Rulesets.FirstOrDefault(ruleset => ruleset.RulesCollection.Contains(expression.GetFirstAncestorExpr() ?? expression));
            if (ruleset != null)
            {
                if (ruleset.Variables.Count > 0) structure.Components[0].BaseComponentName = ruleset.Variables[structure.Components[0].ComponentName];
                else structure.Components[0].BaseComponentName = ruleset.ValueDomains[structure.Components[0].ComponentName].Signature;
            }

            return structure;
        }
    }
}