namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.JoinBranches.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The "join" operator expressions builder.
    /// </summary>
    public sealed class JoinBuilder : IJoinBuilder
    {
        private readonly JoinExpressionResolver joinExprResolver;
        private readonly IEnumerable<IJoinBranch> joinBranches;

        /// <summary>
        /// Initializes a new instance of the <see cref="JoinBuilder"/> class.
        /// </summary>
        /// <param name="joinExprResolver">The join expression resolver.</param>
        /// <param name="joinBranches">The join branches preparers.</param>
        public JoinBuilder(JoinExpressionResolver joinExprResolver, IEnumerable<IJoinBranch> joinBranches)
        {
            this.joinExprResolver = joinExprResolver;
            this.Branches = new Dictionary<string, IExpression>();
            this.IsCleared = true;
            this.joinBranches = joinBranches;
        }

        public IExpression this[string key] => this.Branches[key];

        public Dictionary<string, IExpression> Branches { get; }

        public bool IsCleared { get; private set; }

        public IJoinBuilder AddMainExpr(IExpression mainExpr)
        {
            this.Branches.Remove("main");
            this.Branches.Add("main", mainExpr);

            this.IsCleared = false;
            return this;
        }

        public IJoinBuilder AddBranch(string key, IExpression branch)
        {
            this.Branches.Remove(key);
            this.Branches.Add(key, branch);

            this.IsCleared = false;
            return this;
        }

        public IJoinBuilder Clear()
        {
            this.Branches.Clear();
            this.IsCleared = true;
            return this;
        }

        public IJoinExpression Build()
        {
            IExpression mainExpr = this.Branches["main"];

            mainExpr.AddOperand("ds", this.Branches["ds"]);
            mainExpr = this.joinExprResolver(mainExpr);

            if (this.Branches.ContainsKey("using"))
            {
                if (mainExpr.OperatorDefinition.Keyword.In("full", "cross"))
                    throw new VtlOperatorError(mainExpr, "join", "Cannot add \"using\" branch to \"full\" and \"cross\" \"join\" operator expression.");

                mainExpr.AddOperand("using", this.Branches["using"]);
            }

            if (this.Branches.ContainsKey("filter")) mainExpr.AddOperand("filter", this.Branches["filter"]);

            if (this.Branches.ContainsKey("apply") && this.Branches.ContainsKey("calc"))
                throw new VtlOperatorError(mainExpr, "join", "\"Join\" expression can't contain \"apply\" and \"calc\" branches at the same time.");

            if (this.Branches.ContainsKey("apply") && this.Branches.ContainsKey("aggr"))
                throw new VtlOperatorError(mainExpr, "join", "\"Join\" expression can't contain \"aggr\" and \"apply\" branches at the same time.");

            if (this.Branches.ContainsKey("calc") && this.Branches.ContainsKey("aggr"))
                throw new VtlOperatorError(mainExpr, "join", "\"Join\" expression can't contain \"aggr\" and \"calc\" branches at the same time.");

            if (this.Branches.ContainsKey("apply")) mainExpr.AddOperand("apply", this.Branches["apply"]);
            if (this.Branches.ContainsKey("calc")) mainExpr.AddOperand("calc", this.Branches["calc"]);

            if (this.Branches.ContainsKey("keep") && this.Branches.ContainsKey("drop"))
                throw new VtlOperatorError(mainExpr, "join", "\"Join\" expression can't contain \"keep\" and \"drop\" branches at the same time.");

            if (this.Branches.ContainsKey("keep")) mainExpr.AddOperand("keep", this.Branches["keep"]);
            if (this.Branches.ContainsKey("drop")) mainExpr.AddOperand("drop", this.Branches["drop"]);
            if (this.Branches.ContainsKey("rename"))
            {
                mainExpr.AddOperand("rename", this.Branches["rename"]);
                this.Branches["rename"].SetContainingSchema(mainExpr.ContainingSchema);

                if (this.Branches["rename"].OperatorDefinition.Keyword == "Variable")
                {
                    this.ReinferTypes(this.Branches["rename"]);
                    this.Branches["rename"].OperatorDefinition.Keyword = null;
                }
            }

            if (this.Branches.ContainsKey("aggr")) mainExpr.AddOperand("aggr", this.Branches["aggr"]);

            mainExpr.SetContainingSchema(mainExpr.ContainingSchema);

            foreach (IExpression branch in this.Branches.Values)
            {
                branch.LineNumber = mainExpr.LineNumber;
            }

            return (IJoinExpression)mainExpr;
        }

        public IExpression BuildBranch(string key, IExpression datasetExpr)
        {
            this.Branches.Remove(key);
            this.Branches.Add(key, this.joinBranches.FirstOrDefault(jb => jb.Signature == key).Build(datasetExpr));

            this.IsCleared = false;
            return this.Branches[key];
        }

        /// <summary>
        /// Reinfers types of a given expression.
        /// </summary>
        /// <param name="expression">The expression to reinfer types of.</param>
        private void ReinferTypes(IExpression expression)
        {
            foreach (IExpression expr in expression.OperandsCollection)
            {
                this.ReinferTypes(expr);
            }

            expression.Structure = expression.OperatorDefinition.GetOutputStructure(expression);
        }
    }
}
