namespace StatisticsPoland.VtlProcessing.Core.Transformations.Interfaces
{
    using Antlr4.Runtime.Tree;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;

    /// <summary>
    /// Interface transforming a CST tree into a transformation schema structure.
    /// </summary>
    public interface ITreeTransformer
    {
        /// <summary>
        /// Transform a CST tree into a transformation schema structure.
        /// </summary>
        /// <param name="tree">The input tree.</param>
        /// <returns>The transformation schema.</returns>
        ITransformationSchema TransformToSchema(IParseTree tree);
    }
}