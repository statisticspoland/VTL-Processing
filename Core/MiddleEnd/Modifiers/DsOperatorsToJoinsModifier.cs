namespace StatisticsPoland.VtlProcessing.Core.MiddleEnd.Modifiers
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.MiddleEnd.Modifiers.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.MiddleEnd.Utilities;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Modifier replacing dataset operator expressions to "join" operators.
    /// </summary>
    public class DsOperatorsToJoinsModifier : ISchemaModifier
    {
        private readonly IExpressionFactory exprFactory;
        private readonly JoinExpressionResolver joinExprResolver;
        private readonly IJoinBuilder builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="DsOperatorsToJoinsModifier"/> class.
        /// </summary>
        /// <param name="exprFactory">The expression factory.</param>
        /// <param name="joinExprResolver">The "join" expression resolver.</param>
        /// <param name="builder">The join builder.</param>
        public DsOperatorsToJoinsModifier(IExpressionFactory exprFactory, JoinExpressionResolver joinExprResolver, IJoinBuilder builder)
        {
            this.exprFactory = exprFactory;
            this.joinExprResolver = joinExprResolver;
            this.builder = builder;
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

                if (this.IsJoinDatasetOperator(expression)) return this.TransformToJoin(expression);
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
            this.builder.BuildBranch("ds", expression);
            if (expression.OperatorSymbol.In(JoinOperators.ComparisonOperators) &&
                    (expression.Operands["ds_1"].Structure.Measures[0].ComponentName != "bool_var"
                    || expression.Operands["ds_2"].Structure.Measures[0].ComponentName != "bool_var"))
                this.builder.BuildBranch("rename", expression);

            if (!this.builder.Branches.ContainsKey("calc")) 
                this.builder.BuildBranch("apply", expression);

            expression.OperatorDefinition = this.exprFactory.OperatorResolver("join");
            expression.OperatorDefinition.Keyword = "inner";
            expression.Operands.Clear();

            expression.AddOperand("ds", this.builder.Branches["ds"]);
            expression = this.joinExprResolver(expression);

            this.builder.BuildBranch("using", expression);
            this.builder.AddMainExpr(expression);

            IJoinExpression joinExpr = this.builder.Build();
            this.builder.Clear();

            joinExpr.Structure = joinExpr.OperatorDefinition.GetOutputStructure(joinExpr);
            return joinExpr;
        }

        /// <summary>
        /// Checks if an expression is a dataset expression convertible to a "join" operator expression.
        /// </summary>
        /// <param name="expression">The expression to check.</param>
        /// <returns>The value specyfing if an expression is a dataset expression convertible to a "join" operator expression.</returns>
        private bool IsJoinDatasetOperator(IExpression expression)
        {
            if (!expression.OperatorSymbol.In("join", "#") && !expression.Operands["ds_1"].IsScalar && !expression.Operands["ds_2"].IsScalar)
                return true;
            return false;
        }
    }
}