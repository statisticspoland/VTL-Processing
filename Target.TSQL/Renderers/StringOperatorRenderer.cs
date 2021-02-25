namespace Target.TSQL.Renderers
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System;
    using System.Linq;
    using Target.TSQL.Infrastructure;
    using Target.TSQL.Infrastructure.Attributes;
    using Target.TSQL.Renderers.Interfaces;

    /// <summary>
    /// The TSQL code renderer for the "String" operator.
    /// </summary>
    [OperatorRendererSymbol("||", "trim", "rtrim", "ltrim", "upper", "lower", "length", "substr", "replace", "instr")]
    internal sealed class StringOperatorRenderer : IOperatorRenderer
    {
        private readonly OperatorRendererResolver opRendererResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringOperatorRenderer"/> class.
        /// </summary>
        /// <param name="opRendererResolver">The operator renderer resolver.</param>
        public StringOperatorRenderer(OperatorRendererResolver opRendererResolver)
        {
            this.opRendererResolver = opRendererResolver;
        }

        public string Render(IExpression expr, StructureComponent component)
        {
            if (!expr.IsScalar && component == null) return this.opRendererResolver("overall").Render(expr, component);

            IExpression expr1 = expr.OperandsCollection.ToArray()[0];
            IExpression expr2 = expr.OperandsCollection.Count > 1 ?
                expr.OperandsCollection.ToArray()[1] :
                null;
            IExpression expr3 = expr.OperandsCollection.Count > 2 ?
                expr.OperandsCollection.ToArray()[2] :
                null;
            IExpression expr4 = expr.OperandsCollection.Count > 3 ?
                expr.OperandsCollection.ToArray()[3] :
                null;

            string symbol = expr.OperatorSymbol.ToUpper();

            string arg1 = this.opRendererResolver(expr1.OperatorSymbol).Render(expr1, component);
            string arg2 = expr2 != null ?
                this.opRendererResolver(expr2.OperatorSymbol).Render(expr2, component) :
                null;
            string arg3 = expr3 != null ?
                this.opRendererResolver(expr3.OperatorSymbol).Render(expr3, component) :
                null;
            string arg4 = expr4 != null ?
                this.opRendererResolver(expr4.OperatorSymbol).Render(expr4, component) :
                null;

            if (symbol == "||") return $"CONCAT({arg1}, {arg2})";
            if (symbol == "TRIM") return $"LTRIM(RTRIM({arg1}))";
            if (symbol == "RTRIM") return $"RTRIM({arg1})";
            if (symbol == "LTRIM") return $"LTRIM({arg1})";
            if (symbol == "UPPER") return $"UPPER({arg1})";
            if (symbol == "LOWER") return $"LOWER({arg1})";
            if (symbol == "LENGTH") return $"LEN({arg1})";
            if (symbol == "SUBSTR")
                return string.Format("SUBSTRING({0}{1}{2})",
                    arg1,
                    arg2.In("_", null) ? ", 1" : $", {arg2}",
                    arg3.In("_", null) ? ", 2147483647" : $", {arg3}");

            if (symbol == "REPLACE")
                return string.Format("REPLACE({0}, {1}{2})",
                    arg1,
                    arg2,
                    arg3.In("_", null) ? ", ''" : $", {arg3}");

            if (symbol == "INSTR")
            {
                if (arg4 != null && arg4 != "_") throw new Exception($"Parameter 'occurrence' (arg4) is unsupported in T-SQL : {symbol}"); //TODO
                return string.Format("CHARINDEX({0},{1}{2})",
                    arg2,
                    arg1,
                    arg3.In("_", null) ? ", 1" : $", {arg3}");
            }
                
            throw new Exception($"Unknown operator symbol: {symbol}");
        }
    }
}
