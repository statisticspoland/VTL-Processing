namespace StatisticsPoland.VtlProcessing.Core.MiddleEnd.Modifiers.Interfaces
{
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;

    /// <summary>
    /// The VTL 2.0 transformation schema modifier interfafce.
    /// </summary>
    public interface ISchemaModifier
    {
        /// <summary>
        /// Performs a modification for a schema.
        /// </summary>
        /// <param name="schema">The schema to modify.</param>
        void Modify(ITransformationSchema schema);
    }
}