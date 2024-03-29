﻿namespace StatisticsPoland.VtlProcessing.Core.MiddleEnd.Modifiers
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.MiddleEnd.Modifiers.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.MiddleEnd.Utilities;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators;
    using StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Modifier replacing dataset operator expressions to "join" operators.
    /// </summary>
    public class DsOperatorsToJoinsModifier : ISchemaModifier
    {
        private readonly IExpressionFactory _exprFactory;
        private readonly JoinExpressionResolver _joinExprResolver;
        private readonly IJoinBuilder _builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="DsOperatorsToJoinsModifier"/> class.
        /// </summary>
        /// <param name="exprFactory">The expression factory.</param>
        /// <param name="joinExprResolver">The "join" expression resolver.</param>
        /// <param name="builder">The join builder.</param>
        public DsOperatorsToJoinsModifier(IExpressionFactory exprFactory, JoinExpressionResolver joinExprResolver, IJoinBuilder builder)
        {
            this._exprFactory = exprFactory;
            this._joinExprResolver = joinExprResolver;
            this._builder = builder;
        }

        /// <summary>
        /// Performs replacing dataset operators to "join" operators.
        /// </summary>
        /// <param name="schema">The schema to modify.</param>
        public void Modify(ITransformationSchema schema)
        {
            foreach (AssignmentObject assignmentObject in schema.AssignmentObjects)
            {
                assignmentObject.Expression = this.TransformDatasets(assignmentObject.Expression);
            }
        }

        /// <summary>
        /// Transforms dataset expressions from a given expression to "join" operator expressions.
        /// </summary>
        /// <param name="expression">The expression to modify.</param>
        /// <returns>The modified expression.</returns>
        private IExpression TransformDatasets(IExpression expression)
        {
            if (!expression.IsScalar && expression.OperatorSymbol.In(new List<string>(JoinOperators.Operators) { "join", "#" }.ToArray()))
            {
                if (expression.OperatorSymbol == "join")
                {
                    IExpression[] dsAliases = expression.Operands["ds"].OperandsCollection.Where(alias => !alias.OperatorSymbol.In("get", "ref")).ToArray();
                    for (int i = 0; i < dsAliases.Length; i++)
                    {
                        expression.Operands["ds"].Operands[dsAliases[i].ParamSignature] = this.TransformDatasets(dsAliases[i]);
                    }
                }
                else
                {
                    if (expression.OperatorSymbol.In("datasetClause", "#") && !expression.OperandsCollection.ToArray()[0].IsScalar)
                        expression.Operands["ds_1"] = this.TransformDatasets(expression.Operands["ds_1"]);

                    for (int i = 0; i < expression.OperandsCollection.Count; i++)
                    {
                        if (expression.OperandsCollection.ToArray()[i].OperatorSymbol.In("datasetClause", "#"))
                            expression.Operands[$"ds_{i + 1}"] = this.TransformDatasets(expression.Operands[$"ds_{i + 1}"]);
                    }
                }

                if (expression.OperatorSymbol == "datasetClause" || this.IsJoinIfThenElse(expression) || this.IsJoinDatasetOperator(expression))
                    return this.TransformToJoin(expression);
            }

            return expression;
        }

        /// <summary>
        /// Transforms a dataset expression to a "join" operator expression.
        /// </summary>
        /// <param name="expression">The expression to modify.</param>
        /// <returns>The modified expression.</returns>
        private IJoinExpression TransformToJoin(IExpression expression)
        {
            IExpression additionalAliasExpr = null;

            if (expression.OperatorSymbol == "datasetClause")
            {
                if (expression.Operands["ds_1"].OperatorSymbol == "join")
                    additionalAliasExpr = expression.Operands["ds_1"];

                string resultName = expression.Operands["ds_2"].ResultName;

                if (!resultName.In(DatasetClauseOperator.ClauseNames))
                    throw new ArgumentException($"Unknown dataset clause symbol: {resultName}", "expression");

                if (resultName == "Aggregation")
                {
                    IExpression aggrExpr = expression.Operands["ds_2"];

                    this._builder.AddBranch("calc", aggrExpr.Operands["calc"]);
                    this._builder.AddBranch("group", aggrExpr.Operands["group"]);
                    if (aggrExpr.Operands.ContainsKey("having")) this._builder.AddBranch("having", aggrExpr.Operands["having"]);

                    this._builder.Branches["calc"].OperatorDefinition.Keyword = "Aggr";
                }
                else
                {
                    this._builder.AddBranch(resultName.ToLower(), expression.Operands["ds_2"]);
                    if (resultName == "Filter")
                    {
                        if (!this._builder.Branches["filter"].Structure.IsSingleComponent || this._builder.Branches["filter"].Structure.Components[0].ValueDomain.DataType != BasicDataType.Boolean)
                            throw new ArgumentException("Expected boolean single component expression as filter branch.", "expression");
                    }
                }
            }
            else if (expression.OperatorSymbol.In(AnalyticFunctionOperator.Symbols)) this._builder.AddBranch("over", expression.Operands["over"]);
            else if (expression.OperatorSymbol.In(AggrFunctionOperator.Symbols))
            {
                if (expression.Operands.ContainsKey("group"))
                {
                    this._builder.AddBranch("group", expression.Operands["group"]);
                    if (expression.Operands.ContainsKey("having")) this._builder.AddBranch("having", expression.Operands["having"]);
                    if (expression.OperatorSymbol == "count") this._builder.BuildBranch("calc", expression);
                }
                else this._builder.AddBranch("over", expression.Operands["over"]);
            }
            else if (expression.OperatorSymbol.In(JoinOperators.ComparisonOperators) &&
                    (expression.Operands["ds_1"].Structure.Measures[0].ComponentName != "bool_var"
                    || expression.Operands["ds_2"].Structure.Measures[0].ComponentName != "bool_var"))
                this._builder.BuildBranch("rename", expression);

            if (additionalAliasExpr == null) this._builder.BuildBranch("ds", expression);
            if (expression.OperatorSymbol != "datasetClause" && !this._builder.Branches.ContainsKey("calc")) this._builder.BuildBranch("apply", expression);

            expression.OperatorDefinition = this._exprFactory.OperatorResolver("join");
            expression.OperatorDefinition.Keyword = "inner";
            expression.Operands.Clear();

            if (additionalAliasExpr != null)
            {
                IExpression dsBranch = this._exprFactory.GetExpression("Alias", ExpressionFactoryNameTarget.ResultName);
                dsBranch.ExpressionText = $"{additionalAliasExpr.ExpressionText} as ds1";
                dsBranch.AddOperand("ds1", additionalAliasExpr);

                additionalAliasExpr.OperatorDefinition = this._exprFactory.OperatorResolver("join");
                additionalAliasExpr.OperatorDefinition.Keyword = "inner";
                additionalAliasExpr.ResultName = "Join";

                this._builder.Branches.Add("ds", dsBranch);
            }

            expression.AddOperand("ds", this._builder.Branches["ds"]);
            expression = this._joinExprResolver(expression);

            this._builder.BuildBranch("using", expression);
            this._builder.AddMainExpr(expression);

            IJoinExpression joinExpr = this._builder.Build();
            this._builder.Clear();

            joinExpr.Structure = joinExpr.OperatorDefinition.GetOutputStructure(joinExpr);
            return joinExpr;
        }

        /// <summary>
        /// Checks if an expression is an "if-then-else" operator expression convertaile to a "join" operator expression.
        /// </summary>
        /// <param name="expression">The expression to check.</param>
        /// <returns>The value indicating if an expression is an "if-then-else" operator expression convertible to a "join" operator expression.</returns>
        private bool IsJoinIfThenElse(IExpression expression)
        {
            if (expression.OperatorSymbol == "if" && 
                    (!expression.Operands["if"].IsScalar || !expression.Operands["then"].IsScalar || !expression.Operands["else"].IsScalar))
                return true;
            return false;
        }

        /// <summary>
        /// Checks if an expression is a dataset expression convertible to a "join" operator expression.
        /// </summary>
        /// <param name="expression">The expression to check.</param>
        /// <returns>The value indicating if an expression is a dataset expression convertible to a "join" operator expression.</returns>
        private bool IsJoinDatasetOperator(IExpression expression)
        {
            if (!expression.OperatorSymbol.In("join", "#", "if") && (expression.Operands.ContainsKey("group") || expression.Operands.ContainsKey("over") || (!expression.Operands["ds_1"].IsScalar && !expression.Operands["ds_2"].IsScalar)))
                return true;
            return false;
        }
    }
}