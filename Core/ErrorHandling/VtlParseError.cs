namespace StatisticsPoland.VtlProcessing.Core.ErrorHandling
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using System;

    /// <summary>
    /// Class of VTL expression interpretation error.
    /// </summary>
    public class VtlParseError : ApplicationException, IVtlError
    {
        public VtlParseError(string message, int line)
            : base(message)
        {
            this.Line = line;
        }

        public VtlParseError(string message, Exception innerException) : base(message, innerException)
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
        public string FullMessage => this.ToString();

        /// <summary>
        /// Gets the type of error.
        /// </summary>
        /// <value>
        /// The type of error.
        /// </value>
        public ErrorType TypeError => ErrorType.Syntax;

        /// <summary>
        /// Creates and returns a string representation of the current exception.
        /// </summary>
        /// <returns>
        /// A string representation of the current exception.
        /// </returns>
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