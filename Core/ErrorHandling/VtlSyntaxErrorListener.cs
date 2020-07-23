namespace StatisticsPoland.VtlProcessing.Core.ErrorHandling
{
    using Antlr4.Runtime;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// The VTL 2.0 syntax error listener.
    /// </summary>
    public class VtlSyntaxErrorListener : IAntlrErrorListener<IToken>
    {
        private List<VtlSyntaxError> errors;

        /// <summary>
        /// Initializes a new instance of the <see cref="VtlSyntaxErrorListener"/> class.
        /// </summary>
        public VtlSyntaxErrorListener()
        {
            this.errors = new List<VtlSyntaxError>();
        }

        /// <summary>
        /// Gets the errors enumerator.
        /// </summary>
        public IEnumerable<VtlSyntaxError> Errors => this.errors;

        public void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            this.errors.Add(new VtlSyntaxError(msg, line));
        }
    }
}