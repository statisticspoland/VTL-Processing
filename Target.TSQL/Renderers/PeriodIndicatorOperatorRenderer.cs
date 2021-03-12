namespace StatisticsPoland.VtlProcessing.Target.TSQL.Renderers
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.ComponentTools;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using System.Linq;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Renderers.Interfaces;

    /// <summary>
    /// The TSQL code renderer for the "Period indicator" operator.
    /// </summary>
    [OperatorRendererSymbol("period_indicator")]
    internal sealed class PeriodIndicatorOperatorRenderer : IOperatorRenderer
    {
        private readonly OperatorRendererResolver opRendererResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="PeriodIndicatorOperatorRenderer"/> class.
        /// </summary>
        /// <param name="opRendererResolver">The operator renderer resolver.</param>
        public PeriodIndicatorOperatorRenderer(OperatorRendererResolver opRendererResolver)
        {
            this.opRendererResolver = opRendererResolver;
        }

        public string Render(IExpression expr, StructureComponent component)
        {
            if (!expr.IsScalar && component == null) return this.opRendererResolver("overall").Render(expr, component);

            IExpression operand = expr.OperandsCollection.First();
            string compName = string.Empty;

            if (expr.CurrentJoinExpr == null || operand.OperatorSymbol == "#")
                compName = this.opRendererResolver(operand.OperatorSymbol).Render(operand, component);
            else
            {
                compName = operand.Structure.Components.FirstOrDefault(id => id.ValueDomain.DataType == BasicDataType.TimePeriod).ComponentName.GetNameWithoutAlias();
                compName = $"{expr.CurrentJoinExpr.GetAliasesSignatures(compName)[0]}.{compName}";
            }

            return $"(IIF(LEN({compName}) = 4, 'A', SUBSTRING({compName}, 5, 1)))";
        }
    }
}
