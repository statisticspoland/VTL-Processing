﻿namespace StatisticsPoland.VtlProcessing.Core.FrontEnd
{
    using Antlr4.Runtime;
    using Microsoft.Extensions.Logging;
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.FrontEnd.Antlr;
    using StatisticsPoland.VtlProcessing.Core.FrontEnd.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Transformations.Interfaces;

    /// <summary>
    /// The VTL 2.0 syntax tree generator
    /// </summary>
    public sealed class TreeGenerator : ITreeGenerator
    {
        private readonly ITreeTransformer transformer;
        private readonly ILogger<TreeGenerator> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeGenerator"/> class.
        /// </summary>
        /// <param name="transformer">The transformer of CST tree to a transformation schema.</param>
        /// <param name="logger">The syntax errors logger.</param>
        public TreeGenerator(ITreeTransformer transformer, ILogger<TreeGenerator> logger = null)
        {
            this.logger = logger;
            this.transformer = transformer;
        }

        public ITransformationSchema BuildTransformationSchema(string vtlSource)
        {
            AntlrInputStream input = new AntlrInputStream(vtlSource);
            VtlLexer lexer = new VtlLexer(input);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            VtlParser parser = new VtlParser(tokens);

            VtlSyntaxErrorListener errListener = new VtlSyntaxErrorListener();

            parser.RemoveErrorListeners();
            parser.AddErrorListener(errListener);

            VtlParser.StartContext cst = parser.start();
            foreach (VtlSyntaxError error in errListener.Errors)
            {
                this.logger?.LogError(error, error.Message);
            }

            return this.transformer.TransformToSchema(cst);
        }        
    }
}
