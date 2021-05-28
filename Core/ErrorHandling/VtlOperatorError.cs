namespace StatisticsPoland.VtlProcessing.Core.ErrorHandling
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using System;

    /// <summary>
    /// The VTL 2.0 operator error representation.
    /// </summary>
    [Serializable]
    public class VtlOperatorError : ApplicationException, IVtlError
    {
        private readonly string msg;
        private readonly string opName;

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
        /// <param name="innerException">The exception that is the cause of the current error.</param>
        public VtlOperatorError(IExpression expression, string @operator, string message, Exception innerException) : base(message, innerException)
        {
            this.Line = expression.LineNumber;
            this.msg = message;
            this.opName = @operator;
        }

        public int Line { get; private set; }

        public string FullMessage => this.ToString();

        public ErrorType TypeError => ErrorType.Operator;

        public override string ToString()
        {
            string result = $"{this.opName} operator error at line {this.Line} | {this.msg}";

            #if DEBUG
                result = $"{this.StackTrace}\n{result}";
            #endif  

            return result;
        }
    }
}
