using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;

namespace StatisticsPoland.VtlProcessing.Core.BackEnd
{
    /// <summary>
    /// Target renderer for the VTL 2.0 translation interface.
    /// </summary>
    public interface ITargetRenderer
    {
        /// <summary>
        /// Gets the name of the target renderer.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Renders a translated code for the entire transformation schema.
        /// </summary>
        /// <param name="schema">The transformation schema.</param>
        /// <returns>The translated code.</returns>
        string Render(ITransformationSchema schema);

        /// <summary>
        /// Renders a translated code for the single expression of the transformation schema.
        /// </summary>
        /// <param name="expression">The expression to render the translated code from.</param>
        /// <returns>The translated code.</returns>
        string Render(IExpression expression);
    }
}
