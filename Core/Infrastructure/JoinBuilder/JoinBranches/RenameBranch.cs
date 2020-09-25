﻿namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.JoinBranches
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.JoinBranches.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;

    /// <summary>
    /// The "rename" branch of the "join" operator expression builded from a dataset expression.
    /// </summary>
    public sealed class RenameBranch : IJoinBranch
    {
        private readonly IExpressionFactory exprFactory;
        private IExpressionTextGenerator exprTextGen;

        /// <summary>
        /// Inittializes a new instance of the <see cref="RenameBranch"/> class.
        /// </summary>
        /// <param name="exprFactory">The expression factory.</param>
        /// <param name="exprTextGen">The epression text generator.</param>
        public RenameBranch(IExpressionFactory exprFactory, IExpressionTextGenerator exprTextGen)
        {
            this.exprFactory = exprFactory;
            this.exprTextGen = exprTextGen;
        }

        public string Signature => "rename";

        public IExpression Build(IExpression datasetExpr)
        {
            IExpression renameBranch = this.exprFactory.GetExpression("rename", ExpressionFactoryNameTarget.OperatorSymbol);
            IExpression renameExpr = this.exprFactory.GetExpression("renameExpr", ExpressionFactoryNameTarget.OperatorSymbol);
            IExpression compExpr1 = this.exprFactory.GetExpression("comp", ExpressionFactoryNameTarget.OperatorSymbol);
            IExpression compExpr2 = this.exprFactory.GetExpression("comp", ExpressionFactoryNameTarget.OperatorSymbol);

            renameBranch.OperatorDefinition.Keyword = "Variable";
            renameBranch.LineNumber = datasetExpr.LineNumber;
            renameExpr.LineNumber = datasetExpr.LineNumber;
            compExpr1.LineNumber = datasetExpr.LineNumber;
            compExpr2.LineNumber = datasetExpr.LineNumber;

            compExpr1.ExpressionText = datasetExpr.Structure.Measures[0].BaseComponentName;
            compExpr2.ExpressionText = datasetExpr.Structure.Measures[0].ComponentName;

            renameExpr.AddOperand("ds_1", compExpr1);
            renameExpr.AddOperand("ds_2", compExpr2);
            renameBranch.AddOperand("ds_1", renameExpr);

            this.exprTextGen.Generate(renameExpr);
            this.exprTextGen.Generate(renameBranch);

            return renameBranch;
        }
    }
}
