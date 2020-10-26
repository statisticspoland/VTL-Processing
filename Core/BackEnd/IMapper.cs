namespace StatisticsPoland.VtlProcessing.Core.BackEnd
{
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;

    /// <summary>
    /// Interface of an mapper of VTL 2.0 to target names.
    /// </summary>
    public interface IMapper
    {
        /// <summary>
        /// Maps names of objects in a transformation schema.
        /// </summary>
        /// <param name="schema">The transformation schema.</param>
        void MapNames(ITransformationSchema schema);
    }
}
