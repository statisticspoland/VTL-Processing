namespace StatisticsPoland.VtlProcessing.Target.TSQL.Renderers
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using System.Linq;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Target.TSQL.Renderers.Interfaces;

    /// <summary>
    /// The TSQL code renderer for the "Constant" operator.
    /// </summary>
    [OperatorRendererSymbol("const")]
    internal sealed class ConstantOperatorRenderer : IOperatorRenderer
    {
        public string Render(IExpression expr, StructureComponent component)
        {
            BasicDataType type = expr.Structure.Components[0].ValueDomain.DataType;

            if (type == BasicDataType.None) return "NULL";

            if (type == BasicDataType.String)
            {
                string str = expr.ExpressionText;

                if (str.First() == '"' && str.Last() == '"' && str.Length >= 2)
                {
                    str = str.Remove(str.Length - 1).Remove(0,1);
                    str = str.Replace("'", "''");

                    //TODO: zamiana '"'

                    return $"'{str}'";
                }
            }

            if (type.In(BasicDataType.Time, BasicDataType.Date, BasicDataType.TimePeriod, BasicDataType.Duration))
            {
                string str = expr.ExpressionText;

                if (str.Length >= 3 && str[0] == 't' && str[1] == '"' && str.Last() == '"')
                {
                    str = str.Remove(str.Length - 1).Remove(0, 2);
                    str = str.Replace("'", "''");

                    //TODO: zamiana '"'

                    return $"'{str}'";
                }
            }

            if (type == BasicDataType.Boolean)
            {
                if (expr.ExpressionText == "true") return "1";
                return "0";
            }

            return expr.ExpressionText;
        }
    }
}
