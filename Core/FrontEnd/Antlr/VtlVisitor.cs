//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.7.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from Vtl.g4 by ANTLR 4.7.2

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace StatisticsPoland.VtlProcessing.Core.FrontEnd.Antlr {
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="VtlParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7.2")]
[System.CLSCompliant(false)]
public interface IVtlVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.start"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStart([NotNull] VtlParser.StartContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatement([NotNull] VtlParser.StatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.dataset"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDataset([NotNull] VtlParser.DatasetContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.openedDataset"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOpenedDataset([NotNull] VtlParser.OpenedDatasetContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.closedDataset"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitClosedDataset([NotNull] VtlParser.ClosedDatasetContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.membershipDataset"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMembershipDataset([NotNull] VtlParser.MembershipDatasetContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.datasetComplex"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDatasetComplex([NotNull] VtlParser.DatasetComplexContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.ifThenElseDataset"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIfThenElseDataset([NotNull] VtlParser.IfThenElseDatasetContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.unopenedDataset"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUnopenedDataset([NotNull] VtlParser.UnopenedDatasetContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.component"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitComponent([NotNull] VtlParser.ComponentContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.scalar"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitScalar([NotNull] VtlParser.ScalarContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.ifThenElseScalar"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIfThenElseScalar([NotNull] VtlParser.IfThenElseScalarContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.optionalExpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOptionalExpr([NotNull] VtlParser.OptionalExprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.setExpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSetExpr([NotNull] VtlParser.SetExprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.datasetClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDatasetClause([NotNull] VtlParser.DatasetClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.aggrClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAggrClause([NotNull] VtlParser.AggrClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.aggrExpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAggrExpr([NotNull] VtlParser.AggrExprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.filterClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFilterClause([NotNull] VtlParser.FilterClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.renameClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRenameClause([NotNull] VtlParser.RenameClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.renameExpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRenameExpr([NotNull] VtlParser.RenameExprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.calcClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCalcClause([NotNull] VtlParser.CalcClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.calcExpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCalcExpr([NotNull] VtlParser.CalcExprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.keepClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitKeepClause([NotNull] VtlParser.KeepClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.dropClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDropClause([NotNull] VtlParser.DropClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.pivotClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPivotClause([NotNull] VtlParser.PivotClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.unpivotClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUnpivotClause([NotNull] VtlParser.UnpivotClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.subspaceClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSubspaceClause([NotNull] VtlParser.SubspaceClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.subspaceExpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSubspaceExpr([NotNull] VtlParser.SubspaceExprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.joinExpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitJoinExpr([NotNull] VtlParser.JoinExprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.joinClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitJoinClause([NotNull] VtlParser.JoinClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.joinBody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitJoinBody([NotNull] VtlParser.JoinBodyContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.joinAliasesClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitJoinAliasesClause([NotNull] VtlParser.JoinAliasesClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.joinAliasExpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitJoinAliasExpr([NotNull] VtlParser.JoinAliasExprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.joinUsingClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitJoinUsingClause([NotNull] VtlParser.JoinUsingClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.joinCalcClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitJoinCalcClause([NotNull] VtlParser.JoinCalcClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.joinAggrClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitJoinAggrClause([NotNull] VtlParser.JoinAggrClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.joinKeepClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitJoinKeepClause([NotNull] VtlParser.JoinKeepClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.joinDropClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitJoinDropClause([NotNull] VtlParser.JoinDropClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.joinFilterClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitJoinFilterClause([NotNull] VtlParser.JoinFilterClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.joinRenameClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitJoinRenameClause([NotNull] VtlParser.JoinRenameClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.joinApplyClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitJoinApplyClause([NotNull] VtlParser.JoinApplyClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.aggrInvocation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAggrInvocation([NotNull] VtlParser.AggrInvocationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.aggrFunction"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAggrFunction([NotNull] VtlParser.AggrFunctionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.aggrFunctionName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAggrFunctionName([NotNull] VtlParser.AggrFunctionNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.groupingClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGroupingClause([NotNull] VtlParser.GroupingClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.havingClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitHavingClause([NotNull] VtlParser.HavingClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.havingExpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitHavingExpr([NotNull] VtlParser.HavingExprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.analyticInvocation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAnalyticInvocation([NotNull] VtlParser.AnalyticInvocationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.analyticFunction"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAnalyticFunction([NotNull] VtlParser.AnalyticFunctionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.analyticClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAnalyticClause([NotNull] VtlParser.AnalyticClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.partitionClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPartitionClause([NotNull] VtlParser.PartitionClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.orderClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOrderClause([NotNull] VtlParser.OrderClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.orderExpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOrderExpr([NotNull] VtlParser.OrderExprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.windowingClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWindowingClause([NotNull] VtlParser.WindowingClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.firstWindowLimit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFirstWindowLimit([NotNull] VtlParser.FirstWindowLimitContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.secondWindowLimit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSecondWindowLimit([NotNull] VtlParser.SecondWindowLimitContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.analyticFunctionName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAnalyticFunctionName([NotNull] VtlParser.AnalyticFunctionNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.list"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitList([NotNull] VtlParser.ListContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.varID"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVarID([NotNull] VtlParser.VarIDContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.datasetID"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDatasetID([NotNull] VtlParser.DatasetIDContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.componentID"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitComponentID([NotNull] VtlParser.ComponentIDContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.joinKeyword"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitJoinKeyword([NotNull] VtlParser.JoinKeywordContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.groupKeyword"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGroupKeyword([NotNull] VtlParser.GroupKeywordContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.constant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstant([NotNull] VtlParser.ConstantContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.componentRole"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitComponentRole([NotNull] VtlParser.ComponentRoleContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.valueDomainName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitValueDomainName([NotNull] VtlParser.ValueDomainNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.retainType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRetainType([NotNull] VtlParser.RetainTypeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.limitsMethod"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLimitsMethod([NotNull] VtlParser.LimitsMethodContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.checkDatapoint"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCheckDatapoint([NotNull] VtlParser.CheckDatapointContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.defExpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDefExpr([NotNull] VtlParser.DefExprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.defDatapoint"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDefDatapoint([NotNull] VtlParser.DefDatapointContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.rulesetSignature"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRulesetSignature([NotNull] VtlParser.RulesetSignatureContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.ruleClauseDatapoint"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRuleClauseDatapoint([NotNull] VtlParser.RuleClauseDatapointContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.ruleItemDatapoint"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRuleItemDatapoint([NotNull] VtlParser.RuleItemDatapointContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.varSignature"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVarSignature([NotNull] VtlParser.VarSignatureContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.errorCode"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitErrorCode([NotNull] VtlParser.ErrorCodeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.errorLevel"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitErrorLevel([NotNull] VtlParser.ErrorLevelContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.rulesetID"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRulesetID([NotNull] VtlParser.RulesetIDContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VtlParser.ruleID"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRuleID([NotNull] VtlParser.RuleIDContext context);
}
} // namespace StatisticsPoland.VtlProcessing.Core.FrontEnd.Antlr
