namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.JoinBranches
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.JoinBranches.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators;

    /// <summary>
    /// The "apply" branch of the "join" operator expression builded from a dataset expression.
    /// </summary>
    public sealed class ApplyBranch : IJoinBranch
    {
        private readonly ExpressionResolver exprResolver;
        private IExpressionTextGenerator exprTextGen;

        /// <summary>
        /// Inittializes a new instance of the <see cref="ApplyBranch"/> class.
        /// </summary>
        /// <param name="exprResolver">Expression resolver.</param>
        /// <param name="exprTextGen">The epression text generator.</param>
        public ApplyBranch(ExpressionResolver exprResolver, IExpressionTextGenerator exprTextGen)
        {
            this.exprResolver = exprResolver;
            this.exprTextGen = exprTextGen;
        }

        public string Signature => "apply";

        public IExpression Build(IExpression datasetExpr)
        {
            IExpression applyBranch = this.exprResolver();

            applyBranch.ParamSignature = this.Signature;
            applyBranch.OperandsCollection = datasetExpr.OperandsCollection;
            applyBranch.OperatorDefinition = datasetExpr.OperatorDefinition;
            applyBranch.ResultName = "Apply";
            applyBranch.Structure = applyBranch.OperatorDefinition.GetOutputStructure(applyBranch);

            if (applyBranch.OperatorSymbol.In(AggrFunctionOperator.Symbols) || applyBranch.OperatorSymbol.In(AnalyticFunctionOperator.Symbols))
            {
                applyBranch.Operands.Remove("group");
                applyBranch.Operands.Remove("having");
                applyBranch.Operands.Remove("over");
            }

            this.exprTextGen.Generate(applyBranch);
            return applyBranch;
        }
    }
}
