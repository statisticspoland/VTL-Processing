namespace StatisticsPoland.VtlProcessing.Core.Transformations
{
    using Antlr4.Runtime;
    using Antlr4.Runtime.Misc;
    using Antlr4.Runtime.Tree;
    using Microsoft.Extensions.Logging;
    using StatisticsPoland.VtlProcessing.Core.FrontEnd.Antlr;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Transformations.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Transforms the CST tree into a transformation schema structure using an ANTRL4 visitor.
    /// </summary>
    public sealed class VisitorTransformer : VtlBaseVisitor<IExpression>, ITreeTransformer
    {
        private readonly TransformationSchemaResolver schemaResolver;
        private readonly IExpressionFactory exprFactory;
        private readonly ILogger<VisitorTransformer> logger;
        private List<string> currentRefs;
        private ITransformationSchema schema;

        /// <summary>
        /// Initializes a new instance of the <see cref="VisitorTransformer"/> class.
        /// </summary>
        /// <param name="schemaResolver">The transformation schema resolver.</param>
        /// <param name="exprFactory">The expressions factory.</param>
        /// <param name="logger">The errors logger.</param>
        public VisitorTransformer(TransformationSchemaResolver schemaResolver, IExpressionFactory exprFactory, ILogger<VisitorTransformer> logger = null)
        {
            this.schemaResolver = schemaResolver;
            this.exprFactory = exprFactory;
            this.logger = logger;
        }

        public ITransformationSchema TransformToSchema(IParseTree tree)
        {
            this.schema = this.schemaResolver();

            try
            {
                this.Visit(tree);
            }
            catch (Exception ex)
            {
                this.logger?.LogError(ex, ex.Message);
            }

            return this.schema;
        }

        /// <summary>
        /// A node to walk to a tree. Returns a temporary expression object whose operands are the correct VTL 2.0 expressions.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The temporary expression object whose operands are the correct VTL 2.0 expressions.</returns>
        public override IExpression VisitStart(VtlParser.StartContext context)
        {
            IExpression startExpr = this.exprFactory.ExprResolver();
            startExpr.OperandsCollection = context.statement().Select(op => this.Visit(op)).ToList();
            return startExpr;
        }

        /// <summary>
        /// Represents a single VTL 2.0 expression. Returns the expression of the assignment. The variable name is the name of the result of the expression.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The VTL 2.0 expression.</returns>
        public override IExpression VisitStatement([NotNull] VtlParser.StatementContext context)
        {
            this.currentRefs = new List<string>();

            IExpression statementExpr;
            if (context.dataset() != null) statementExpr = this.Visit(context.dataset());
            else statementExpr = this.Visit(context.scalar());
            statementExpr.ResultName = context.varID().GetText();
            statementExpr.ParamSignature = "<root>";
            statementExpr.LineNumber = context.Start.Line;
            statementExpr.SetContainingSchema(this.schema);

            while (statementExpr.ExpressionText[0] == ' ')
            {
                statementExpr.ExpressionText = statementExpr.ExpressionText.Remove(0, 1);
            }
            
            this.schema.AssignmentObjects.Add(new AssignmentObject(this.schema, statementExpr, context.PUT_SYMBOL() != null, this.currentRefs));
            return statementExpr;
        }

        public override IExpression VisitOpenedDataset([NotNull] VtlParser.OpenedDatasetContext context)
        {
            IExpression datasetExpr;
            if (context.opSymbol == null) return this.Visit(context.ifThenElseDataset());
            datasetExpr = this.exprFactory.GetExpression(context.opSymbol.Text, ExpressionFactoryNameTarget.OperatorSymbol);

            if (context.unopenedDataset() != null) datasetExpr.AddOperand("ds_1", this.Visit(context.unopenedDataset()));
            else if (context.openedDatasetLeft != null) datasetExpr.AddOperand("ds_1", this.Visit(context.openedDatasetLeft));
            else if (context.constant() != null) datasetExpr.AddOperand("ds_1", this.Visit(context.constant()));
            else if (context.scalar() != null) datasetExpr.AddOperand("ds_1", this.Visit(context.scalar()));
            else // opSymbol = "not"
            {
                datasetExpr.ExpressionText = this.GetOriginalText(context);
                datasetExpr.LineNumber = context.Start.Line;
                datasetExpr.AddOperand("ds_1", this.Visit(context.dataset()));
                return datasetExpr;
            }

            VtlParser.OpenedDatasetContext openedDataset = context.openedDataset()?.FirstOrDefault(ds => ds != context.openedDatasetLeft);

            if (openedDataset != null) datasetExpr.AddOperand("ds_2", this.Visit(openedDataset));
            else if (context.closedDataset() != null) datasetExpr.AddOperand("ds_2", this.Visit(context.closedDataset()));
            else if (context.membershipDataset() != null) datasetExpr.AddOperand("ds_2", this.Visit(context.membershipDataset()));
            else if (context.constant() != null) datasetExpr.AddOperand("ds_2", this.Visit(context.constant()));
            else if (context.scalar() != null) datasetExpr.AddOperand("ds_2", this.Visit(context.scalar()));
            else if (context.list() != null) datasetExpr.AddOperand("ds_2", this.Visit(context.list()));
            else datasetExpr.AddOperand("ds_2", this.Visit(context.valueDomainName()));

            datasetExpr.ExpressionText = this.GetOriginalText(context);
            datasetExpr.LineNumber = context.Start.Line;
            return datasetExpr;
        }

        public override IExpression VisitClosedDataset([NotNull] VtlParser.ClosedDatasetContext context)
        {
            IExpression datasetExpr = null;
            if (context.opSymbol == null)
            {
                if (context.datasetID() != null) return this.Visit(context.datasetID());
                if (context.datasetComplex() != null) return this.Visit(context.datasetComplex());
                if (context.openedDataset() != null) return this.Visit(context.openedDataset());
                if (context.membershipDataset() != null) return this.Visit(context.membershipDataset());
                if (context.closedDataset() != null)
                {
                    if (context.datasetClause() == null) return this.Visit(context.closedDataset());
                    datasetExpr = this.exprFactory.GetExpression("datasetClause", ExpressionFactoryNameTarget.OperatorSymbol);
                    datasetExpr.AddOperand("ds_1", this.Visit(context.closedDataset()));
                    datasetExpr.AddOperand("ds_2", this.Visit(context.datasetClause()));
                }
            }
            else
            {
                string opSymbol = context.opSymbol.Text;
                if (opSymbol == "+") return this.Visit(context.dataset()[0]);
                if (opSymbol == "-") opSymbol = "minus";

                datasetExpr = this.exprFactory.GetExpression(opSymbol, ExpressionFactoryNameTarget.OperatorSymbol);

                int index = 1;
                foreach (VtlParser.DatasetContext datasetContext in context.dataset())
                {
                    datasetExpr.AddOperand($"ds_{index++}", this.Visit(datasetContext));
                }

                foreach (VtlParser.ScalarContext scalarContext in context.scalar())
                {
                    datasetExpr.AddOperand($"ds_{index++}", this.Visit(scalarContext));
                }

                foreach (VtlParser.OptionalExprContext optionalExprContext in context.optionalExpr())
                {
                    datasetExpr.AddOperand($"ds_{index++}", this.Visit(optionalExprContext));
                }

                if (context.retainType() != null) datasetExpr.OperatorDefinition.Keyword = context.retainType().GetText();
                else if (context.limitsMethod() != null) datasetExpr.OperatorDefinition.Keyword =  context.limitsMethod().GetText();
            }

            datasetExpr.ExpressionText = this.GetOriginalText(context);
            datasetExpr.LineNumber = context.Start.Line;
            return datasetExpr;
        }

        public override IExpression VisitMembershipDataset([NotNull] VtlParser.MembershipDatasetContext context)
        {
            IExpression membershipExpr = this.exprFactory.GetExpression("#", ExpressionFactoryNameTarget.OperatorSymbol);
            membershipExpr.ExpressionText = this.GetOriginalText(context);
            membershipExpr.LineNumber = context.Start.Line;
            membershipExpr.AddOperand("ds_1", this.Visit(context.closedDataset()));
            membershipExpr.AddOperand("ds_2", this.Visit(context.componentID()));

            return membershipExpr;
        }

        public override IExpression VisitComponent([NotNull] VtlParser.ComponentContext context)
        {
            IExpression componentExpr;
            if (context.MEMBERSHIP() != null)
            {
                componentExpr = this.exprFactory.GetExpression("#", ExpressionFactoryNameTarget.OperatorSymbol);
                componentExpr.AddOperand("ds_1", this.Visit(context.closedDataset()));
                componentExpr.AddOperand("ds_2", this.Visit(context.componentID()));
            }
            else componentExpr = this.Visit(context.componentID());

            componentExpr.ExpressionText = this.GetOriginalText(context);
            componentExpr.LineNumber = context.Start.Line;
            //componentExpr.OperatorDefinition.Keyword = "Component";
            return componentExpr;
        }

        public override IExpression VisitScalar([NotNull] VtlParser.ScalarContext context)
        {
            IExpression scalarExpr;
            if (context.component() != null)
            {
                if (context.opSymbol?.Text == "-")
                {
                    scalarExpr = this.exprFactory.GetExpression("minus", ExpressionFactoryNameTarget.OperatorSymbol);
                    scalarExpr.AddOperand("ds_1", this.Visit(context.component()));
                    return scalarExpr;
                }

                return this.Visit(context.component());
            }

            if (context.constant() != null) return this.Visit(context.constant());
            if (context.ifThenElseScalar() != null) return this.Visit(context.ifThenElseScalar());
            if (context.opSymbol == null) return this.Visit(context.scalar()[0]);
            scalarExpr = this.exprFactory.GetExpression(context.opSymbol.Text, ExpressionFactoryNameTarget.OperatorSymbol);

            int index = 1;
            foreach (VtlParser.ScalarContext scalarContext in context.scalar())
            {
                scalarExpr.AddOperand($"ds_{index++}", this.Visit(scalarContext));
            }

            foreach (VtlParser.OptionalExprContext optionalExprContext in context.optionalExpr())
            {
                scalarExpr.AddOperand($"ds_{index++}", this.Visit(optionalExprContext));
            }

            if (context.list() != null) scalarExpr.AddOperand($"ds_{index}", this.Visit(context.list()));
            else if (context.valueDomainName() != null) scalarExpr.AddOperand($"ds_{index}", this.Visit(context.valueDomainName()));

            scalarExpr.ExpressionText = this.GetOriginalText(context);
            scalarExpr.LineNumber = context.Start.Line;
            return scalarExpr;
        }

        public override IExpression VisitOptionalExpr([NotNull] VtlParser.OptionalExprContext context)
        {
            IExpression optionalExpr;
            if (context.scalar() != null) return this.Visit(context.scalar());
            optionalExpr = this.exprFactory.GetExpression("optional", ExpressionFactoryNameTarget.OperatorSymbol);
            
            optionalExpr.ExpressionText = this.GetOriginalText(context);
            optionalExpr.LineNumber = context.Start.Line;
            return optionalExpr;
        }

        public override IExpression VisitDatasetID([NotNull] VtlParser.DatasetIDContext context)
        {
            IExpression datasetExpr;
            if (this.schema.AssignmentObjects[this.GetOriginalText(context)] == null)
                datasetExpr = this.exprFactory.GetExpression("get", ExpressionFactoryNameTarget.OperatorSymbol);
            else
            {
                datasetExpr = this.exprFactory.GetExpression("ref", ExpressionFactoryNameTarget.OperatorSymbol);
                datasetExpr.ReferenceExpression = this.schema.AssignmentObjects[this.GetOriginalText(context)].Expression;
                this.currentRefs.Add(this.GetOriginalText(context));
            }

            datasetExpr.ExpressionText = this.GetOriginalText(context);
            datasetExpr.LineNumber = context.Start.Line;
            return datasetExpr;
        }

        public override IExpression VisitComponentID([NotNull] VtlParser.ComponentIDContext context)
        {
            IExpression componentExpr = this.exprFactory.GetExpression("comp", ExpressionFactoryNameTarget.OperatorSymbol);
            componentExpr.ExpressionText = this.GetOriginalText(context);
            componentExpr.LineNumber = context.Start.Line;

            return componentExpr;
        }

        public override IExpression VisitConstant([NotNull] VtlParser.ConstantContext context)
        {
            IExpression constantExpr = this.exprFactory.GetExpression("const", ExpressionFactoryNameTarget.OperatorSymbol);
            constantExpr.ExpressionText = this.GetOriginalText(context);
            constantExpr.LineNumber = context.Start.Line;

            constantExpr.ExpressionText = constantExpr.ExpressionText.Replace("+", string.Empty);
            constantExpr.ExpressionText = constantExpr.ExpressionText.Replace("(", string.Empty);
            constantExpr.ExpressionText = constantExpr.ExpressionText.Replace(")", string.Empty);

            int unaryCount = constantExpr.ExpressionText.Where(chr => chr == '-').Count();
            if (unaryCount > 1)
            {
                constantExpr.ExpressionText = constantExpr.ExpressionText.Remove(0, unaryCount - unaryCount % 2);
            }

            return constantExpr;
        }

        private string GetOriginalText(ParserRuleContext ctx)
        {
            int a = ctx.Start.StartIndex;
            int b = ctx.Stop.StopIndex;
            Interval interval = new Interval(a, b);
            return ctx.Start.InputStream.GetText(interval);
        }
    }
}
