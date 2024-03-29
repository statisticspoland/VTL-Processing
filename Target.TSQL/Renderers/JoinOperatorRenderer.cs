﻿namespace StatisticsPoland.VtlProcessing.Target.TSQL.Renderers
{
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System.Linq;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Renderers.Interfaces;

    /// <summary>
    /// The TSQL code renderer for the "Join" operator.
    /// </summary>
    [OperatorRendererSymbol("join")]
    internal sealed class JoinOperatorRenderer : IOperatorRenderer
    {
        private readonly IJoinSelectBuilder _joinSelectBuilder;
        private readonly OperatorRendererResolver _opRendererResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="JoinOperatorRenderer"/> class.
        /// </summary>
        /// <param name="joinSelectBuilder">The TSQL select query builder.</param>
        /// <param name="opRendererResolver">The operator renderer resolver.</param>
        public JoinOperatorRenderer(IJoinSelectBuilder joinSelectBuilder, OperatorRendererResolver opRendererResolver)
        {
            this._joinSelectBuilder = joinSelectBuilder;
            this._opRendererResolver = opRendererResolver;
        }

        public string Render(IExpression expr, StructureComponent component = null)
        {
            return this._joinSelectBuilder
                .AddJoinExpr((IJoinExpression)(expr))
                .AddIdentifiers()
                .AddMeasures(this.RenderApplyMeasures)
                .AddViralAttributes()
                .AddSource()
                .AddFilters()
                .AddGroupingClause()
                .AddHavingClause()
                .Build();
        }

        /// <summary>
        /// Visits an "apply" branch of a "join" operator expression and renders measures for the TSQL join select query.
        /// </summary>
        /// <param name="applyBranch">The "apply" branch of a "join" operator which parameters shall be used to render.</param>
        /// <param name="measure">The selected measure with the old name to assign in the translated code.</param>
        /// <param name="renamedMeasure">The selected measure with the new name to assign in the translated code.</param>
        /// <returns>The TSQL translated code with measures.</returns>
        private string RenderApplyMeasures(IExpression applyBranch, StructureComponent measure, StructureComponent renamedMeasure)
        {
            string result = this._opRendererResolver(applyBranch.OperatorSymbol).Render(applyBranch, measure);

            string leftParenthesis = string.Empty;
            string rightParenthesis = string.Empty;

            if (applyBranch.ExpressionText.First() == '(' && applyBranch.ExpressionText.Last() == ')')
            {
                leftParenthesis = "(";
                rightParenthesis = ")";
            }

            result = $"{leftParenthesis}{result}{rightParenthesis},";
            if (result.Split(',')[0].Length != 2 || result.Split(',')[0].Split('.')[1] != renamedMeasure.ComponentName)
            {
                result = result.Remove(result.Length - 1); // removement of ","
                if (applyBranch.CurrentJoinExpr.Operands.ContainsKey("over")) result += this._opRendererResolver("over").Render(applyBranch.CurrentJoinExpr.Operands["over"]);
                result += $" AS {renamedMeasure.ComponentName},";
            }

            return result;
        }
    }
}