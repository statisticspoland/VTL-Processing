namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.JoinBranches.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.MiddleEnd.Utilities;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The "join" operator expressions builder.
    /// </summary>
    public sealed class JoinBuilder : IJoinBuilder
    {
        private readonly JoinExpressionResolver _joinExprResolver;
        private readonly IEnumerable<IJoinBranch> _joinBranches;

        /// <summary>
        /// Initializes a new instance of the <see cref="JoinBuilder"/> class.
        /// </summary>
        /// <param name="joinExprResolver">The join expression resolver.</param>
        /// <param name="joinBranches">The join branches preparers.</param>
        public JoinBuilder(JoinExpressionResolver joinExprResolver, IEnumerable<IJoinBranch> joinBranches)
        {
            this._joinExprResolver = joinExprResolver;
            this.Branches = new Dictionary<string, IExpression>();
            this.IsCleared = true;
            this._joinBranches = joinBranches;
        }

        public IExpression this[string key] => this.Branches[key];

        public Dictionary<string, IExpression> Branches { get; }

        public bool IsCleared { get; private set; }

        public IJoinBuilder AddMainExpr(IExpression mainExpr)
        {
            this.Branches.Remove("main");

            if (!mainExpr.IsScalar && mainExpr.OperatorSymbol.In(JoinOperators.Operators))
                this.Branches.Add("main", mainExpr);
            else this.Branches.Add("main", this._joinExprResolver(mainExpr));

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

            if (!this.Branches.ContainsKey("ds"))
                throw new VtlOperatorError(mainExpr, "join", "\"Join\" expression expects a \"ds\" branch.");

            mainExpr.AddOperand("ds", this.Branches["ds"]);
            mainExpr = this._joinExprResolver(mainExpr);

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
            if (this.Branches.ContainsKey("calc"))
            {
                mainExpr.AddOperand("calc", this.Branches["calc"]);
                this.Branches["calc"].SetContainingSchema(mainExpr.ContainingSchema);

                if (this.Branches["calc"].OperatorDefinition.Keyword == "Aggr Built")
                {
                    this.ReinferTypes(this.Branches["calc"]);
                    this.Branches["calc"].OperatorDefinition.Keyword = "Aggr";
                }
            }

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
            if (this.Branches.ContainsKey("group")) mainExpr.AddOperand("group", this.Branches["group"]);
            if (this.Branches.ContainsKey("having")) mainExpr.AddOperand("having", this.Branches["having"]);
            if (this.Branches.ContainsKey("over")) mainExpr.AddOperand("over", this.Branches["over"]);

            this.ValidateClause();

            if (this.Branches.ContainsKey("pivot")) mainExpr.AddOperand("pivot", this.Branches["pivot"]);
            if (this.Branches.ContainsKey("unpivot")) mainExpr.AddOperand("unpivot", this.Branches["unpivot"]);

            if (this.Branches.ContainsKey("subspace")) mainExpr.AddOperand("subspace", this.Branches["subspace"]);

            mainExpr.SetContainingSchema(mainExpr.ContainingSchema);

            foreach (IExpression branch in this.Branches.Values)
            {
                branch.LineNumber = mainExpr.LineNumber;
            }

            this.Branches["main"] = mainExpr;
            return (IJoinExpression)mainExpr;
        }

        public IExpression BuildBranch(string key, IExpression datasetExpr)
        {
            IJoinBranch joinBranch = this._joinBranches.FirstOrDefault(jb => jb.Signature == key);

            if (joinBranch == null) throw new Exception($"Join branch named \"{key}\" has been not found.");

            this.Branches.Remove(key);
            this.Branches.Add(key, joinBranch.Build(datasetExpr));

            this.IsCleared = false;
            return this.Branches[key];
        }

        /// <summary>
        /// Validates possible pivot/unpivot/subspace clause.
        /// </summary>
        private void ValidateClause()
        {
            string clause = this.Branches.ContainsKey("pivot") ? "pivot" :
                this.Branches.ContainsKey("unpivot") ? "unpivot" :
                this.Branches.ContainsKey("subspace") ? "subspace" : null;

            if (clause != null)
            {
                bool isError = false;
                if (this.Branches.Count > 4) isError = true;
                else if (this.Branches.Count == 4 && !(this.Branches.ContainsKey("ds") && this.Branches.ContainsKey("using")))
                    isError = true;
                else if (this.Branches.Count == 3 && !(this.Branches.ContainsKey("ds") || this.Branches.ContainsKey("using")))
                    isError = true;
                
                if (isError)
                    throw new VtlOperatorError(this.Branches["main"], "join", $"\"Join\" expression can't contain \"{clause}\" and other than \"ds\" and \"using\" branches.");
            }
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
