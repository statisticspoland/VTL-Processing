namespace StatisticsPoland.VtlProcessing.Core.Models
{
    using System;

    /// <summary>
    /// VTL error types.
    /// </summary>
    [Flags]
    public enum ErrorType
    {
        /// <summary>
        /// VTL Common error.
        /// </summary>
        Common = 1,

        /// <summary>
        /// VTL Syntax error.
        /// </summary>
        Syntax = 2,

        /// <summary>
        /// VTL Operator error.
        /// </summary>
        Operator = 4,

        /// <summary>
        /// VTL Target error.
        /// </summary>
        Target = 8,
    }
}
