namespace StatisticsPoland.VtlProcessing.Target.TSQL.Renderers
{
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Renderers.Interfaces;

    /// <summary>
    /// The TSQL code renderer for "Check_datapoint" operator.
    /// </summary>
    [OperatorRendererSymbol("check_datapoint")]
    internal sealed class CheckDatapointOperatorRenderer : IOperatorRenderer
    {
        private readonly OperatorRendererResolver opRendererResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckDatapointOperatorRenderer"/> class.
        /// </summary>
        /// <param name="opRendererResolver">The operator renderer resolver.</param>
        public CheckDatapointOperatorRenderer(OperatorRendererResolver opRendererResolver)
        {
            this.opRendererResolver = opRendererResolver;
        }

        public string Render(IExpression expr, StructureComponent component)
        {
            IRuleset ruleset = expr.ContainingSchema.Rulesets.FirstOrDefault(ruleset => ruleset.Name == expr.Operands["ruleset"].ExpressionText);
            string opSymbol = expr.OperatorDefinition.Keyword;

            if (opSymbol != "all_measures")
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("SELECT");
                sb.Append(this.RenderIdentifiers(expr.Operands["ds_1"]));
                sb.AppendLine("ruleid,");
                
                if (opSymbol == "invalid") sb.Append(this.RenderMeasures(expr.Operands["ds_1"]));
                else sb.AppendLine("bool_var,");
                
                sb.AppendLine("errorcode,");
                sb.AppendLine("errorlevel,");

                if (expr.Structure.ViralAttributes.Count > 0) sb.Append(this.RenderViralAttributes(expr));
                else sb = new StringBuilder(sb.ToString().Remove(sb.ToString().Length - 3)).AppendLine(); // removement of ",\n" 

                sb.AppendLine("FROM (");
                sb.Append(this.RenderRules(expr, ruleset));
                sb.AppendLine(") AS t");

                if (opSymbol == "invalid") sb.AppendLine("WHERE bool_var = 0");

                return sb.ToString();
            }

            return this.RenderRules(expr, ruleset);
        }

        /// <summary>
        /// Renders a rules part of a TSQL translated code.
        /// </summary>
        /// <param name="expr">The expression which parameters shall be used to render.</param>
        /// <param name="ruleset">The ruleset which parameters shall be used to render.</param>
        /// <returns>The TSQL translated code.</returns>
        private string RenderRules(IExpression expr, IRuleset ruleset)
        {
            StringBuilder sb = new StringBuilder();
            IRuleExpression[] rules = ruleset.RulesCollection.ToArray();

            this.ReplaceComponentNames(
                rules,
                ruleset.Variables.Count > 0 ? ruleset.Variables.Keys.ToArray() : ruleset.ValueDomains.Keys.ToArray(),
                expr.Operands["comps"].OperandsCollection.Select(op => op.ExpressionText).ToArray());

            for (int i = 0; i < rules.Length; i++)
            {
                if (i > 0) sb.Append("UNION ALL ");
                sb.AppendLine("SELECT");
                sb.Append(this.RenderIdentifiers(expr.Operands["ds_1"]));

                sb.AppendLine($"'{rules[i].ResultName}' AS ruleid,");

                sb.Append(this.RenderMeasures(expr.Operands["ds_1"]));

                string errorCode = rules[i].ErrorCode?.Replace("'", "''").Replace('"', '\'') ?? "NULL";
                string errorLevel = rules[i].ErrorLevel?.ToString() ?? "NULL";

                if (rules[i].OperatorSymbol == "when")
                {
                    string whenRender = this.opRendererResolver(rules[i].Operands["when"].OperatorSymbol).Render(rules[i].Operands["when"]);
                    string thenRender = this.opRendererResolver(rules[i].Operands["then"].OperatorSymbol).Render(rules[i].Operands["then"]);

                    if (errorCode != "NULL") errorCode = $"IIF({whenRender}, IIF({thenRender}, NULL, {errorCode}), NULL)";
                    if (errorLevel != "NULL") errorLevel = $"IIF({whenRender}, IIF({thenRender}, NULL, {errorLevel}), NULL)";

                    sb.AppendLine($"IIF({whenRender}, IIF({thenRender}, 1, 0), 1) AS bool_var,");
                }
                else
                {
                    string boolRender = this.opRendererResolver(rules[i].OperatorSymbol).Render(rules[i]);

                    if (errorCode != "NULL") errorCode = $"IIF({boolRender}, NULL, {errorCode})";
                    if (errorLevel != "NULL") errorLevel = $"IIF({boolRender}, NULL, {errorLevel})";

                    sb.AppendLine($"IIF({boolRender}, 1, 0) AS bool_var,");
                }

                sb.AppendLine($"{errorCode} AS errorcode,");
                sb.AppendLine($"{errorLevel} AS errorlevel,");

                if (expr.Structure.ViralAttributes.Count > 0) sb.Append(this.RenderViralAttributes(expr));
                else sb = new StringBuilder(sb.ToString().Remove(sb.ToString().Length - 3)).AppendLine(); // removement of ",\n" 

                sb.AppendLine($"FROM {this.opRendererResolver(expr.Operands["ds_1"].OperatorSymbol).Render(expr.Operands["ds_1"])}");
            }

            return sb.ToString();

        }

        /// <summary>
        /// Renders identifiers of a given expression TSQL translated code.
        /// </summary>
        /// <param name="expr">The expression which parameters shall be used to render.</param>
        /// <returns>The TSQL translated code.</returns>
        private string RenderIdentifiers(IExpression expr)
        {
            StringBuilder sb = new StringBuilder();

            foreach (StructureComponent identifier in expr.Structure.Identifiers)
            {
                sb.AppendLine($"{identifier.ComponentName},");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Renders measures of a given expression TSQL translated code.
        /// </summary>
        /// <param name="expr">The expression which parameters shall be used to render.</param>
        /// <returns>The TSQL translated code.</returns>
        private string RenderMeasures(IExpression expr)
        {
            StringBuilder sb = new StringBuilder();

            foreach (StructureComponent measure in expr.Structure.Measures)
            {
                sb.AppendLine($"{measure.ComponentName},");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Renders viral attributes of a given expression TSQL translated code.
        /// </summary>
        /// <param name="expr">The expression which parameters shall be used to render.</param>
        /// <returns>The TSQL translated code.</returns>
        private string RenderViralAttributes(IExpression expr)
        {
            StringBuilder sb = new StringBuilder();

            foreach (StructureComponent attribute in expr.Structure.ViralAttributes)
            {
                sb.AppendLine($"{attribute.ComponentName},");
            }

            sb = new StringBuilder(sb.ToString().Remove(sb.ToString().Length - 3)); // removement of ",\n" 
            sb.AppendLine();
            return sb.ToString();
        }

        /// <summary>
        /// Replaces names of given expressions and their descendants.
        /// </summary>
        /// <param name="exprs">The colection of expressions to replace names.</param>
        /// <param name="oldNames">The old names collection.</param>
        /// <param name="newNames">The new names collection.</param>
        private void ReplaceComponentNames(ICollection<IExpression> exprs, ICollection<string> oldNames, ICollection<string> newNames)
        {
            foreach (IExpression operand in exprs)
            {
                for (int i = 0; i < oldNames.Count; i++)
                {
                    this.ReplaceComponentNames(operand, oldNames.ToArray()[i], newNames.ToArray()[i]);
                }
            }
        }

        /// <summary>
        /// Replaces names of a given expression and its descendants.
        /// </summary>
        /// <param name="expr">The expression.</param>
        /// <param name="oldName">The old name.</param>
        /// <param name="newName">The new name.</param>
        private void ReplaceComponentNames(IExpression expr, string oldName, string newName)
        {
            if (expr.OperatorSymbol == "comp")
            {
                if (expr.ExpressionText == oldName)
                {
                    expr.ExpressionText = newName;
                    expr.Structure.Components[0].ComponentName = newName;
                }
            }
            else
            {
                foreach (IExpression operand in expr.OperandsCollection)
                {
                    this.ReplaceComponentNames(operand, oldName, newName);
                }
            }
        }
    }
}
