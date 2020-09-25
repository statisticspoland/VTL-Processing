using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;

namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.JoinBranches.Interfaces
{
    /// <summary>
    /// Interface of a branch of the "join" operator expression builded from a dataset expression.
    /// </summary>
    public interface IJoinBranch
    {
        /// <summary>
        /// Gets the signature of the branch.
        /// </summary>
        string Signature { get; }

        /// <summary>
        /// Builds the branch.
        /// </summary>
        /// <param name="datasetExpr">The dataset expression the "join" operator expression has to be built from.</param>
        /// <returns>The built branch expression.</returns>
        IExpression Build(IExpression datasetExpr);
    }
}
