namespace Target.TSQL.Preparers.Interfaces
{
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;

    /// <summary>
    /// The references manager interface.
    /// </summary>
    public interface IReferencesManager
    {
        /// <summary>
        /// Assigns non-persistent assignment expressions from a schema to its dictionary. <br />
        /// Gives out a collection of persistent assignemnt expressions.
        /// </summary>
        /// <param name="schema">The transformation schema.</param>
        public void TakeNonPersistentExprs(ITransformationSchema schema);

        /// <summary>
        /// Renders a TSQL translated code for its non-persistent assignment expression.
        /// </summary>
        /// <param name="expr">The expression.</param>
        /// <returns>The TSQL translated code.</returns>
        public string RenderNonPersistentExpr(IExpression expr);

        /// <summary>
        /// Renders a TSQL translated code for a dropping of tables of its non-persistent assignment expressions.
        /// </summary>
        /// <returns>The TSQL translated code.</returns>
        public string RenderDroppingOfTables();

        /// <summary>
        /// Checks if an expression exists in references.
        /// </summary>
        /// <param name="expr">The expression.</param>
        /// <returns>Value determining whether the expression exists in references.</returns>
        public bool ContainsExpression(IExpression expr);
    }
}
