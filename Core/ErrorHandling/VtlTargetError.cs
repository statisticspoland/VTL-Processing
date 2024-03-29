﻿namespace StatisticsPoland.VtlProcessing.Core.ErrorHandling
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using System;

    /// <summary>
    /// The VTL 2.0 translation target representation.
    /// </summary>
    [Serializable]
    public class VtlTargetError : ApplicationException, IVtlError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VtlTargetError"/> class.
        /// </summary>
        /// <param name="expression">The expression an error is from.</param>
        /// <param name="message">The error message.</param>
        public VtlTargetError(IExpression expression, string message) : base(message)
        {
            if (expression != null) this.Line = expression.LineNumber;
            else this.Line = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VtlTargetError"/> class.
        /// </summary>
        /// <param name="expression">The expression an error is from.</param>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public VtlTargetError(IExpression expression, string message, Exception innerException) : base(message, innerException)
        {
            if (expression != null) this.Line = expression.LineNumber;
            else this.Line = 0;
        }

        public int Line { get; private set; }

        public string FullMessage => this.ToString();

        public ErrorType TypeError => ErrorType.Target;

        public override string ToString()
        {
            string result = $"Target error at line {this.Line} | {base.Message}";

            #if DEBUG
                result = $"{this.StackTrace}\n{result}";
            #endif

            return result;
        }
    }
}
