namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.Interfaces
{
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System.Collections.Generic;

    /// <summary>
    /// The "join" operator expressions builder interface.
    /// </summary>
    public interface IJoinBuilder
    {
        /// <summary>
        /// Adds a main expression to the builder.
        /// </summary>
        /// <param name="mainExpr">The expression to add.</param>
        /// <returns>Instance of the builder.</returns>
        IJoinBuilder AddMainExpr(IExpression mainExpr);

        /// <summary>
        /// Adds a branch to the builder.
        /// </summary>
        /// <param name="key">The branch name.</param>
        /// <param name="branch">The expression to add.</param>
        /// <returns>Instance of the builder.</returns>
        IJoinBuilder AddBranch(string key, IExpression branch);

        /// <summary>
        /// Clears a data of the builder.
        /// </summary>
        IJoinBuilder Clear();

        /// <summary>
        /// Gets a value specifying if the builder is cleared.
        /// </summary>
        bool IsCleared { get; }

        /// <summary>
        /// Gets the builder's branches dictionary.
        /// </summary>
        Dictionary<string, IExpression> Branches { get; }

        /// <summary>
        /// Gets an expression from the builder's expressions dictionary.
        /// </summary>
        /// <param name="key">The key of expression.</param>
        /// <returns>The chosen expression.</returns>
        IExpression this[string key] { get; }

        /// <summary>
        /// Builds an join expression from the builder expressions.
        /// </summary>
        /// <returns>The join expression.</returns>
        IJoinExpression Build();
    }
}
