namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System.Linq;

    /// <summary>
    /// The "Check datapoint" operator definition.
    /// </summary>
    [OperatorSymbol("check_datapoint")]
    public class CheckDatapointOperator : IOperatorDefinition
    {
        private readonly DataStructureResolver _dsResolver;
        private readonly IExpressionFactory _exprFac;

        /// <summary>
        /// Initialises a new instance of the <see cref="CheckDatapointOperator"/> class.
        /// </summary>
        /// <param name="dsResolver">The data structure resolver.</param>
        /// <param name="exprFac">The expression factory.</param>
        public CheckDatapointOperator(DataStructureResolver dsResolver, IExpressionFactory exprFac)
        {
            this._dsResolver = dsResolver;
            this._exprFac = exprFac;
        }

        public string Name => "Check datapoint";

        public string Symbol { get; set; } = "check_datapoint";

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            this.Validate(expression);

            IDataStructure structure = this._dsResolver();
            structure.Identifiers = expression.Operands["ds_1"].Structure.GetCopy().Identifiers;
            structure.Identifiers.Add(new StructureComponent(BasicDataType.String, "ruleid"));
            
            if (this.Keyword != "all") structure.Measures = expression.Operands["ds_1"].Structure.GetCopy().Measures;
            if (this.Keyword != "invalid") structure.Measures.Add(new StructureComponent(BasicDataType.Boolean, "bool_var"));

            structure.Measures.Add(new StructureComponent(BasicDataType.String, "errorcode"));
            structure.Measures.Add(new StructureComponent(BasicDataType.Integer, "errorlevel"));
            structure.ViralAttributes = expression.Operands["ds_1"].Structure.GetCopy().ViralAttributes;
            structure.NonViralAttributes = expression.Operands["ds_1"].Structure.GetCopy().NonViralAttributes;

            if (expression.Operands.ContainsKey("comps")) 
            {
                StructureComponent[] nonViralAttributes = structure.NonViralAttributes
                        .Where(at => at.ComponentName.In(expression.Operands["comps"].OperandsCollection.Select(op => op.ExpressionText).ToArray())).ToArray();

                foreach (StructureComponent attribute in nonViralAttributes)
                {
                    structure.NonViralAttributes.Remove(attribute);
                    structure.ViralAttributes.Add(attribute);
                }
            }

            return structure;
        }

        private void Validate(IExpression expr)
        {
            IRuleset ruleset = expr.ContainingSchema.Rulesets.FirstOrDefault(ruleset => ruleset.Name == expr.Operands["ruleset"].ExpressionText);

            if (ruleset == null)
                throw new VtlOperatorError(expr, this.Symbol, $"Ruleset {expr.Operands["ruleset"].ExpressionText} has been not found.");

            if (expr.Operands.ContainsKey("comps")) // components list specified
            {
                IExpression compsExpr = expr.Operands["comps"];
                if (ruleset.Variables.Count > 0) // Variables validation
                {
                    if (compsExpr.Structure.Components.Count != ruleset.Variables.Count)
                        throw new VtlOperatorError(expr, this.Symbol, $"A components collection items count is not equal to a ruleset's variables collection items count.");

                    for (int i = 0; i < compsExpr.Structure.Components.Count; i++)
                    {
                        string compName = compsExpr.Structure.Components[i].ComponentName;
                        StructureComponent comp = ruleset.Structure.Components.First(comp => comp.ComponentName == ruleset.Variables.ToArray()[i].Key);
                        if (!comp.ValueDomain.DataType.EqualsObj(compsExpr.Structure.Components[i].ValueDomain.DataType))
                            throw new VtlOperatorError(expr, this.Symbol, $"A data type of a component in the components collection is not the same as a component's from a ruleset's variables collection. (Component {compName})");
                    }
                }
                else // Value domains validation
                {
                    if (compsExpr.OperandsCollection.Count != ruleset.ValueDomains.Count)
                        throw new VtlOperatorError(expr, this.Symbol, $"A components collection items count is not equal to a ruleset's valuedomains collection items count.");

                    for (int i = 0; i < compsExpr.OperandsCollection.Count; i++)
                    {
                        string compName = compsExpr.OperandsCollection.ToArray()[i].ExpressionText;
                        if (!ruleset.ValueDomains[ruleset.ValueDomains.Keys.ToArray()[i]].DataType.EqualsObj(compsExpr.OperandsCollection.ToArray()[i].Structure.Components[0].ValueDomain.DataType))
                            throw new VtlOperatorError(expr, this.Symbol, $"A data type of a component in the components collection is not the same as a valuedomain's from a ruleset's valedomains collection. (Component {compName})");
                    }
                }
            }
            else // components list not specified
            {
                IExpression collectionExpr = this._exprFac.GetExpression("collection", ExpressionFactoryNameTarget.OperatorSymbol);
                collectionExpr.ExpressionText = "components ";
                collectionExpr.LineNumber = expr.LineNumber;

                expr.AddOperand("comps", collectionExpr); // new operand

                if (ruleset.Variables.Count > 0) // Variables validation
                {
                    foreach (string variableName in ruleset.Variables.Select(var => var.Key))
                    {
                        StructureComponent component = expr.Operands["ds_1"].Structure.Components.FirstOrDefault(comp => comp.ComponentName == ruleset.Variables[variableName]);
                        if (component != null)
                        {
                            StructureComponent comp = ruleset.Structure.Components.First(comp => comp.ComponentName == variableName);
                            if (!comp.ValueDomain.DataType.EqualsObj(component.ValueDomain.DataType))
                                throw new VtlOperatorError(expr, this.Symbol, $"A data type of a component in the components collection is not the same as a component's from a ruleset's variables collection. (Component {component.ComponentName})");

                            this.CreateComponentExpression(component.ComponentName, expr.LineNumber, collectionExpr);
                        }
                        else throw new VtlOperatorError(expr, this.Symbol, $"A datapoint structure does not contain a component named {variableName}.");
                    }
                }
                else // Value domains validation
                {
                    foreach (ValueDomain valueDomain in ruleset.ValueDomains.Values)
                    {
                        StructureComponent compNotNull = expr.Operands["ds_1"].Structure.Components.FirstOrDefault(comp => comp.ValueDomain.DataType == valueDomain.DataType);
                        if (compNotNull == null)
                            throw new VtlOperatorError(expr, this.Symbol, $"A component of a data type of the value domain \"{valueDomain.Signature}\" has been not found.");

                        this.CreateComponentExpression(compNotNull.ComponentName, expr.LineNumber, collectionExpr);
                    }
                }

                for (int i = 0; i < collectionExpr.OperandsCollection.Count; i++)
                {
                    collectionExpr.ExpressionText += collectionExpr.Operands[$"ds_{i + 1}"].ExpressionText;
                    if (i < collectionExpr.OperandsCollection.Count - 1) collectionExpr.ExpressionText += ", ";
                }

                collectionExpr.Structure = collectionExpr.OperatorDefinition.GetOutputStructure(collectionExpr);
            }
        }

        /// <summary>
        /// Creates and returns a component expression.
        /// </summary>
        /// <param name="expressionText">The expression text.</param>
        /// <param name="lineNumber">The line number.</param>
        /// <param name="parentExpr">The parent expression.</param>
        /// <returns>A component expression.</returns>
        private IExpression CreateComponentExpression(string expressionText, int lineNumber, IExpression parentExpr)
        {
            IExpression componentExpr = this._exprFac.GetExpression("comp", ExpressionFactoryNameTarget.OperatorSymbol);
            componentExpr.ExpressionText = expressionText;
            componentExpr.LineNumber = lineNumber;

            parentExpr.AddOperand($"ds_{parentExpr.OperandsCollection.Count + 1}", componentExpr);
            componentExpr.Structure = componentExpr.OperatorDefinition.GetOutputStructure(componentExpr);

            return componentExpr;
        }
    }
}