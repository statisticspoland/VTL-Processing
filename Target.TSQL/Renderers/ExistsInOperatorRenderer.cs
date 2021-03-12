namespace StatisticsPoland.VtlProcessing.Target.TSQL.Renderers
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.ComponentTools;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System.Linq;
    using System.Text;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Renderers.Interfaces;

    /// <summary>
    /// The TSQL code renderer for the "Exists in" operator.
    /// </summary>
    [OperatorRendererSymbol("exists_in")]
    internal sealed class ExistsInOperatorRenderer : IOperatorRenderer
    {
        private readonly OperatorRendererResolver opRendererResolver;
        private readonly IAttributePropagationAlgorithm propagationAlgorithm;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExistsInOperatorRenderer"/> class.
        /// </summary>
        /// <param name="opRendererResolver">The operator renderer resolver.</param>
        /// <param name="propagationAlgorithm">The attribute propagation algorithm.</param>
        public ExistsInOperatorRenderer(OperatorRendererResolver opRendererResolver, IAttributePropagationAlgorithm propagationAlgorithm)
        {
            this.opRendererResolver = opRendererResolver;
            this.propagationAlgorithm = propagationAlgorithm;
        }

        public string Render(IExpression expr, StructureComponent component)
        {
            IExpression expr1 = expr.Operands["ds_1"];
            IExpression expr2 = expr.Operands["ds_2"];

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT");

            for (int step = 1; step <= 3; step++)
            {
                foreach (StructureComponent identifier in expr.Structure.Identifiers)
                {
                    if (step == 1) sb.AppendLine($"ds1.{identifier.ComponentName} AS {identifier.ComponentName}, ");
                    else sb.AppendLine($"ds1.{identifier.ComponentName} = ds2.{identifier.ComponentName} AND");
                }

                switch (step)
                {
                    case 1:
                        sb = new StringBuilder(sb.ToString().Remove(sb.ToString().Length - 3)); // usunięcie ",\n"
                        sb.AppendLine();
                        sb.Append("IIF(");
                        break;
                    case 2:
                        sb = new StringBuilder(sb.ToString().Remove(sb.ToString().Length - 6)); // usunięcie " AND\n"
                        sb.AppendLine($", 1, 0) AS {expr.Structure.Measures[0].ComponentName},");

                        foreach (StructureComponent attribute in expr.Structure.ViralAttributes)
                        {
                            if (expr1.Structure.ViralAttributes.FirstOrDefault(at => at.ComponentName == attribute.ComponentName) != null &&
                                expr2.Structure.ViralAttributes.FirstOrDefault(at => at.ComponentName == attribute.ComponentName) != null)
                                    sb.AppendLine($"{this.propagationAlgorithm.Propagate(attribute, new string[] { "ds1", "ds2" })} AS {attribute.ComponentName.GetNameWithoutAlias()},");
                        }

                        sb = new StringBuilder(sb.ToString().Remove(sb.ToString().Length - 3)); // usunięcie ",\n"
                        sb.AppendLine();

                        sb.AppendLine($"FROM {this.opRendererResolver(expr1.OperatorSymbol).Render(expr1)} AS ds1");

                        if (expr.OperatorDefinition.Keyword == "true") sb.Append("INNER JOIN ");
                        else sb.Append("LEFT JOIN ");

                        sb.AppendLine($"{this.opRendererResolver(expr2.OperatorSymbol).Render(expr2)} AS ds2");
                        sb.AppendLine("ON");
                        break;
                    case 3:
                        sb = new StringBuilder(sb.ToString().Remove(sb.ToString().Length - 6)); // usunięcie " AND\n"
                        sb.AppendLine();
                        break;
                }           
            }

            return sb.ToString();
        }
    }
}
