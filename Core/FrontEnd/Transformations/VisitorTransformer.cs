﻿namespace StatisticsPoland.VtlProcessing.Core.Transformations
{
    using Antlr4.Runtime;
    using Antlr4.Runtime.Misc;
    using Antlr4.Runtime.Tree;
    using Microsoft.Extensions.Logging;
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.FrontEnd.Antlr;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Transformations.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Transforms a CST tree into a transformation schema structure by using an ANTRL4 visitor.
    /// </summary>
    public sealed class VisitorTransformer : VtlBaseVisitor<IExpression>, ITreeTransformer
    {
        private readonly TransformationSchemaResolver _schemaResolver;
        private readonly DatapointRulesetResolver _dprReslover;
        private readonly IExpressionFactory _exprFactory;
        private readonly IJoinBuilder _joinBuilder;
        private readonly ILogger<VisitorTransformer> _logger;
        private List<string> currentRefs;
        private ITransformationSchema schema;

        /// <summary>
        /// Initializes a new instance of the <see cref="VisitorTransformer"/> class.
        /// </summary>
        /// <param name="schemaResolver">The transformation schema resolver.</param>
        /// <param name="dprReslover">The datapoint ruleset resolver.</param>
        /// <param name="exprFactory">The expressions factory.</param>
        /// <param name="joinBuilder">The "join" operator expressions builder.</param>
        /// <param name="logger">The errors logger.</param>
        public VisitorTransformer(
            TransformationSchemaResolver schemaResolver, 
            DatapointRulesetResolver dprReslover,
            IExpressionFactory exprFactory, 
            IJoinBuilder joinBuilder, 
            ILogger<VisitorTransformer> logger = null)
        {
            this._schemaResolver = schemaResolver;
            this._dprReslover = dprReslover;
            this._exprFactory = exprFactory;
            this._joinBuilder = joinBuilder;
            this._logger = logger;
        }

        public ITransformationSchema TransformToSchema(IParseTree tree)
        {
            this.schema = this._schemaResolver();

            try
            {
                this.Visit(tree);
            }
            catch (Exception ex)
            {
                this._logger?.LogError(ex, ex.Message);
            }

            return this.schema;
        }

        /// <summary>
        /// Node to walk to a tree.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Null.</returns>
        public override IExpression VisitStart(VtlParser.StartContext context)
        {
            foreach(VtlParser.StatementContext op in context.statement())
            {
                this.Visit(op);
            }
            
            return null;
        }

        /// <summary>
        /// Represents a single VTL 2.0 expression. Returns an expression of the assignment. The variable name is the name of the result of the expression.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The VTL 2.0 expression.</returns>
        public override IExpression VisitStatement([NotNull] VtlParser.StatementContext context)
        {
            this.currentRefs = new List<string>();

            IExpression statementExpr;
            if (context.datasetID() != null)
            {
                if (context.dataset() != null) statementExpr = this.Visit(context.dataset());
                else statementExpr = this.Visit(context.scalar());

                statementExpr.ResultName = context.datasetID().GetText();
                statementExpr.ParamSignature = "<root>";
                statementExpr.LineNumber = context.Start.Line;
                statementExpr.SetContainingSchema(this.schema);

                while (statementExpr.ExpressionText[0] == ' ')
                {
                    statementExpr.ExpressionText = statementExpr.ExpressionText.Remove(0, 1);
                }

                this.schema.AssignmentObjects.Add(new AssignmentObject(this.schema, statementExpr, context.PUT_SYMBOL() != null, this.currentRefs));
            }
            else statementExpr = this.Visit(context.defExpr());

            return statementExpr;
        }

        public override IExpression VisitOpenedDataset([NotNull] VtlParser.OpenedDatasetContext context)
        {
            IExpression datasetExpr;
            if (context.opSymbol == null) return this.Visit(context.ifThenElseDataset());
            datasetExpr = this._exprFactory.GetExpression(context.opSymbol.Text, ExpressionFactoryNameTarget.OperatorSymbol);

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
            IExpression datasetExpr;
            if (context.opSymbol == null)
            {
                if (context.datasetID() != null) return this.Visit(context.datasetID());
                if (context.datasetComplex() != null) return this.Visit(context.datasetComplex());
                if (context.openedDataset() != null) return this.Visit(context.openedDataset());
                if (context.membershipDataset() != null) return this.Visit(context.membershipDataset());
                if (context.closedDataset() != null)
                {
                    if (context.datasetClause() == null) return this.Visit(context.closedDataset());
                    datasetExpr = this._exprFactory.GetExpression("datasetClause", ExpressionFactoryNameTarget.OperatorSymbol);
                    datasetExpr.AddOperand("ds_1", this.Visit(context.closedDataset()));
                    datasetExpr.AddOperand("ds_2", this.Visit(context.datasetClause()));
                }
                else throw new ArgumentException("Missing context in ClosedDatasetContext.", "context");
            }
            else
            {
                string opSymbol = context.opSymbol.Text;
                if (opSymbol == "time_agg") throw new NotImplementedException("Operator time_agg is not supported.");
                if (opSymbol == "+") return this.Visit(context.dataset()[0]);
                if (opSymbol == "-") opSymbol = "minus";

                datasetExpr = this._exprFactory.GetExpression(opSymbol, ExpressionFactoryNameTarget.OperatorSymbol);

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
            IExpression membershipExpr = this._exprFactory.GetExpression("#", ExpressionFactoryNameTarget.OperatorSymbol);
            membershipExpr.ExpressionText = this.GetOriginalText(context);
            membershipExpr.LineNumber = context.Start.Line;
            membershipExpr.AddOperand("ds_1", this.Visit(context.closedDataset()));
            membershipExpr.AddOperand("ds_2", this.Visit(context.componentID()));

            return membershipExpr;
        }

        public override IExpression VisitIfThenElseDataset([NotNull] VtlParser.IfThenElseDatasetContext context)
        {
            IExpression ifThenElseExpr = this._exprFactory.GetExpression("if", ExpressionFactoryNameTarget.OperatorSymbol);
            ifThenElseExpr.ExpressionText = this.GetOriginalText(context);
            ifThenElseExpr.LineNumber = context.Start.Line;

            IExpression ifExpr = this._exprFactory.ExprResolver();
            IExpression thenExpr = this._exprFactory.ExprResolver();
            IExpression elseExpr = this._exprFactory.ExprResolver();

            if (context.ifDataset != null) ifExpr.AddOperand("ds_1", this.Visit(context.ifDataset));
            else ifExpr.AddOperand("ds_1", this.Visit(context.ifScalar));

            if (context.thenDataset != null) thenExpr.AddOperand("ds_1", this.Visit(context.thenDataset));
            else thenExpr.AddOperand("ds_1", this.Visit(context.thenScalar));

            if (context.elseDataset != null) elseExpr.AddOperand("ds_1", this.Visit(context.elseDataset));
            else elseExpr.AddOperand("ds_1", this.Visit(context.elseScalar));

            ifExpr.ResultName = "If";
            thenExpr.ResultName = "Then";
            elseExpr.ResultName = "Else";
            ifExpr.ExpressionText = $"if {ifExpr.Operands["ds_1"].ExpressionText}";
            thenExpr.ExpressionText = $"then {thenExpr.Operands["ds_1"].ExpressionText}";
            elseExpr.ExpressionText = $"else {elseExpr.Operands["ds_1"].ExpressionText}";
            ifExpr.LineNumber = ifThenElseExpr.LineNumber;
            thenExpr.LineNumber = ifThenElseExpr.LineNumber;
            elseExpr.LineNumber = ifThenElseExpr.LineNumber;

            ifThenElseExpr.AddOperand("if", ifExpr);
            ifThenElseExpr.AddOperand("then", thenExpr);
            ifThenElseExpr.AddOperand("else", elseExpr);

            return ifThenElseExpr;
        }

        public override IExpression VisitComponent([NotNull] VtlParser.ComponentContext context)
        {
            IExpression componentExpr;
            if (context.MEMBERSHIP() != null)
            {
                componentExpr = this._exprFactory.GetExpression("#", ExpressionFactoryNameTarget.OperatorSymbol);
                componentExpr.AddOperand("ds_1", this.Visit(context.closedDataset()));
                componentExpr.AddOperand("ds_2", this.Visit(context.componentID()));
            }
            else componentExpr = this.Visit(context.componentID());

            componentExpr.ExpressionText = this.GetOriginalText(context);
            componentExpr.LineNumber = context.Start.Line;
            
            return componentExpr;
        }

        public override IExpression VisitScalar([NotNull] VtlParser.ScalarContext context)
        {
            IExpression scalarExpr;
            if (context.component() != null)
            {
                if (context.opSymbol?.Text == "-")
                {
                    scalarExpr = this._exprFactory.GetExpression("minus", ExpressionFactoryNameTarget.OperatorSymbol);
                    scalarExpr.AddOperand("ds_1", this.Visit(context.component()));
                    return scalarExpr;
                }

                return this.Visit(context.component());
            }

            if (context.constant() != null) return this.Visit(context.constant());
            if (context.ifThenElseScalar() != null) return this.Visit(context.ifThenElseScalar());
            if (context.opSymbol == null) return this.Visit(context.scalar()[0]);
            scalarExpr = this._exprFactory.GetExpression(context.opSymbol.Text, ExpressionFactoryNameTarget.OperatorSymbol);

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

        public override IExpression VisitIfThenElseScalar([NotNull] VtlParser.IfThenElseScalarContext context)
        {
            IExpression ifThenElseExpr = this._exprFactory.GetExpression("if", ExpressionFactoryNameTarget.OperatorSymbol);
            ifThenElseExpr.ExpressionText = this.GetOriginalText(context);
            ifThenElseExpr.LineNumber = context.Start.Line;

            IExpression ifExpr = this._exprFactory.ExprResolver();
            IExpression thenExpr = this._exprFactory.ExprResolver();
            IExpression elseExpr = this._exprFactory.ExprResolver();

            ifExpr.AddOperand("ds_1", this.Visit(context.scalar()[0]));
            thenExpr.AddOperand("ds_1", this.Visit(context.scalar()[1]));
            elseExpr.AddOperand("ds_1", this.Visit(context.scalar()[2]));

            ifExpr.ResultName = "If";
            thenExpr.ResultName = "Then";
            elseExpr.ResultName = "Else";
            ifExpr.ExpressionText = $"if {ifExpr.Operands["ds_1"].ExpressionText}";
            thenExpr.ExpressionText = $"then {thenExpr.Operands["ds_1"].ExpressionText}";
            elseExpr.ExpressionText = $"else {elseExpr.Operands["ds_1"].ExpressionText}";
            ifExpr.LineNumber = ifThenElseExpr.LineNumber;
            thenExpr.LineNumber = ifThenElseExpr.LineNumber;
            elseExpr.LineNumber = ifThenElseExpr.LineNumber;

            ifThenElseExpr.AddOperand("if", ifExpr);
            ifThenElseExpr.AddOperand("then", thenExpr);
            ifThenElseExpr.AddOperand("else", elseExpr);

            return ifThenElseExpr;
        }

        public override IExpression VisitOptionalExpr([NotNull] VtlParser.OptionalExprContext context)
        {
            IExpression optionalExpr;
            if (context.scalar() != null) return this.Visit(context.scalar());
            optionalExpr = this._exprFactory.GetExpression("optional", ExpressionFactoryNameTarget.OperatorSymbol);
            
            optionalExpr.ExpressionText = this.GetOriginalText(context);
            optionalExpr.LineNumber = context.Start.Line;
            return optionalExpr;
        }

        public override IExpression VisitSetExpr([NotNull] VtlParser.SetExprContext context)
        {
            IExpression setExpr = this._exprFactory.GetExpression(context.opSymbol.Text, ExpressionFactoryNameTarget.OperatorSymbol);
            setExpr.ExpressionText = this.GetOriginalText(context);
            setExpr.LineNumber = context.Start.Line;
            setExpr.AddOperand("ds_1", this.Visit(context.dataset()[0]));
            setExpr.AddOperand("ds_2", this.Visit(context.dataset()[1]));

            return setExpr;
        }
        
        public override IExpression VisitDatasetClause([NotNull] VtlParser.DatasetClauseContext context)
        {
            if (context.calcClause() != null) return this.Visit(context.calcClause());
            if (context.filterClause() != null) return this.Visit(context.filterClause());
            if (context.keepClause() != null) return this.Visit(context.keepClause());
            if (context.dropClause() != null) return this.Visit(context.dropClause());
            if (context.renameClause() != null) return this.Visit(context.renameClause());
            if (context.aggrClause() != null) return this.Visit(context.aggrClause());
            if (context.pivotClause() != null) return this.Visit(context.pivotClause());
            if (context.unpivotClause() != null) return this.Visit(context.unpivotClause());
            return this.Visit(context.subspaceClause());
        }

        public override IExpression VisitAggrClause([NotNull] VtlParser.AggrClauseContext context)
        {
            IExpression aggrClauseExpr = this._exprFactory.GetExpression("aggr", ExpressionFactoryNameTarget.OperatorSymbol);
            aggrClauseExpr.ExpressionText = this.GetOriginalText(context);
            aggrClauseExpr.LineNumber = context.Start.Line;

            IExpression calcClauseExpr = this._exprFactory.GetExpression("calc", ExpressionFactoryNameTarget.OperatorSymbol);
            calcClauseExpr.ExpressionText = $"calc {this.GetOriginalText(context).Split("aggr ")[1].Split("group")[0]}";

            for (int i = 0; i < context.aggrExpr().Length; i++)
            {
                calcClauseExpr.AddOperand($"ds_{i + 1}", this.Visit(context.aggrExpr()[i]));
            }

            aggrClauseExpr.AddOperand("calc", calcClauseExpr);
            aggrClauseExpr.AddOperand("group", this.Visit(context.groupingClause()));
            if (context.havingClause() != null) aggrClauseExpr.AddOperand("having", this.Visit(context.havingClause()));

            return aggrClauseExpr;
        }

        public override IExpression VisitAggrExpr([NotNull] VtlParser.AggrExprContext context)
        {
            IExpression aggrExpr = this._exprFactory.GetExpression("calcExpr", ExpressionFactoryNameTarget.OperatorSymbol);
            aggrExpr.ExpressionText = this.GetOriginalText(context);
            aggrExpr.LineNumber = context.Start.Line;

            aggrExpr.OperatorDefinition.Keyword = "measure";
            if (context.componentRole() != null) aggrExpr.OperatorDefinition.Keyword = context.componentRole().GetText().Replace("viral", "viral ");

            aggrExpr.AddOperand("ds_1", this.Visit(context.componentID()));
            aggrExpr.AddOperand("ds_2", this.Visit(context.aggrFunction()));

            return aggrExpr;
        }

        public override IExpression VisitFilterClause([NotNull] VtlParser.FilterClauseContext context)
        {
            IExpression filterExpr = this.Visit(context.scalar());
            filterExpr.ResultName = "Filter";
            filterExpr.ExpressionText = this.GetOriginalText(context);
            filterExpr.LineNumber = context.Start.Line;

            return filterExpr;
        }

        public override IExpression VisitRenameClause([NotNull] VtlParser.RenameClauseContext context)
        {
            IExpression renameClauseExpr = this._exprFactory.GetExpression("rename", ExpressionFactoryNameTarget.OperatorSymbol);
            renameClauseExpr.ExpressionText = this.GetOriginalText(context);
            renameClauseExpr.LineNumber = context.Start.Line;

            for (int i = 0; i < context.renameExpr().Length; i++)
            {
                renameClauseExpr.AddOperand($"ds_{i + 1}", this.Visit(context.renameExpr()[i]));
            }

            return renameClauseExpr;
        }

        public override IExpression VisitRenameExpr([NotNull] VtlParser.RenameExprContext context)
        {
            IExpression renameExpr = this._exprFactory.GetExpression("renameExpr", ExpressionFactoryNameTarget.OperatorSymbol);
            renameExpr.ExpressionText = this.GetOriginalText(context);
            renameExpr.LineNumber = context.Start.Line;
            renameExpr.AddOperand("ds_1", this.Visit(context.component()));
            renameExpr.AddOperand("ds_2", this.Visit(context.componentID()));

            return renameExpr;
        }

        public override IExpression VisitCalcClause([NotNull] VtlParser.CalcClauseContext context)
        {
            IExpression calcClauseExpr = this._exprFactory.GetExpression("calc", ExpressionFactoryNameTarget.OperatorSymbol);
            calcClauseExpr.ExpressionText = this.GetOriginalText(context);
            calcClauseExpr.LineNumber = context.Start.Line;

            for (int i = 0; i < context.calcExpr().Length; i++)
            {
                calcClauseExpr.AddOperand($"ds_{i + 1}", this.Visit(context.calcExpr()[i]));
            }

            return calcClauseExpr;
        }

        public override IExpression VisitCalcExpr([NotNull] VtlParser.CalcExprContext context)
        {
            IExpression calcExpr = this._exprFactory.GetExpression("calcExpr", ExpressionFactoryNameTarget.OperatorSymbol);
            calcExpr.ExpressionText = this.GetOriginalText(context);
            calcExpr.LineNumber = context.Start.Line;

            calcExpr.OperatorDefinition.Keyword = "measure";
            if (context.componentRole() != null) calcExpr.OperatorDefinition.Keyword = context.componentRole().GetText().Replace("viral", "viral ");

            calcExpr.AddOperand("ds_1", this.Visit(context.componentID()));
            calcExpr.AddOperand("ds_2", this.Visit((IParseTree)context.scalar() ?? (IParseTree)context.analyticFunction()));

            return calcExpr;
        }

        public override IExpression VisitKeepClause([NotNull] VtlParser.KeepClauseContext context)
        {
            IExpression keepClauseExpr = this._exprFactory.GetExpression("keep", ExpressionFactoryNameTarget.OperatorSymbol);
            keepClauseExpr.ResultName = "Keep";
            keepClauseExpr.ExpressionText = this.GetOriginalText(context);
            keepClauseExpr.LineNumber = context.Start.Line;

            for (int i = 0; i < context.component().Length; i++)
            {
                keepClauseExpr.AddOperand($"ds_{i + 1}", this.Visit(context.component()[i]));
            }

            return keepClauseExpr;
        }

        public override IExpression VisitDropClause([NotNull] VtlParser.DropClauseContext context)
        {
            IExpression dropClauseExpr = this._exprFactory.GetExpression("drop", ExpressionFactoryNameTarget.OperatorSymbol);
            dropClauseExpr.ResultName = "Drop";
            dropClauseExpr.ExpressionText = this.GetOriginalText(context);
            dropClauseExpr.LineNumber = context.Start.Line;

            for (int i = 0; i < context.component().Length; i++)
            {
                dropClauseExpr.AddOperand($"ds_{i + 1}", this.Visit(context.component()[i]));
            }

            return dropClauseExpr;
        }

        public override IExpression VisitPivotClause([NotNull] VtlParser.PivotClauseContext context)
        {
            IExpression pivotClauseExpr = this._exprFactory.GetExpression("pivot", ExpressionFactoryNameTarget.OperatorSymbol);
            pivotClauseExpr.ExpressionText = this.GetOriginalText(context);
            pivotClauseExpr.LineNumber = context.Start.Line;

            for (int i = 0; i < context.componentID().Length; i++)
            {
                pivotClauseExpr.AddOperand($"ds_{i + 1}", this.Visit(context.componentID()[i]));
            }

            return pivotClauseExpr;
        }

        public override IExpression VisitUnpivotClause([NotNull] VtlParser.UnpivotClauseContext context)
        {
            IExpression unpivotClauseExpr = this._exprFactory.GetExpression("unpivot", ExpressionFactoryNameTarget.OperatorSymbol);
            unpivotClauseExpr.ExpressionText = this.GetOriginalText(context);
            unpivotClauseExpr.LineNumber = context.Start.Line;

            for (int i = 0; i < context.componentID().Length; i++)
            {
                unpivotClauseExpr.AddOperand($"ds_{i + 1}", this.Visit(context.componentID()[i]));
            }

            return unpivotClauseExpr;
        }

        public override IExpression VisitSubspaceClause([NotNull] VtlParser.SubspaceClauseContext context)
        {
            IExpression subspaceClauseExpr = this._exprFactory.GetExpression("sub", ExpressionFactoryNameTarget.OperatorSymbol);
            subspaceClauseExpr.ExpressionText = this.GetOriginalText(context);
            subspaceClauseExpr.LineNumber = context.Start.Line;

            for (int i = 0; i < context.subspaceExpr().Length; i++)
            {
                subspaceClauseExpr.AddOperand($"ds_{i + 1}", this.Visit(context.subspaceExpr()[i]));
            }

            return subspaceClauseExpr;
        }

        public override IExpression VisitSubspaceExpr([NotNull] VtlParser.SubspaceExprContext context)
        {
            IExpression subspaceExpr = this._exprFactory.GetExpression("subExpr", ExpressionFactoryNameTarget.OperatorSymbol);
            subspaceExpr.ExpressionText = this.GetOriginalText(context);
            subspaceExpr.LineNumber = context.Start.Line;
            subspaceExpr.AddOperand("ds_1", this.Visit(context.component()));
            subspaceExpr.AddOperand("ds_2", this.Visit(context.constant()));

            return subspaceExpr;
        }

        public override IExpression VisitJoinExpr([NotNull] VtlParser.JoinExprContext context)
        {
            IExpression joinExpr = this._exprFactory.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol);
            joinExpr.ExpressionText = this.GetOriginalText(context);
            joinExpr.LineNumber = context.Start.Line;
            joinExpr.OperatorDefinition.Keyword = context.joinKeyword().GetText().Split('_')[0];

            this.Visit(context.joinClause());
            this.Visit(context.joinBody());

            joinExpr = this._joinBuilder
                .AddMainExpr(joinExpr)
                .Build();

            foreach (IExpression membershipExpr in joinExpr.GetDescendantExprs("Membership"))
            {
                if (membershipExpr.GetFirstAncestorExpr("Alias") == null) membershipExpr.OperatorDefinition.Keyword = "Component";
            }

            this._joinBuilder.Clear();
            return joinExpr;
        }

        public override IExpression VisitJoinAliasesClause([NotNull] VtlParser.JoinAliasesClauseContext context)
        {
            IExpression dsBranch = this._exprFactory.GetExpression("Alias", ExpressionFactoryNameTarget.ResultName);
            dsBranch.ExpressionText = this.GetOriginalText(context);
            dsBranch.LineNumber = context.Start.Line;

            for (int i = 0; i < context.joinAliasExpr().Length; i++)
            {
                IExpression aliasExpr = this.Visit(context.joinAliasExpr()[i]);
                if (aliasExpr.ParamSignature == "ds") aliasExpr.ParamSignature = $"ds{i + 1}";

                dsBranch.AddOperand(aliasExpr.ParamSignature, aliasExpr);
                dsBranch.Operands[aliasExpr.ParamSignature].ExpressionText = dsBranch.Operands[aliasExpr.ParamSignature].ExpressionText.Split(" as")[0];
            }

            this._joinBuilder.AddBranch("ds", dsBranch);
            return dsBranch;
        }

        public override IExpression VisitJoinAliasExpr([NotNull] VtlParser.JoinAliasExprContext context)
        {
            IExpression aliasExpr = this.Visit(context.dataset());
            aliasExpr.ExpressionText = this.GetOriginalText(context);
            aliasExpr.LineNumber = context.Start.Line;
            aliasExpr.ParamSignature = context.varID()?.GetText();

            if (aliasExpr.ParamSignature == null) aliasExpr.ParamSignature = "ds";

            return aliasExpr;
        }

        public override IExpression VisitJoinUsingClause([NotNull] VtlParser.JoinUsingClauseContext context)
        {
            IExpression usingBranch = this._exprFactory.GetExpression("Using", ExpressionFactoryNameTarget.ResultName);
            usingBranch.ExpressionText = this.GetOriginalText(context);
            usingBranch.LineNumber = context.Start.Line;

            for (int i = 0; i < context.componentID().Length; i++)
            {
                usingBranch.AddOperand(context.componentID()[i].GetText(), this.Visit(context.componentID()[i]));
            }

            this._joinBuilder.AddBranch("using", usingBranch);
            return usingBranch;
        }

        public override IExpression VisitJoinCalcClause([NotNull] VtlParser.JoinCalcClauseContext context)
        {
            IExpression calcBranch = this.Visit(context.calcClause());

            this._joinBuilder.AddBranch("calc", calcBranch);
            return calcBranch;
        }

        public override IExpression VisitJoinAggrClause([NotNull] VtlParser.JoinAggrClauseContext context)
        {
            IExpression aggrBranch = this.Visit(context.aggrClause());

            this._joinBuilder.AddBranch("aggr", aggrBranch);
            return aggrBranch;
        }

        public override IExpression VisitJoinKeepClause([NotNull] VtlParser.JoinKeepClauseContext context)
        {
            IExpression keepBranch = this.Visit(context.keepClause());

            this._joinBuilder.AddBranch("keep", keepBranch);
            return keepBranch;
        }

        public override IExpression VisitJoinDropClause([NotNull] VtlParser.JoinDropClauseContext context)
        {
            IExpression dropBranch = this.Visit(context.dropClause());

            this._joinBuilder.AddBranch("drop", dropBranch);
            return dropBranch;
        }

        public override IExpression VisitJoinFilterClause([NotNull] VtlParser.JoinFilterClauseContext context)
        {
            IExpression filterBranch = this.Visit(context.filterClause());

            this._joinBuilder.AddBranch("filter", filterBranch);
            return filterBranch;
        }

        public override IExpression VisitJoinRenameClause([NotNull] VtlParser.JoinRenameClauseContext context)
        {
            IExpression renameBranch = this.Visit(context.renameClause());

            this._joinBuilder.AddBranch("rename", renameBranch);
            return renameBranch;
        }

        public override IExpression VisitJoinApplyClause([NotNull] VtlParser.JoinApplyClauseContext context)
        {
            IExpression applyBranch = this.Visit(context.scalar());
            applyBranch.ResultName = "Apply";

            if (applyBranch.GetDescendantExprs("Membership").Count > 0)
                throw new VtlOperatorError(applyBranch, "apply", "Operator membership (#) can not be used in join apply operator.");

            bool aliasFound = false;
            foreach (IExpression expr in applyBranch.GetDescendantExprs("Component"))
            {
                expr.OperatorDefinition = this._exprFactory.OperatorResolver("ref");
                expr.ReferenceExpression = this._joinBuilder.Branches["ds"].OperandsCollection.FirstOrDefault(alias => alias.ParamSignature == expr.ExpressionText);
                if (expr.ReferenceExpression == null)
                {
                    if (this.schema.AssignmentObjects.FirstOrDefault(ao => ao.Name == expr.ExpressionText) != null)
                    {
                        expr.ReferenceExpression = this.schema.AssignmentObjects[expr.ExpressionText].Expression;
                        expr.ResultName = "Reference";
                    }
                    else throw new VtlOperatorError(applyBranch, "apply", $"Alias {expr.ExpressionText} has been not found.");
                }
                else
                {
                    expr.ResultName = "Alias";
                    aliasFound = true;
                }
            }

            if (!aliasFound) throw new VtlOperatorError(applyBranch, "apply", "Expected alias reference in apply operator expression.");

            this._joinBuilder.AddBranch("apply", applyBranch);
            return applyBranch;
        }

        public override IExpression VisitAggrInvocation([NotNull] VtlParser.AggrInvocationContext context)
        {
            IExpression aggrInvocationExpr = this._exprFactory.GetExpression(context.aggrFunctionName().GetText(), ExpressionFactoryNameTarget.OperatorSymbol);
            aggrInvocationExpr.ExpressionText = this.GetOriginalText(context);
            aggrInvocationExpr.LineNumber = context.Start.Line;

            aggrInvocationExpr.AddOperand("ds_1", this.Visit(context.dataset()));
            aggrInvocationExpr.AddOperand("group", this.Visit(context.groupingClause()));
            if (context.havingClause() != null) aggrInvocationExpr.AddOperand("having", this.Visit(context.havingClause()));

            return aggrInvocationExpr;
        }

        public override IExpression VisitAggrFunction([NotNull] VtlParser.AggrFunctionContext context)
        {
            IExpression aggrFunctionExpr = this._exprFactory.GetExpression(context.opSymbol.Text, ExpressionFactoryNameTarget.OperatorSymbol);
            aggrFunctionExpr.ExpressionText = this.GetOriginalText(context);
            aggrFunctionExpr.LineNumber = context.Start.Line;
            if (context.component() != null) aggrFunctionExpr.AddOperand("ds_1", this.Visit(context.component()));

            return aggrFunctionExpr;
        }

        public override IExpression VisitGroupingClause([NotNull] VtlParser.GroupingClauseContext context)
        {
            IExpression groupingClauseExpr = this._exprFactory.GetExpression("group", ExpressionFactoryNameTarget.OperatorSymbol);

            if (groupingClauseExpr.OperatorDefinition.Keyword == null) groupingClauseExpr.OperatorDefinition.Keyword = context.groupKeyword().BY()?.GetText();
            if (groupingClauseExpr.OperatorDefinition.Keyword == null) groupingClauseExpr.OperatorDefinition.Keyword = context.groupKeyword().EXCEPT()?.GetText();
            if (groupingClauseExpr.OperatorDefinition.Keyword == null) groupingClauseExpr.OperatorDefinition.Keyword = context.groupKeyword().ALL()?.GetText();

            groupingClauseExpr.ExpressionText = $"{this.GetOriginalText(context.groupKeyword())} {this.GetOriginalText(context).Split(groupingClauseExpr.OperatorDefinition.Keyword + " ")[1]}";
            groupingClauseExpr.LineNumber = context.Start.Line;

            for (int i = 0; i < context.component().Length; i++)
            {
                groupingClauseExpr.AddOperand($"ds_{i + 1}", this.Visit(context.component()[i]));
            }

            return groupingClauseExpr;
        }

        public override IExpression VisitHavingClause([NotNull] VtlParser.HavingClauseContext context)
        {
            IExpression havingClauseExpr = this.Visit(context.havingExpr());
            havingClauseExpr.ResultName = "Having";
            havingClauseExpr.ExpressionText = this.GetOriginalText(context);
            havingClauseExpr.LineNumber = context.Start.Line;

            return havingClauseExpr;
        }

        public override IExpression VisitHavingExpr([NotNull] VtlParser.HavingExprContext context)
        {
            IExpression havingExpr = this._exprFactory.GetExpression(context.opSymbol.Text, ExpressionFactoryNameTarget.OperatorSymbol);
            havingExpr.ExpressionText = this.GetOriginalText(context);
            havingExpr.LineNumber = context.Start.Line;

            if (context.leftScalar != null)
            {
                havingExpr.AddOperand("ds_1", this.Visit(context.leftScalar));
                havingExpr.AddOperand("ds_2", this.Visit(context.aggrFunction()));
            }
            else if (context.leftAggrFunction != null)
            {
                havingExpr.AddOperand("ds_1", this.Visit(context.leftAggrFunction));
                havingExpr.AddOperand("ds_2", this.Visit(context.scalar()));
            }
            else
            {
                havingExpr.AddOperand("ds_1", this.Visit(context.havingExpr()[0]));
                havingExpr.AddOperand("ds_2", this.Visit(context.havingExpr()[1]));
            }

            return havingExpr;
        }

        public override IExpression VisitAnalyticInvocation([NotNull] VtlParser.AnalyticInvocationContext context)
        {
            string opSymbol = context.opSymbol?.Text ?? this.GetOriginalText(context.aggrFunctionName());
            
            IExpression analyticInvocationExpr = this._exprFactory.GetExpression(opSymbol, ExpressionFactoryNameTarget.OperatorSymbol);
            analyticInvocationExpr.ExpressionText = this.GetOriginalText(context);
            analyticInvocationExpr.LineNumber = context.Start.Line;

            analyticInvocationExpr.AddOperand("ds_1", this.Visit(context.dataset()));

            if (!opSymbol.In("ratio_to_report", "lag", "lead")) analyticInvocationExpr.AddOperand("over", this.Visit(context.analyticClause()));
            else
            {
                IExpression overExpr = this._exprFactory.GetExpression("Over", ExpressionFactoryNameTarget.ResultName);
                overExpr.ExpressionText = "over (";
                overExpr.LineNumber = context.Start.Line;

                if (context.partitionClause() != null)
                {
                    overExpr.ExpressionText += $"{this.GetOriginalText(context.partitionClause())} ";
                    overExpr.AddOperand("partition", this.Visit(context.partitionClause()));
                }

                if (context.orderClause() != null)
                {
                    overExpr.ExpressionText += $"{this.GetOriginalText(context.orderClause())})";
                    overExpr.AddOperand("order", this.Visit(context.orderClause()));
                }

                analyticInvocationExpr.AddOperand("over", overExpr);

                if (opSymbol.In("lag", "lead"))
                {
                    analyticInvocationExpr.AddOperand("offset", this.Visit(context.scalar()[0]));
                    if (context.scalar().Length > 1) analyticInvocationExpr.AddOperand("default", this.Visit(context.scalar()[1]));
                }
            }

            return analyticInvocationExpr;
        }

        public override IExpression VisitAnalyticFunction([NotNull] VtlParser.AnalyticFunctionContext context)
        {
            string opSymbol = context.opSymbol?.Text ?? this.GetOriginalText(context.aggrFunctionName());

            IExpression analyticFunctionExpr = this._exprFactory.GetExpression(opSymbol, ExpressionFactoryNameTarget.OperatorSymbol);
            analyticFunctionExpr.ExpressionText = this.GetOriginalText(context);
            analyticFunctionExpr.LineNumber = context.Start.Line;

            if (context.component() != null) analyticFunctionExpr.AddOperand("ds_1", this.Visit(context.component()));

            if (!opSymbol.In("rank", "ration_to_report", "lag", "lead")) analyticFunctionExpr.AddOperand("over", this.Visit(context.analyticClause()));
            else
            {
                IExpression overExpr = this._exprFactory.GetExpression("Over", ExpressionFactoryNameTarget.ResultName);
                overExpr.ExpressionText = "over (";
                overExpr.LineNumber = context.Start.Line;

                if (context.partitionClause() != null)
                {
                    overExpr.ExpressionText += $"{this.GetOriginalText(context.partitionClause())} ";
                    overExpr.AddOperand("partition", this.Visit(context.partitionClause()));
                }

                if (context.orderClause() != null)
                {
                    overExpr.ExpressionText += $"{this.GetOriginalText(context.orderClause())})";
                    overExpr.AddOperand("order", this.Visit(context.orderClause()));
                }

                analyticFunctionExpr.AddOperand("over", overExpr);

                if (opSymbol.In("lag", "lead"))
                {
                    analyticFunctionExpr.AddOperand("offset", this.Visit(context.scalar()[0]));
                    if (context.scalar().Length > 1) analyticFunctionExpr.AddOperand("default", this.Visit(context.scalar()[1]));
                }
            }

            return analyticFunctionExpr;
        }

        public override IExpression VisitAnalyticClause([NotNull] VtlParser.AnalyticClauseContext context)
        {
            IExpression analyticClauseExpr = this._exprFactory.GetExpression("Over", ExpressionFactoryNameTarget.ResultName);
            analyticClauseExpr.ExpressionText = $"over ({this.GetOriginalText(context)})";
            analyticClauseExpr.LineNumber = context.Start.Line;

            if (context.partitionClause() != null) analyticClauseExpr.AddOperand("partition", this.Visit(context.partitionClause()));
            if (context.orderClause() != null) analyticClauseExpr.AddOperand("order", this.Visit(context.orderClause()));
            if (context.windowingClause() != null) analyticClauseExpr.AddOperand("window", this.Visit(context.windowingClause()));

            return analyticClauseExpr;
        }

        public override IExpression VisitPartitionClause([NotNull] VtlParser.PartitionClauseContext context)
        {
            IExpression partitionClauseExpr = this._exprFactory.GetExpression("partition", ExpressionFactoryNameTarget.OperatorSymbol);
            partitionClauseExpr.ExpressionText = this.GetOriginalText(context);
            partitionClauseExpr.LineNumber = context.Start.Line;

            for (int i = 0; i < context.component().Length; i++)
            {
                partitionClauseExpr.AddOperand($"ds_{i + 1}", this.Visit(context.component()[i]));
            }

            return partitionClauseExpr;
        }

        public override IExpression VisitOrderClause([NotNull] VtlParser.OrderClauseContext context)
        {
            IExpression orderClauseExpr = this._exprFactory.GetExpression("order", ExpressionFactoryNameTarget.OperatorSymbol);
            orderClauseExpr.ExpressionText = this.GetOriginalText(context);
            orderClauseExpr.LineNumber = context.Start.Line;

            for (int i = 0; i < context.orderExpr().Length; i++)
            {
                orderClauseExpr.AddOperand($"ds_{i + 1}", this.Visit(context.orderExpr()[i]));
            }

            return orderClauseExpr;
        }

        public override IExpression VisitOrderExpr([NotNull] VtlParser.OrderExprContext context)
        {
            IExpression orderExpr = this.Visit(context.component());
            orderExpr.OperatorDefinition.Keyword = context.ASC()?.GetText() ?? context.DESC()?.GetText();

            return orderExpr;
        }

        public override IExpression VisitWindowingClause([NotNull] VtlParser.WindowingClauseContext context)
        {
            IExpression windowingClauseExpr = this._exprFactory.GetExpression("Window", ExpressionFactoryNameTarget.ResultName);
            windowingClauseExpr.ExpressionText = this.GetOriginalText(context);
            windowingClauseExpr.LineNumber = context.Start.Line;

            windowingClauseExpr.AddOperand($"limit_1", this.Visit(context.firstWindowLimit()));
            windowingClauseExpr.AddOperand($"limit_2", this.Visit(context.secondWindowLimit()));

            return windowingClauseExpr;
        }

        public override IExpression VisitFirstWindowLimit([NotNull] VtlParser.FirstWindowLimitContext context)
        {
            IExpression windowLimitExpr = this._exprFactory.GetExpression("WindowLimit", ExpressionFactoryNameTarget.ResultName);
            windowLimitExpr.ExpressionText = this.GetOriginalText(context);
            windowLimitExpr.LineNumber = context.Start.Line;

            return windowLimitExpr;
        }

        public override IExpression VisitSecondWindowLimit([NotNull] VtlParser.SecondWindowLimitContext context)
        {
            IExpression windowLimitExpr = this._exprFactory.GetExpression("WindowLimit", ExpressionFactoryNameTarget.ResultName);
            windowLimitExpr.ExpressionText = this.GetOriginalText(context);
            windowLimitExpr.LineNumber = context.Start.Line;

            return windowLimitExpr;
        }

        public override IExpression VisitList([NotNull] VtlParser.ListContext context)
        {
            IExpression listExpr = this._exprFactory.GetExpression("collection", ExpressionFactoryNameTarget.OperatorSymbol);
            listExpr.ExpressionText = this.GetOriginalText(context);
            listExpr.LineNumber = context.Start.Line;

            for (int i = 0; i < context.scalar().Length; i++)
            {
                listExpr.AddOperand($"ds_{i + 1}", this.Visit(context.scalar()[i]));
            }

            return listExpr;
        }

        public override IExpression VisitDatasetID([NotNull] VtlParser.DatasetIDContext context)
        {
            IExpression datasetExpr;
            if (this.schema.AssignmentObjects[this.GetOriginalText(context)] == null)
                datasetExpr = this._exprFactory.GetExpression("get", ExpressionFactoryNameTarget.OperatorSymbol);
            else
            {
                datasetExpr = this._exprFactory.GetExpression("ref", ExpressionFactoryNameTarget.OperatorSymbol);
                datasetExpr.ReferenceExpression = this.schema.AssignmentObjects[this.GetOriginalText(context)].Expression;
                this.currentRefs.Add(this.GetOriginalText(context));
            }

            datasetExpr.ExpressionText = this.GetOriginalText(context);
            datasetExpr.LineNumber = context.Start.Line;
            return datasetExpr;
        }

        public override IExpression VisitComponentID([NotNull] VtlParser.ComponentIDContext context)
        {
            IExpression componentExpr = this._exprFactory.GetExpression("comp", ExpressionFactoryNameTarget.OperatorSymbol);
            componentExpr.ExpressionText = this.GetOriginalText(context);
            componentExpr.LineNumber = context.Start.Line;

            return componentExpr;
        }

        public override IExpression VisitConstant([NotNull] VtlParser.ConstantContext context)
        {
            IExpression constantExpr = this._exprFactory.GetExpression("const", ExpressionFactoryNameTarget.OperatorSymbol);
            constantExpr.ExpressionText = this.GetOriginalText(context);
            constantExpr.LineNumber = context.Start.Line;

            constantExpr.ExpressionText = constantExpr.ExpressionText.Replace("+", string.Empty);
            constantExpr.ExpressionText = constantExpr.ExpressionText.Replace("(", string.Empty);
            constantExpr.ExpressionText = constantExpr.ExpressionText.Replace(")", string.Empty);

            int unaryCount = constantExpr.ExpressionText.Count(chr => chr == '-');
            if (unaryCount > 1)
            {
                constantExpr.ExpressionText = constantExpr.ExpressionText.Remove(0, unaryCount - unaryCount % 2);
            }

            return constantExpr;
        }

        public override IExpression VisitCheckDatapoint([NotNull] VtlParser.CheckDatapointContext context)
        {
            IExpression checkExpr = this._exprFactory.GetExpression("check_datapoint", ExpressionFactoryNameTarget.OperatorSymbol);
            checkExpr.ExpressionText = this.GetOriginalText(context);
            checkExpr.LineNumber = context.Start.Line;
            checkExpr.OperatorDefinition.Keyword = context.output?.Text ?? "invalid";

            IExpression componentsExpr = this._exprFactory.GetExpression("collection", ExpressionFactoryNameTarget.OperatorSymbol);
            componentsExpr.ExpressionText = "components ";
            componentsExpr.LineNumber = context.Start.Line;

            for (int i = 0; i < context.componentID().Length; i++)
            {
                componentsExpr.AddOperand($"ds_{i + 1}", this.Visit(context.componentID()[i]));
                componentsExpr.ExpressionText += this.GetOriginalText(context.componentID()[i]);
                if (i < context.componentID().Length - 1) componentsExpr.ExpressionText += ", ";
            }

            checkExpr.AddOperand("ds_1", this.Visit(context.dataset()));
            checkExpr.AddOperand("ruleset", this.Visit(context.rulesetID()));
            if (componentsExpr.OperandsCollection.Count > 0 ) checkExpr.AddOperand("comps", componentsExpr);

            return checkExpr;
        }

        public override IExpression VisitDefDatapoint([NotNull] VtlParser.DefDatapointContext context)
        {
            this.schema.Rulesets.Add(this._dprReslover(this.GetOriginalText(context.rulesetID()), this.GetOriginalText(context)));
            base.VisitDefDatapoint(context);

            foreach (IExpression expr in this.schema.Rulesets.Last().RulesCollection)
            {
                expr.SetContainingSchema(this.schema);
            }

            return null;
        }

        public override IExpression VisitRulesetSignature([NotNull] VtlParser.RulesetSignatureContext context)
        {
            IRuleset ruleset = this.schema.Rulesets.Last();
            if (context.VARIABLE() != null) ruleset.Variables.Add("variable", null);
            else ruleset.ValueDomains.Add("valuedomain", null);

            return base.VisitRulesetSignature(context);
        }

        public override IExpression VisitVarSignature([NotNull] VtlParser.VarSignatureContext context)
        {
            IRuleset ruleset = this.schema.Rulesets.Last();

            if (ruleset.Variables.Count > 0)
            {
                if (ruleset.Variables.Last().Value == null) ruleset.Variables.Clear();
                ruleset.Variables.Add(context.IDENTIFIER()?.GetText() ?? $"{this.GetOriginalText(context.varID())}", this.GetOriginalText(context.varID()));
            }
            else
            {
                if (ruleset.ValueDomains.Last().Value == null) ruleset.ValueDomains.Clear();
                ruleset.ValueDomains.Add(context.IDENTIFIER()?.GetText() ?? $"{this.GetOriginalText(context.varID())}", new ValueDomain(this.GetOriginalText(context.varID())));
            }

            return base.VisitVarSignature(context);
        }

        public override IExpression VisitRuleItemDatapoint([NotNull] VtlParser.RuleItemDatapointContext context)
        {
            IRuleset ruleset = this.schema.Rulesets.Last();
            IRuleExpression ruleExpr;
            int? errorLevel = null;

            if (context.errorLevel() != null)
            {
                int errLevel = 0;
                if (!int.TryParse(context.errorLevel().GetText().Replace("errorlevel", string.Empty), out errLevel)) 
                    throw new VtlSyntaxError("Expected integer as errorlevel value.", context.Start.Line);

                errorLevel = errLevel;
            }

            if (context.WHEN() != null)
            {
                IExpression whenExpr = this._exprFactory.GetExpression("when", ExpressionFactoryNameTarget.OperatorSymbol);
                whenExpr.AddOperand("when", this.Visit(context.scalar()[0]));
                whenExpr.AddOperand("then", this.Visit(context.scalar()[1]));
                whenExpr.LineNumber = context.Start.Line;

                ruleExpr = this._exprFactory.RuleExprResolver(whenExpr, ruleset, context.errorCode()?.GetText().Replace("errorcode", string.Empty), errorLevel);
            }
            else ruleExpr = this._exprFactory.RuleExprResolver(this.Visit(context.scalar()[0]), ruleset, context.errorCode()?.GetText().Replace("errorcode", string.Empty), errorLevel);

            if (context.ruleID() != null) ruleExpr.ResultName = this.GetOriginalText(context.ruleID());
            else
            {
                int index = 0;
                ruleset.RulesCollection.LastOrDefault(rule => rule.ResultName.Contains('_') && rule.ResultName.Remove(
                    rule.ResultName.LastIndexOf('_'), rule.ResultName.Split('_').Last().Length + 1) == ruleset.Name && int.TryParse(rule.ResultName.Split('_').Last(), out index));
                ruleExpr.ResultName = $"{ruleset.Name}_{++index}";
            }

            ruleExpr.ExpressionText = this.GetOriginalText(context).Split(":").Last().Split("errorcode")[0].Split("errorlevel")[0];
            if (ruleExpr.ExpressionText[0] == ' ') ruleExpr.ExpressionText = ruleExpr.ExpressionText.Remove(0, 1);

            ruleExpr.LineNumber = context.Start.Line;
            ruleset.Rules.Add(ruleExpr.ResultName, ruleExpr);
            return ruleExpr;
        }

        public override IExpression VisitRulesetID([NotNull] VtlParser.RulesetIDContext context)
        {
            IExpression rulesetExpr = this._exprFactory.GetExpression("Ruleset", ExpressionFactoryNameTarget.ResultName);
            rulesetExpr.ExpressionText = this.GetOriginalText(context);
            rulesetExpr.LineNumber = context.Start.Line;

            return rulesetExpr;
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
