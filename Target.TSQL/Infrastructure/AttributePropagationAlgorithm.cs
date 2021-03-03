namespace StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.ComponentTools;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using System.Collections.Generic;
    using System.Text;
    using Infrastructure.Interfaces;

    /// <summary>
    /// The standard VTL 2.0 attribute propagation algorithm representation.
    /// </summary>
    public class AttributePropagationAlgorithm : IAttributePropagationAlgorithm
    {
        public string Propagate(StructureComponent attribute, ICollection<string> aliases)
        {    
            string attrName = attribute.ComponentName.GetNameWithoutAlias();
            string result;
            StringBuilder sb = new StringBuilder();
            switch (attribute.ValueDomain.DataType)
            {
                case BasicDataType.Boolean:
                    int endCount = 0;
                    foreach (string alias in aliases)
                    {
                        sb.AppendLine($"IIF({alias}.{attrName} = 1, 1,"); // nesting "If Then Else" 
                        endCount++;
                    }

                    result = $"{sb.ToString().Remove(sb.Length - 2)} 0"; // the last "else"
                    for (int i = 0; i < endCount; i++)
                    {
                        result += ")"; // closing of the nest
                    }

                    result += "";
                    break;
                case BasicDataType.Integer:
                case BasicDataType.Number:
                case BasicDataType.String:
                    foreach (string alias in aliases)
                    {
                        sb.AppendLine($"SELECT {alias}.{attrName} AS VALUE UNION ");
                    }

                    string function = "MAX";
                    if (attribute.ValueDomain.DataType == BasicDataType.String) function = "MIN";

                    result = $"(SELECT {function}(VALUE) FROM\n({sb.ToString().Remove(sb.Length - 9)}) AS t)"; // remove " UNION "
                    break;
                default: throw new VtlTargetError(null, $"Unknow data type: {attribute.ValueDomain.DataType}.");
            }

            return result;
        }
    }
}
