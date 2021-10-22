namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.JoinBranches
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
        private readonly IExpressionFactory _exprFactory;
        private readonly IExpressionTextGenerator _exprTextGen;

        /// <summary>
        /// Inittializes a new instance of the <see cref="RenameBranch"/> class.
        /// </summary>
        /// <param name="exprFactory">The expression factory.</param>
        /// <param name="exprTextGen">The epression text generator.</param>
        public RenameBranch(IExpressionFactory exprFactory, IExpressionTextGenerator exprTextGen)
        {
            this._exprFactory = exprFactory;
            this._exprTextGen = exprTextGen;
        }

        public string Signature => "rename";

        public IExpression Build(IExpression datasetExpr)
        {
            /*
            This method is using mainly for dataset "comparison" operators. Example: DS_r := X = Y;
            The method can be used to generate a "rename" operator expression renaming a given dataset expression's single measure name to "bool_var".
            */

            IExpression renameBranch = this._exprFactory.GetExpression("rename", ExpressionFactoryNameTarget.OperatorSymbol);
            IExpression renameExpr = this._exprFactory.GetExpression("renameExpr", ExpressionFactoryNameTarget.OperatorSymbol);
            IExpression compExpr1 = this._exprFactory.GetExpression("comp", ExpressionFactoryNameTarget.OperatorSymbol);
            IExpression compExpr2 = this._exprFactory.GetExpression("comp", ExpressionFactoryNameTarget.OperatorSymbol);

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

            this._exprTextGen.Generate(renameExpr);
            this._exprTextGen.Generate(renameBranch);

            return renameBranch;
        }
    }
}
