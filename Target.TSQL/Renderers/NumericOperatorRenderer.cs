namespace StatisticsPoland.VtlProcessing.Target.TSQL.Renderers
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System;
    using System.Linq;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Renderers.Interfaces;

    /// <summary>
    /// The TSQL code renderer for "Numeric" operator.
    /// </summary>
    [OperatorRendererSymbol("ceil", "floor", "abs", "exp", "ln", "sqrt", "mod", "round", "power", "log", "trunc")]
    internal sealed class NumericOperatorRenderer : IOperatorRenderer
    {
        private readonly OperatorRendererResolver _opRendererResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericOperatorRenderer"/> class.
        /// </summary>
        /// <param name="opRendererResolver">The operator renderer resolver.</param>
        public NumericOperatorRenderer(OperatorRendererResolver opRendererResolver)
        {
            this._opRendererResolver = opRendererResolver;
        }

        public string Render(IExpression expr, StructureComponent component = null)
        {
            if (!expr.IsScalar && component == null) return this._opRendererResolver("overall").Render(expr, component);

            IExpression expr1 = expr.OperandsCollection.ToArray()[0];
            IExpression expr2 = expr.OperandsCollection.ToArray().Length > 1 ?
                expr.OperandsCollection.ToArray()[1] :
                null;

            string symbol = expr.OperatorSymbol.ToUpper();

            string arg1 = this._opRendererResolver(expr1.OperatorSymbol).Render(expr1, component);
            string arg2 = expr2 != null ?
                this._opRendererResolver(expr2.OperatorSymbol).Render(expr2, component) :
                null;

            if (symbol == "CEIL") return $"CEILING({arg1})";
            if (symbol == "FLOOR") return $"FLOOR({arg1})";
            if (symbol == "ABS") return $"ABS({arg1})";
            if (symbol == "EXP") return $"EXP({arg1})";
            if (symbol == "LN") return $"LOG({arg1}, EXP(1))";
            if (symbol == "LOG") return $"LOG({arg1}, {arg2})";
            if (symbol == "SQRT") return $"SQRT({arg1})";
            if (symbol == "MOD") return arg2 != "0" ? $"{arg1} % {arg2}" : arg1; // 0 given by reference not supported
            if (symbol == "POWER") return $"POWER({arg1}, {arg2})";
            if (symbol == "ROUND") return string.Format("ROUND({0}, {1})", arg1, arg2.In("_", null) ? "0" : arg2);
            if (symbol == "TRUNC") throw new VtlTargetError(expr, $"[{symbol}] is not supported in T-SQL "); //TODO

            throw new ArgumentException("expr", $"Unknown operator symbol: {symbol}");
        }
    }
}
