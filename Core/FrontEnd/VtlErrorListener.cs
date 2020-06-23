using Antlr4.Runtime;
using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
using System.Collections.Generic;
using System.IO;

namespace StatisticsPoland.VtlProcessing.Core.FrontEnd
{
    public class VtlErrorListener : IAntlrErrorListener<IToken>
    {
        private List<VtlParseError> errors;

        public VtlErrorListener()
        {
            this.errors = new List<VtlParseError>();
        }

        public IEnumerable<VtlParseError> Errors
        {
            get
            {
                return this.errors;
            }
        }

        public void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            this.errors.Add(new VtlParseError(msg, line));
        }
    }
}