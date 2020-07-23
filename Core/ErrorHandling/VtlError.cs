namespace StatisticsPoland.VtlProcessing.Core.ErrorHandling
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using System;

    /// <summary>
    /// The VTL 2.0 error representation.
    /// </summary>
    public class VtlError : ApplicationException, IVtlError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VtlError"/> class.
        /// </summary>
        /// <param name="expression">The expression an error is from.</param>
        /// <param name="message">The error message.</param>
        public VtlError(IExpression expression, string message) : base(message)
        {
            this.Line = expression.LineNumber;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VtlError"/> class.
        /// </summary>
        /// <param name="expression">The expression an error is from.</param>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public VtlError(IExpression expression, string message, Exception innerException) : base(message, innerException)
        {
            this.Line = expression.LineNumber;
        }

        public int Line { get; private set; }

        public string FullMessage => base.ToString();

        public ErrorType TypeError => ErrorType.Common;
    }
}
