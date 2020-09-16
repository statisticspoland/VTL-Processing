﻿namespace StatisticsPoland.VtlProcessing.Core.ErrorHandling
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using System;

    /// <summary>
    /// The VTL 2.0 operator error representation.
    /// </summary>
    public class VtlOperatorError : ApplicationException, IVtlError
    {
        private string msg;
        private string opName;

        /// <summary>
        /// Initializes a new instance of the <see cref="VtlOperatorError"/> class.
        /// </summary>
        /// <param name="expression">The expression an error is from.</param>
        /// <param name="operator">The operator an error occured in.</param>
        /// <param name="message">The error message.</param>
        public VtlOperatorError(IExpression expression, string @operator, string message) : base(message)
        {
            this.Line = expression.LineNumber;
            this.msg = message;
            this.opName = @operator;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VtlOperatorError"/> class.
        /// </summary>
        /// <param name="expression">The expression an error is from.</param>
        /// <param name="operator">The operator an error occured in.</param>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public VtlOperatorError(IExpression expression, string @operator, string message, Exception innerException) : base(message, innerException)
        {
            this.Line = expression.LineNumber;
            this.msg = message;
            this.opName = @operator;
        }

        /// <summary>
        /// Checks an errors number of an expression and throws exception if number is greater than 0.
        /// </summary>
        /// <param name="errorsNumber">The errors number.</param>
        /// <param name="expression">The expression errors are from.</param>
        /// <param name="operator">The operator errors occured in.</param>
        public static void ProcessAttributeErrors(int errorsNumber, IExpression expression, string @operator)
        {
            if (errorsNumber > 0) throw new VtlOperatorError(expression, @operator, "Viral attributes don't match.");
        }

        public int Line { get; private set; }

        public string FullMessage => this.ToString();

        public ErrorType TypeError => ErrorType.Operator;

        public override string ToString()
        {
            string result = $"{opName} operator error at line {this.Line} | {this.msg}";

            #if DEBUG
                result = $"{this.StackTrace}\n{result}";
            #endif  

            return result;
        }
    }
}