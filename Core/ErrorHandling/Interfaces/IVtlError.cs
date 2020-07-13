namespace StatisticsPoland.VtlProcessing.Core.ErrorHandling.Interfaces
{
    using StatisticsPoland.VtlProcessing.Core.Models;

    public interface IVtlError
    {
        /// <summary>
        /// Gets the line number of error.
        /// </summary>
        /// <value>
        /// The line number of error.
        /// </value>
        int Line { get; }

        /// <summary>
        /// Gets the message of error.
        /// </summary>
        /// <value>
        /// The message of error.
        /// </value>
        string FullMessage { get; }

        /// <summary>
        /// Gets the type of error.
        /// </summary>
        /// <value>
        /// The type of error.
        /// </value>
        ErrorType TypeError { get; }
    }
}
