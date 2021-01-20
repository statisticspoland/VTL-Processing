﻿namespace StatisticsPoland.VtlProcessing.Core.MiddleEnd.Utilities
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Modifiers.Utilities.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators;
    using StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary;
    using System.Linq;

    /// <summary>
    /// Infers basic data types of components.
    /// </summary>
    public class ComponentTypeInference : IComponentTypeInference
    {
        private DataStructureResolver dsResolver;

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
               this.InferByAggrAnalyticOperator(expr, componentType);

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
            if (expr.CurrentJoinExpr?.Operands["ds"]?.OperandsCollection?.Count >= 0)
            {
                foreach (IExpression alias in expr.CurrentJoinExpr.Operands["ds"].OperandsCollection)
                {
                    dataType = alias.Structure?.Components.FirstOrDefault(comp => comp.ComponentName == expr.ExpressionText && (comp.ComponentType == componentType || componentType == null))?.ValueDomain.DataType;
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
    }
}
