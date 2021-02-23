namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.ComponentTools;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The "Period indicator" operator definition.
    /// </summary>
    [OperatorSymbol("period_indicator")]
    public class PeriodIndicatorOperator : IOperatorDefinition
    {
        private readonly IJoinApplyMeasuresOperator joinApplyMeasuresOp;
        private readonly DataStructureResolver dsResolver;
        private readonly IExpressionFactory exprFac;

        /// <summary>
        /// Initialises a new instance of the <see cref="NumericOperator"/> class.
        /// </summary>
        /// <param name="joinApplyMeasuresOp">The join apply measure operator.</param>
        /// <param name="dsResolver">The data structure resolver.</param>
        /// <param name="exprFac">The expression factory.</param>
        public PeriodIndicatorOperator(IJoinApplyMeasuresOperator joinApplyMeasuresOp, DataStructureResolver dsResolver, IExpressionFactory exprFac)
        {
            this.joinApplyMeasuresOp = joinApplyMeasuresOp;
            this.dsResolver = dsResolver;
            this.exprFac = exprFac;
        }

        public string Name => "Period indicator";

        public string Symbol => "period_indicator";

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            if (expression.IsApplyComponent) return this.joinApplyMeasuresOp.GetMeasuresStructure(expression);

            IExpression expr1 = expression.OperandsCollection.FirstOrDefault();
            if (expr1 == null) return this.ProcessNoParameterFunction(expression);
            if (expr1.IsScalar) return this.ProcessScalarParameterFunction(expression);
            return this.ProcessDatasetParameterFunction(expression);
        }

        /// <summary>
        /// Processes a no parameter version of an operator and returns the resulting structure.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>A dynamically defined structure.</returns>
        private IDataStructure ProcessNoParameterFunction(IExpression expression)
        {
            IExpression componentExpr = this.exprFac.GetExpression("comp", ExpressionFactoryNameTarget.OperatorSymbol);
            IExpression ancesorExpr = null;
            foreach (string name in DatasetClauseOperator.ClauseNames)
            {
                ancesorExpr = expression.GetFirstAncestorExpr(name);
                if (ancesorExpr != null) break;
            }

            if (ancesorExpr == null) throw new VtlOperatorError(expression, this.Name, "Expected function parameter.");
            if (expression.CurrentJoinExpr == null)
            {
                IExpression datasetExpr = ancesorExpr.ParentExpression.OperandsCollection.ToArray()[0];
                StructureComponent component = datasetExpr.Structure.Identifiers.FirstOrDefault(comp => comp.ValueDomain.DataType == BasicDataType.TimePeriod);
                if (component == null)
                    throw new VtlOperatorError(expression, this.Name, "Could not find time period data type identifier.");
                if (datasetExpr.Structure.Identifiers.Where(comp => comp.ValueDomain.DataType == BasicDataType.TimePeriod).ToArray().Length > 1)
                    throw new VtlOperatorError(expression, this.Name, "Found more than 1 identifier of time period data type.");

                componentExpr.ExpressionText = component.ComponentName.GetNameWithoutAlias();
            }
            else
            {
                string[] aliases = this.GetCompatibleAliases(expression.CurrentJoinExpr.Operands["ds"]);
                if (aliases.Length == 0) throw new VtlOperatorError(expression, this.Name, "Could not find time period data type identifier.");
                if (aliases.Length > 1) throw new VtlOperatorError(expression, this.Name, "Identifier of time period data type has been found in more than 1 join structure.");

                componentExpr.ExpressionText = expression.CurrentJoinExpr.GetAliasExpression(aliases[0]).Structure.Identifiers
                    .First(id => id.ValueDomain.DataType == BasicDataType.TimePeriod).ComponentName.GetNameWithoutAlias();
            }

            expression.AddOperand("ds_1", componentExpr);
            componentExpr.LineNumber = expression.LineNumber;
            componentExpr.Structure = componentExpr.OperatorDefinition.GetOutputStructure(componentExpr);
            
            IDataStructure structure = this.dsResolver("duration_var", ComponentType.Measure, BasicDataType.Duration);
            structure.Components[0].BaseComponentName = componentExpr.Structure.Components[0].BaseComponentName;
            return structure;
        }

        /// <summary>
        /// Processes a scalar version of the operator and returns the resulting structure.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>A dynamically defined structure.</returns>
        private IDataStructure ProcessScalarParameterFunction(IExpression expression)
        {
            IExpression expr1 = expression.OperandsCollection.ToArray()[0];
            if (expr1.Structure.Components[0].ValueDomain.DataType != BasicDataType.TimePeriod)
                throw new VtlOperatorError(expression, this.Name, "Expected time period data type parameter.");

            if (expr1.OperatorSymbol != "#" && expression.CurrentJoinExpr != null && this.GetCompatibleAliases(expression.CurrentJoinExpr.Operands["ds"]).Length > 1)
                throw new VtlOperatorError(expression, this.Name, "Identifier of time period data type has been found in more than 1 join structure.");

            IDataStructure structure = this.dsResolver("duration_var", ComponentType.Measure, BasicDataType.Duration);
            structure.Components[0].BaseComponentName = expr1.Structure.Components[0].BaseComponentName;
            return structure;
        }

        /// <summary>
        /// Processes a dataset version of the operator and returns the resulting structure.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>A dynamically defined structure.</returns>
        private IDataStructure ProcessDatasetParameterFunction(IExpression expression)
        {
            IExpression datasetExpr = expression.OperandsCollection.ToArray()[0];
            if (datasetExpr.Structure.Identifiers.Where(id => id.ValueDomain.DataType == BasicDataType.TimePeriod).ToArray().Length == 0)
                throw new VtlOperatorError(expression, this.Name, "Expected time period identifier.");

            if (datasetExpr.Structure.Identifiers.Where(id => id.ValueDomain.DataType == BasicDataType.TimePeriod).ToArray().Length > 1)
                throw new VtlOperatorError(expression, this.Name, "Found more than 1 identifier of time period data type.");

            IDataStructure structure = datasetExpr.Structure.GetCopy();
            structure.Measures.Clear();
            structure.Measures.Add(new StructureComponent(BasicDataType.Duration, "duration_var"));
            structure.Measures[0].BaseComponentName = datasetExpr.Structure.Identifiers
                .First(id => id.ValueDomain.DataType == BasicDataType.TimePeriod).BaseComponentName;

            return structure;
        }

        /// <summary>
        /// Gets names of component aliases of time perdiod data type components.
        /// </summary>
        /// <param name="expression">The expression to search component aliases.</param>
        /// <returns>An array of aliases.</returns>
        private string[] GetCompatibleAliases(IExpression expression)
        {
            List<string> signatures = new List<string>();
            if (expression.OperatorSymbol == "join") expression = expression.Operands["ds"];
            foreach (IExpression expr in expression.OperandsCollection)
            {
                if (expr.OperatorSymbol.In("get", "ref", "join"))
                {
                    if (expr.Structure.Components.Where(comp => comp.ValueDomain.DataType == BasicDataType.TimePeriod).ToArray().Length == 1)
                        signatures.Add(expr.ParamSignature);
                }
                else signatures.AddRange(this.GetCompatibleAliases(expr));
            }

            return signatures.ToArray();
        }
    }
}
