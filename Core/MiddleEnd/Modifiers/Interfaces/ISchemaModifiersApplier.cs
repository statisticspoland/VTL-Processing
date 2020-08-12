namespace StatisticsPoland.VtlProcessing.Core.MiddleEnd.Modifiers.Interfaces
{
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;

    /// <summary>
    /// Apllier of a complex transformation of a VTL 2.0 transformation schema interface.
    /// </summary>
    public interface ISchemaModifiersApplier
    {
        /// <summary>
        /// Performs processing of a schema.
        /// </summary>
        /// <param name="schema">The schema object to be processed.</param>
        void Process(ITransformationSchema schema);
    }
}
