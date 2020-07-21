namespace StatisticsPoland.VtlProcessing.Core.Models.Types
{
    /// <summary>
    /// VTL 2.0 datasets components types.
    /// </summary>
    public enum ComponentType
    {
        /// <summary>
        /// The identifier component.
        /// </summary>
        Identifier = 1,

        /// <summary>
        /// The measure component.
        /// </summary>
        Measure = 2,

        /// <summary>
        /// The viral attribute component.
        /// </summary>
        ViralAttribute = 4,

        /// <summary>
        /// The non-viral attribute component.
        /// </summary>
        NonViralAttribute = 8
    }
}
