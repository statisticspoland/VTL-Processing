namespace StatisticsPoland.VtlProcessing.Core.Infrastructure
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using System;

    /// <summary>
    /// Factory of <see cref="IExpression"/> objects.
    /// </summary>
    public sealed class ExpressionFactory : IExpressionFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionFactory"/> class.
        /// </summary>
        /// <param name="exprResolver">The expression resolver.</param>
        /// <param name="ruleExprResolver">The rule expression resolver.</param>
        /// <param name="joinExprResolver">The join expression resolver.</param>
        /// <param name="opResolver">The operator resolver.</param>
        public ExpressionFactory(ExpressionResolver exprResolver, RuleExpressionResolver ruleExprResolver, JoinExpressionResolver joinExprResolver, OperatorResolver opResolver)
        {
            this.ExprResolver = exprResolver;
            this.RuleExprResolver = ruleExprResolver;
            this.OperatorResolver = opResolver;
            this.JoinExprResolver = joinExprResolver;
        }

        public ExpressionResolver ExprResolver { get; }

        public RuleExpressionResolver RuleExprResolver { get; }

        public JoinExpressionResolver JoinExprResolver { get; }

        public OperatorResolver OperatorResolver { get; }

        public IExpression GetExpression(string name, ExpressionFactoryNameTarget nameTarget)
        {
            IExpression expr = this.ExprResolver();

            if (nameTarget == ExpressionFactoryNameTarget.ResultName)
            {
                expr.ResultName = name;
            }
            else
            {
                expr.OperatorDefinition = this.OperatorResolver(name);
                if (expr.OperatorDefinition == null)
                {
                    throw new Exception($"Unsuported operator: {name}");
                }

                expr.ResultName = expr.OperatorDefinition.Name;
            }

            return expr;
        }
    }
}
