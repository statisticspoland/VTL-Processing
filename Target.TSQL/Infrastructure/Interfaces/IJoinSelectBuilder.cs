namespace Target.TSQL.Infrastructure.Interfaces
{
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;

    /// <summary>
    /// The TSQL join select query builder interface.
    /// </summary>
    internal interface IJoinSelectBuilder
    {
        /// <summary>
        /// Builds a TSQL join select query.
        /// </summary>
        /// <returns>The TSQL Select query.</returns>
        string Build();

        /// <summary>
        /// Adds a "join" expression to the builder.
        /// </summary>
        /// <param name="joinExpr">The "join" expression.</param>
        /// <returns>Instance of the builder.</returns>
        IJoinSelectBuilder AddJoinExpr(IJoinExpression joinExpr);

        /// <summary>
        /// Adds identifiers to the builder.
        /// </summary>
        /// <returns>Instance of the builder.</returns>
        IJoinSelectBuilder AddIdentifiers();

        /// <summary>
        /// Adds measures to the builder.
        /// </summary>
        /// <param name="applyMeasuresRenderer">The measures renderer for expresions with an "apply" branch.</param>
        /// <returns>Instance of the builder.</returns>
        IJoinSelectBuilder AddMeasures(JoinMeasuresRenderer applyMeasuresRenderer);

        /// <summary>
        /// Adds viral attributes to the builder.
        /// </summary>
        /// <returns>Instance of the builder.</returns>
        IJoinSelectBuilder AddViralAttributes();

        /// <summary>
        /// Adds data sources to the builder.
        /// </summary>
        /// <returns>Instance of the builder.</returns>
        IJoinSelectBuilder AddSource();

        /// <summary>
        /// Adds "join" operator's filters to the builder.
        /// </summary>
        /// <returns>Instance of the builder.</returns>
        IJoinSelectBuilder AddFilters();

        /// <summary>
        /// Adds a "join" operator's grouping clause to the builder.
        /// </summary>
        /// <returns>Instance of the builder.</returns>
        IJoinSelectBuilder AddGroupingClause();

        /// <summary>
        /// Adds a "join" operator's having clause to the builder.
        /// </summary>
        /// <returns>Instance of the builder.</returns>
        IJoinSelectBuilder AddHavingClause();
    }
}
