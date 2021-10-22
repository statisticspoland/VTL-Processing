namespace StatisticsPoland.VtlProcessing.Core.FrontEnd.Interfaces
{
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;

    /// <summary>
    /// The VTL 2.0 syntax tree generator interface.
    /// </summary>
    public interface ITreeGenerator
    {
        /// <summary>
        /// Builds a transformation schema based on VTL 2.0 source code.
        /// </summary>
        /// <param name="vtlSource">The VTL 2.0 source code.</param>
        /// <returns>The transformation schema.</returns>
        ITransformationSchema BuildTransformationSchema(string vtlSource);
    }
}