﻿namespace StatisticsPoland.VtlProcessing.Target.TSQL.Renderers
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System.Linq;
    using System.Text;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Renderers.Interfaces;

    /// <summary>
    /// The TSQL code renderer for the "Set" operator.
    /// </summary>
    [OperatorRendererSymbol("union", "intersect", "setdiff", "symdiff")]
    internal sealed class SetOperatorRenderer : IOperatorRenderer
    {
        private readonly OperatorRendererResolver _opRendererResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetOperatorRenderer"/> class.
        /// </summary>
        /// <param name="opRendererResolver">The operator renderer resolver.</param>
        public SetOperatorRenderer(OperatorRendererResolver opRendererResolver)
        {
            this._opRendererResolver = opRendererResolver;
        }

        public string Render(IExpression expr, StructureComponent component = null)
        {
            IExpression expr1 = expr.OperandsCollection.ToArray()[0];
            IExpression expr2 = expr.OperandsCollection.ToArray()[1];

            string op1 = $"SELECT * FROM {this._opRendererResolver(expr1.OperatorSymbol).Render(expr1, component)}";
            string op2 = $"SELECT * FROM {this._opRendererResolver(expr2.OperatorSymbol).Render(expr2, component)}";

            StringBuilder result = new StringBuilder();

            if (expr.OperatorSymbol == "union")
            {
                result.AppendLine(op1);
                result.AppendLine("UNION");
                result.AppendLine($"{op1.Replace("*", "ds2.*")} AS ds1");
                result.AppendLine("FULL JOIN (");
                result.AppendLine($"{op2}) AS ds2 ON");
                foreach (StructureComponent identifier in expr1.Structure.Identifiers)
                {
                    result.AppendLine($"ds1.{identifier.ComponentName} = ds2.{identifier.ComponentName} AND");
                }

                result = new StringBuilder(result.ToString().Remove(result.ToString().Length - 5)); // removement of "AND"
                result.AppendLine();
                result.AppendLine($"WHERE ds1.{expr1.Structure.Identifiers[0].ComponentName} IS NULL");
            }
            else if (expr.OperatorSymbol != "symdiff")
            {
                string sqlOperator;
                switch (expr.OperatorSymbol)
                {
                    case "intersect": sqlOperator = "INTERSECT"; break;
                    case "setdiff": sqlOperator = "EXCEPT"; break;
                    default: throw new VtlTargetError(expr, $"Unknow operator keyword: {expr.OperatorSymbol}.");
                }

                result.AppendLine(op1);
                result.AppendLine(sqlOperator);
                result.AppendLine(op2);
            }
            else
            {
                result.AppendLine("SELECT * FROM (");
                result.AppendLine(op1);
                result.AppendLine("EXCEPT");
                result.AppendLine($"{op2}) AS t");
                result.AppendLine("UNION SELECT * FROM (");
                result.AppendLine(op2);
                result.AppendLine("EXCEPT");
                result.AppendLine($"{op1}) AS t");
            }

            return result.ToString();
        }
    }
}
