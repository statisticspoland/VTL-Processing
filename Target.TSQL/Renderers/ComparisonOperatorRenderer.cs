﻿namespace StatisticsPoland.VtlProcessing.Target.TSQL.Renderers
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using System.Linq;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Renderers.Interfaces;

    /// <summary>
    /// The TSQL code renderer for the "Comparison" operator.
    /// </summary>
    [OperatorRendererSymbol("=", "<>", "<", "<=", ">", ">=")]
    internal sealed class ComparisonOperatorRenderer :  IOperatorRenderer
    {
        private readonly OperatorRendererResolver _opRendererResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparisonOperatorRenderer"/> class.
        /// </summary>
        /// <param name="opRendererResolver">The operator renderer resolver.</param>
        public ComparisonOperatorRenderer(OperatorRendererResolver opRendererResolver)
        {
            this._opRendererResolver = opRendererResolver;
        }

        public string Render(IExpression expr, StructureComponent component = null)
        {
            if (!expr.IsScalar && !expr.IsApplyComponent && component == null) return this._opRendererResolver("overall").Render(expr, component);

            IExpression expr1 = expr.OperandsCollection.ToArray()[0];
            IExpression expr2 = expr.OperandsCollection.ToArray()[1];

            string op1 = this._opRendererResolver(expr1.OperatorSymbol).Render(expr1, !expr.IsApplyComponent ? component : expr1.Structure.Measures[0]);
            string op2 = this._opRendererResolver(expr2.OperatorSymbol).Render(expr2, !expr.IsApplyComponent ? component : expr2.Structure.Measures[0]);

            string result = string.Empty;
            string symbol = expr.OperatorSymbol;

            if (symbol == "<>") symbol = "!=";

            result = $"{op1} {symbol} {op2}";
            if (expr1.Structure.Components[0].ValueDomain.DataType == BasicDataType.String && expr2.Structure.Components[0].ValueDomain.DataType == BasicDataType.String)
                result += " COLLATE Latin1_General_BIN"; // TODO

            if (!expr.ParamSignature.In("filter", "having") &&
                (expr.ParentExpression == null || (!expr.ParentExpression.OperatorSymbol.In("and", "or", "xor", "not") && !expr.ParentExpression.ParamSignature.In("if", "subspace"))) &&
                expr.ContainingSchema.Rulesets.FirstOrDefault(ruleset => ruleset.RulesCollection.Contains(expr.GetFirstAncestorExpr() ?? expr)) == null)
            {
                result = $"IIF({op1} IS NULL OR {op2} IS NULL, NULL,\nIIF({result}, 1, 0))";
            }

            return result;
        }
    }
}
