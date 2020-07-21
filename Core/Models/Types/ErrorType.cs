namespace StatisticsPoland.VtlProcessing.Core.Models.Types
{
    /// <summary>
    /// VTL 2.0 error types.
    /// </summary>
    public enum ErrorType
    {
        /// <summary>
        /// The VTL common error.
        /// </summary>
        Common = 1,

        /// <summary>
        /// The VTL syntax error.
        /// </summary>
        Syntax = 2,

        /// <summary>
        /// The VTL operator error.
        /// </summary>
        Operator = 3,

        /// <summary>
        /// The VTL target error.
        /// </summary>
        Target = 4
    }
}
