[assembly: System.CLSCompliant(false)]

namespace StatisticsPoland.VtlProcessing.Core.FrontEnd
{
    using Antlr4.Runtime;
    using Microsoft.Extensions.Logging;
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.FrontEnd.Antlr;
    using StatisticsPoland.VtlProcessing.Core.FrontEnd.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Transformations.Interfaces;

    /// <summary>
    /// Front-end class for the VTL 2.0 version.
    /// </summary>
    public sealed class TreeGenerator : ITreeGenerator
    {
        private readonly ITreeTransformer transformer;
        private readonly ILogger<TreeGenerator> logger;

        public TreeGenerator(ITreeTransformer transformer, ILogger<TreeGenerator> logger = null)
        {
            this.logger = logger;
            this.transformer = transformer;
        }

        /// <summary>
        /// Gets the version of VTL syntax
        /// </summary>
        /// <value>
        /// Version of VTL syntax
        /// </value>
        public string SyntaxVersion
        {
            get
            {
                return "2.0";
            }
        }

        /// <summary>
        /// Builds a new TransformationSchema object from the source code.
        /// </summary>
        /// <param name="vtlSource">VTL source code.</param>
        /// <returns>
        /// Intermediate representation object of TransformationSchema.
        /// </returns>
        public ITransformationSchema BuildTransformationSchema(string vtlSource)
        {
            var input = new AntlrInputStream(vtlSource);
            var lexer = new VtlLexer(input);
            var tokens = new CommonTokenStream(lexer);
            var parser = new VtlParser(tokens);

            var errListener = new VtlErrorListener();

            parser.RemoveErrorListeners();
            parser.AddErrorListener(errListener);

            VtlParser.StartContext cst = parser.start();
            foreach (VtlParseError error in errListener.Errors)
            {
                this.logger?.LogCritical(error, error.Message);
            }

            return this.transformer.TransformToSchema(cst);
        }
    }
}
