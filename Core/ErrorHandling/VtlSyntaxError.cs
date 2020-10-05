namespace StatisticsPoland.VtlProcessing.Core.ErrorHandling
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using System;

    /// <summary>
    /// The VTL 2.0 syntax error representation.
    /// </summary>
    public class VtlSyntaxError : ApplicationException, IVtlError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VtlSyntaxError"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="line">The line of the error.</param>
        public VtlSyntaxError(string message, int line)
            : base(message)
        {
            this.Line = line;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VtlSyntaxError"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="line">The line of the error.</param>
        /// <param name="innerException">The exception that is the cause of the current error.</param>
        public VtlSyntaxError(string message, int line, Exception innerException) : base(message, innerException)
        {
            this.Line = line;
        }

        public int Line { get; private set; }

        public string FullMessage => this.ToString();

        public ErrorType TypeError => ErrorType.Syntax;

        public override string ToString()
        {
            string result = $"Syntax error at line {this.Line} | {base.Message}";

            #if DEBUG
                result = $"{this.StackTrace}\n{result}";
            #endif

            return result;
        }
    }
}