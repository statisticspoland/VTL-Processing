namespace StatisticsPoland.VtlProcessing.Core.ErrorHandling
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System;

    public class VtlError : ApplicationException, IVtlError
    {
        public VtlError(IExpression expression, string message) : base(message)
        {
        }

        public VtlError(IExpression expression, string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Gets the line number of error.
        /// </summary>
        /// <value>
        /// The line number of error.
        /// </value>
        public int Line { get; private set; }

        /// <summary>
        /// Gets the message of error.
        /// </summary>
        /// <value>
        /// The message of error.
        /// </value>
        public string FullMessage => base.ToString();

        /// <summary>
        /// Gets the type of error.
        /// </summary>
        /// <value>
        /// The type of error.
        /// </value>
        public ErrorType TypeError => ErrorType.Common;
    }
}
