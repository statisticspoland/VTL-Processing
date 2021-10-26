namespace StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure.Interfaces
{
    /// <summary>
    /// The builder interface for the TSQL target renderer.
    /// </summary>
    public interface ITargetBuilder
    {
        /// <summary>
        /// Adds comments to the translated TSQL code.
        /// </summary>
        /// <returns>Instance of the builder.</returns>A
        ITargetBuilder AddComments();

        /// <summary>
        /// Sets an attribute propagation algorithm used by the target.
        /// </summary>
        /// <param name="propagationAlgorithm">The attribute propagation algorithm.</param>
        /// <returns>Instance of the builder.</returns>
        ITargetBuilder SetAttributePropagationAlgorithm(IAttributePropagationAlgorithm propagationAlgorithm);
    }
}
