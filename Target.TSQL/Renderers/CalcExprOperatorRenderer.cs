namespace Target.TSQL.Renderers
{
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System.Linq;
    using System.Text;
    using Target.TSQL.Infrastructure;
    using Target.TSQL.Infrastructure.Attributes;
    using Target.TSQL.Renderers.Interfaces;

    /// <summary>
    /// TSQL code renderer for the "Calc expression" operator.
    /// </summary>
    [OperatorRendererSymbol("calcExpr")]
    internal sealed class CalcExprOperatorRenderer : IOperatorRenderer
    {
        private readonly OperatorRendererResolver opRendererResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="CalcExprOperatorRenderer"/> class.
        /// </summary>
        /// <param name="opRendererResolver">The operator renderer resolver.</param>
        public CalcExprOperatorRenderer(OperatorRendererResolver opRendererResolver)
        {
            this.opRendererResolver = opRendererResolver;
        }

        public string Render(IExpression expr, StructureComponent component)
        {
            StringBuilder sb = new StringBuilder();
            
            IExpression expr1 = expr.Operands["ds_1"];
            IExpression expr2 = expr.Operands["ds_2"];

            string name = expr1.ExpressionText;

            if (expr.CurrentJoinExpr.Structure.Components.FirstOrDefault(comp => comp.BaseComponentName == name) == null)
                return string.Empty; // Brak komponentu w strukturze wynikowej

            if (expr.CurrentJoinExpr.Operands.ContainsKey("rename"))
            {
                StructureComponent renameComp = expr.CurrentJoinExpr.Operands["rename"].Structure.Components.LastOrDefault(comp => comp.BaseComponentName == name);
                if (renameComp != null) name = renameComp.ComponentName;
            }

            sb.AppendLine($"{this.opRendererResolver(expr2.OperatorSymbol).Render(expr2, component)} AS {name},");
            return sb.ToString();
        }
    }
}
