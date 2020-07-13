namespace StatisticsPoland.VtlProcessing.Core.Transformations
{
    using Antlr4.Runtime.Tree;
    using Microsoft.Extensions.Logging;
    using StatisticsPoland.VtlProcessing.Core.FrontEnd.Antlr;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Transformations.Interfaces;
    using System;

    /// <summary>
    /// Class that transforms the CST into a transformation schema structure using an antler visitor.
    /// </summary>
    public sealed class VisitorTransformer : VtlBaseVisitor<IExpression>, ITreeTransformer
    {
        private readonly ILogger<VisitorTransformer> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="VisitorTransformer"/> class.
        /// </summary>
        /// <param name="logger">Errors logger.</param>
        public VisitorTransformer(ILogger<VisitorTransformer> logger = null)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Transform a CST tree into a transformation schema structure.
        /// </summary>
        /// <param name="tree">The input tree.</param>
        /// <returns>Transformation schema.</returns>
        public ITransformationSchema TransformToSchema(IParseTree tree)
        {
            try
            {
                this.Visit(tree);
            }
            catch (Exception ex)
            {
                this.logger?.LogCritical(ex, ex.Message);
            }

            return new TransformationSchema();
        }

        /// <summary>
        /// A node to walk to a tree. Returns a temporary expression object whose operands are the correct VTL expressions.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Temporary expression object whose operands are the correct VTL expressions.</returns>
        public override IExpression VisitStart(VtlParser.StartContext context)
        {
            throw new NotImplementedException();
        }
    }
}
