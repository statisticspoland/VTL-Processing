namespace StatisticsPoland.VtlProcessing.Core.BackEnd
{
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;

    /// <summary>
    /// Interface of a target renderer for the VTL 2.0 translation.
    /// </summary>
    public interface ITargetRenderer
    {
        /// <summary>
        /// Gets the name of the target renderer.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Renders a TSQL translated code for the entire transformation schema.
        /// </summary>
        /// <param name="schema">The transformation schema.</param>
        /// <returns>The TSQL translated code.</returns>
        string Render(ITransformationSchema schema);

        /// <summary>
        /// Renders a TSQL translated code for a single expression of a transformation schema.
        /// </summary>
        /// <param name="expression">The expression to render the TSQL translated code from.</param>
        /// <returns>The TSQL translated code.</returns>
        string Render(IExpression expression);
    }
}
