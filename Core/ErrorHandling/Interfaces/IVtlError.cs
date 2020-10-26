namespace StatisticsPoland.VtlProcessing.Core.ErrorHandling.Interfaces
{
    using StatisticsPoland.VtlProcessing.Core.Models.Types;

    /// <summary>
    /// Interface of a VTL 2.0 error representation.
    /// </summary>
    public interface IVtlError
    {
        /// <summary>
        /// Gets the line number of the error.
        /// </summary>
        int Line { get; }

        /// <summary>
        /// Gets the message of the error.
        /// </summary>
        string FullMessage { get; }

        /// <summary>
        /// Gets the type of the error.
        /// </summary>
        ErrorType TypeError { get; }
    }
}
