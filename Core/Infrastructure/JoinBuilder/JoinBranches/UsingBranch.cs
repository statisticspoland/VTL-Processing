namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.JoinBranches
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.JoinBranches.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using System.Linq;

    /// <summary>
    /// The "using" branch of the "join" operator expression builded from a dataset expression.
    /// </summary>
    public sealed class UsingBranch : IJoinBranch
    {
        private readonly IExpressionFactory exprFactory;

        /// <summary>
        /// Inittializes a new instance of the <see cref="UsingBranch"/> class.
        /// </summary>
        /// <param name="exprFactory">The expression factory.</param>
        public UsingBranch(IExpressionFactory exprFactory)
        {
            this.exprFactory = exprFactory;
        }

        public string Signature => "using";

        public IExpression Build(IExpression datasetExpr)
        {
            IExpression usingBranch = this.exprFactory.GetExpression("Using", ExpressionFactoryNameTarget.ResultName);
            usingBranch.ParamSignature = this.Signature;
            usingBranch.ExpressionText = "using ";

            foreach (StructureComponent identifier in (datasetExpr as IJoinExpression).GetSubsetAliasStructure().Identifiers)
            {
                usingBranch.AddOperand(identifier.ComponentName, this.exprFactory.GetExpression("Identifier", ExpressionFactoryNameTarget.ResultName)); // assignment of all common identifiers to "using" branch
                usingBranch.OperandsCollection.Last().ExpressionText = identifier.ComponentName;
                usingBranch.ExpressionText += $"{identifier.ComponentName}, ";
            }

            usingBranch.ExpressionText = usingBranch.ExpressionText.Remove(usingBranch.ExpressionText.Length - 2); // removement of ", "

            return usingBranch;
        }
    }
}
