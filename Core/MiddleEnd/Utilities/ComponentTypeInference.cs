namespace StatisticsPoland.VtlProcessing.Core.MiddleEnd.Utilities
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Modifiers.Utilities.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators;
    using StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Infers basic data types of components.
    /// </summary>
    public class ComponentTypeInference : IComponentTypeInference
    {
        private readonly DataStructureResolver dsResolver;

        /// <summary>
        /// Initialises a new instance of the <see cref="ComponentTypeInference"/> class.
        /// </summary>
        /// <param name="dsResolver">The data structure resolver.</param>
        public ComponentTypeInference(DataStructureResolver dsResolver)
        {
            this.dsResolver = dsResolver;
        }

        public BasicDataType InferTypeOfComponent(IExpression expr, ComponentType? componentType = null)
        {
            BasicDataType? dataType =
                this.InferByJoinDsBranch(expr, componentType) ??
                this.InferByDatasetClauseOperator(expr, componentType) ??
                this.InferByAggrAnalyticOperator(expr, componentType) ??
                this.InferByCheckOperator(expr, componentType) ??
                this.InferByRuleset(expr, componentType);

            if (dataType != null) return (BasicDataType)dataType;

            string typePrefix = componentType != null ? " of a type " : string.Empty;
            throw new VtlOperatorError(expr, expr.OperatorSymbol, $"Component{typePrefix}{componentType} {expr.ExpressionText} has been not found in any dataset.");
        }

        /// <summary>
        /// Infers a basic data type of a component by a "join ds" branch expression.
        /// </summary>
        /// <param name="expr">The component expression.</param>
        /// <param name="componentType">The type of a component.</param>
        /// <returns>The basic data type type of a component.</returns>
        private BasicDataType? InferByJoinDsBranch(IExpression expr, ComponentType? componentType)
        {
            BasicDataType? dataType = null;
            if (expr.CurrentJoinExpr?.Operands["ds"]?.OperandsCollection != null)
            {
                foreach (IExpression alias in expr.CurrentJoinExpr.Operands["ds"].OperandsCollection)
                {
                    dataType = alias.Structure.Components.FirstOrDefault(comp => comp.ComponentName == expr.ExpressionText && (comp.ComponentType == componentType || componentType == null))?.ValueDomain.DataType;
                    if (dataType != null)
                    {
                        componentType = alias.Structure.Components.First(comp => comp.ComponentName == expr.ExpressionText && (comp.ComponentType == componentType || componentType == null)).ComponentType;
                        expr.Structure = this.dsResolver(expr.ExpressionText, (ComponentType)componentType, (BasicDataType)dataType);

                        return dataType;
                    }
                }
            }

            return dataType;
        }

        /// <summary>
        /// Infers a basic data type of a component by a "datasetClause" operator expression.
        /// </summary>
        /// <param name="expr">The component expression.</param>
        /// <param name="componentType">The type of a component.</param>
        /// <returns>The basic data type type of a component.</returns>
        private BasicDataType? InferByDatasetClauseOperator(IExpression expr, ComponentType? componentType)
        {
            BasicDataType? dataType = null;
            string[] datasetClauseNames = DatasetClauseOperator.ClauseNames;
            
            foreach (string name in datasetClauseNames)
            {
                IExpression clauseExpr = expr.GetFirstAncestorExpr(name);
                if (clauseExpr != null && clauseExpr.ParentExpression.OperandsCollection.ToArray()[0].Structure != null)
                {
                    IExpression sourceExpr = clauseExpr.ParentExpression.OperandsCollection.ToArray()[0];
                    dataType = sourceExpr.Structure.Components.FirstOrDefault(comp => comp.ComponentName == expr.ExpressionText && (comp.ComponentType == componentType || componentType == null))?.ValueDomain.DataType;
                    if (dataType != null)
                    {
                        componentType = sourceExpr.Structure.Components.First(comp => comp.ComponentName == expr.ExpressionText && (comp.ComponentType == componentType || componentType == null)).ComponentType;
                        expr.Structure = this.dsResolver(expr.ExpressionText, (ComponentType)componentType, (BasicDataType)dataType);

                        return dataType;
                    }
                }
            }

            return dataType;
        }

        /// <summary>
        /// Infers a basic data type of a component by an aggregation or analityc operator expression.
        /// </summary>
        /// <param name="expr">The component expression.</param>
        /// <param name="componentType">The type of a component.</param>
        /// <returns>The basic data type type of a component.</returns>
        private BasicDataType? InferByAggrAnalyticOperator(IExpression expr, ComponentType? componentType)
        {
            BasicDataType? dataType = null;
            IExpression functionExpr = expr.GetFirstAncestorExpr("Aggregation function");
            if (functionExpr == null && expr.GetFirstAncestorExpr("Over") != null) functionExpr = expr.GetFirstAncestorExpr("Analytic function");

            if (functionExpr == null) // operator AggrFunction / AnalyticFunction może mieć ResultName o nazwie zmiennej, jeżeli jest w korzeniu
                functionExpr = expr.GetFirstAncestorExpr(); // root

            if (functionExpr.OperatorSymbol != "rank" && (functionExpr.OperatorSymbol.In(AggrFunctionOperator.Symbols) || functionExpr.OperatorSymbol.In(AnalyticFunctionOperator.Symbols)))
            {
                dataType = functionExpr.Operands["ds_1"].Structure?.Components.FirstOrDefault(comp => comp.ComponentName == expr.ExpressionText && (comp.ComponentType == componentType || componentType == null))?.ValueDomain.DataType;
                if (dataType != null)
                {
                    componentType = functionExpr.Operands["ds_1"].Structure.Components?.First(comp => comp.ComponentName == expr.ExpressionText && (comp.ComponentType == componentType || componentType == null)).ComponentType;
                    expr.Structure = this.dsResolver(expr.ExpressionText, (ComponentType)componentType, (BasicDataType)dataType);
                }
            }

            return dataType;
        }

        /// <summary>
        /// Infers a basic data type of a component by a check operator expression.
        /// </summary>
        /// <param name="expr">The component expression.</param>
        /// <param name="componentType">The type of a component.</param>
        /// <returns>The basic data type type of a component.</returns>
        private BasicDataType? InferByCheckOperator(IExpression expr, ComponentType? componentType)
        {
            BasicDataType? dataType = null;
            IExpression checkExpr = null;
            string[] checkNames = new string[] { "Check datapoint", "Check Hierarchy", "Check" };
            string[] checkSymbols = new string[] { "check_datapoint", "check_hierarchy", "check" };

            foreach (string name in checkNames)
            {
                checkExpr = expr.GetFirstAncestorExpr(name);
            }
                
            if (checkExpr == null) // operator check może mieć ResultName o nazwie zmiennej, jeżeli jest w korzeniu
                checkExpr = expr.GetFirstAncestorExpr(); // root

            if (checkExpr.OperatorSymbol.In(checkSymbols))
            {
                IExpression sourceExpr = checkExpr.Operands["ds_1"];
                dataType = sourceExpr.Structure.Components.FirstOrDefault(comp => comp.ComponentName == expr.ExpressionText && (comp.ComponentType == componentType || componentType == null))?.ValueDomain.DataType;
                if (dataType != null)
                {
                    componentType = sourceExpr.Structure.Components.First(comp => comp.ComponentName == expr.ExpressionText && (comp.ComponentType == componentType || componentType == null)).ComponentType;
                    expr.Structure = this.dsResolver(expr.ExpressionText, (ComponentType)componentType, (BasicDataType)dataType);

                    return dataType;
                }
            }
            
            return dataType;
        }

        /// <summary>
        /// Infers a basic data type of a component by a ruleset.
        /// </summary>
        /// <param name="expr">The component expression.</param>
        /// <param name="componentType">The type of a component.</param>
        /// <returns>The basic data type type of a component.</returns>
        private BasicDataType? InferByRuleset(IExpression expr, ComponentType? componentType)
        {
            BasicDataType? dataType = null;
            IRuleExpression ruleExpr = expr.GetFirstAncestorExpr() as IRuleExpression;
            
            if (ruleExpr != null && componentType.In(ComponentType.Measure, null))
            {
                componentType = ComponentType.Measure;

                IRuleset ruleset = ruleExpr.ContainingRuleset;
                if (ruleset.Variables.ContainsKey(expr.ExpressionText))
                {
                    string parentOpSymbol = expr.ParentExpression.OperatorSymbol;

                    if (parentOpSymbol.In("match_characters", "trim", "rtrim", "ltrim", "upper", "lower", "replace", "length")) dataType = BasicDataType.String;
                    else if (parentOpSymbol == "period_indicator") dataType = BasicDataType.TimePeriod;
                    else if (parentOpSymbol == "substr")
                    {
                        if (expr.ParentExpression.Operands["ds_1"].ExpressionText == expr.ExpressionText) dataType = BasicDataType.String;
                        else dataType = BasicDataType.Integer;
                    }
                    else if (parentOpSymbol == "instr")
                    {
                        if (expr.ParentExpression.OperandsCollection.FirstOrDefault(op => op.ParamSignature.In("ds_1", "ds_2") && op.ExpressionText == expr.ExpressionText) != null)
                            dataType = BasicDataType.String;
                        else dataType = BasicDataType.Integer;
                    }
                    else if (parentOpSymbol.In("mod", "round", "trunc", "log"))
                    {
                        if (expr.ParentExpression.Operands["ds_1"].ExpressionText == expr.ExpressionText) dataType = BasicDataType.Number;
                        else dataType = BasicDataType.Integer;
                    }
                    else if (parentOpSymbol == "time_agg")
                    {
                        if (expr.ParentExpression.Operands["ds_1"].ExpressionText == expr.ExpressionText) dataType = BasicDataType.Time;
                        else dataType = BasicDataType.Duration;
                    }
                    else if (parentOpSymbol.In("between", "in", "not_in"))
                    {
                        IEnumerable<IExpression> operands = expr.ParentExpression.OperandsCollection.Where(op => op.ExpressionText != expr.ExpressionText);
                        if (parentOpSymbol != "between") operands = expr.ParentExpression.Operands["ds_2"].OperandsCollection;

                        foreach (IExpression operand in operands)
                        {
                            if (dataType != BasicDataType.Number)
                            {
                                operand.Structure = operand.OperatorDefinition.GetOutputStructure(operand);
                                dataType = operand.Structure.Components[0].ValueDomain.DataType;
                            }
                        }
                    }
                    else if (parentOpSymbol == "if")
                    {
                        if (expr.ParentExpression.Operands["if"].ExpressionText == expr.ExpressionText) dataType = BasicDataType.Boolean;
                        else
                        {
                            if (expr.ParentExpression.Operands["then"].ExpressionText == expr.ExpressionText && expr.ParentExpression.Operands["else"].ExpressionText != expr.ExpressionText)
                            {
                                IExpression operand = expr.ParentExpression.Operands["else"];
                                operand.Structure = operand.OperatorDefinition.GetOutputStructure(operand);
                                dataType = operand.Structure.Components[0].ValueDomain.DataType;
                            }
                            else
                            if (expr.ParentExpression.Operands["else"].ExpressionText == expr.ExpressionText && expr.ParentExpression.Operands["then"].ExpressionText != expr.ExpressionText)
                            {
                                IExpression operand = expr.ParentExpression.Operands["else"];
                                operand.Structure = operand.OperatorDefinition.GetOutputStructure(operand);
                                dataType = operand.Structure.Components[0].ValueDomain.DataType;
                            }
                        }
                    }
                    else
                    {
                        IExpression operand = expr.ParentExpression.OperandsCollection.FirstOrDefault(op => op.ExpressionText != expr.ExpressionText);
                        if (operand != null && operand.OperatorSymbol != null)
                        {
                            operand.Structure = operand.OperatorDefinition.GetOutputStructure(operand);
                            dataType = operand.Structure.Components[0].ValueDomain.DataType;
                        }
                    }

                    if (dataType == null) dataType = BasicDataType.None;
                    expr.Structure = this.dsResolver(expr.ExpressionText, (ComponentType)componentType, (BasicDataType)dataType);
                        
                    return dataType;
                }

                if (ruleset.ValueDomains.ContainsKey(expr.ExpressionText))
                {
                    expr.Structure = this.dsResolver();
                    expr.Structure.Measures.Add(new StructureComponent(ruleset.ValueDomains[expr.ExpressionText], expr.ExpressionText, (ComponentType)componentType));

                    return ruleset.ValueDomains[expr.ExpressionText].DataType;
                }
            }

            return dataType;
        }
    }
}
