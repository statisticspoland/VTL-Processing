namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System;
    using System.Linq;

    /// <summary>
    /// The "Time" operator definition.
    /// </summary>
    [OperatorSymbol("fill_time_series", "flow_to_stock", "stock_to_flow", "timeshift", "time_agg")]
    public class TimeOperator : IOperatorDefinition
    {
        public string Name => "Time";

        public string Symbol { get; set; }

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            if (this.Symbol == "time_agg") throw new NotImplementedException("Operator time_agg is not supported.");

            IExpression datasetExpr = expression.OperandsCollection.ToArray()[0];

            if (!datasetExpr.IsScalar)
            {
                StructureComponent[] timeIdentifiers = datasetExpr.Structure.Identifiers.Where(id => id.ValueDomain.DataType.In(BasicDataType.Time, BasicDataType.Date, BasicDataType.TimePeriod)).ToArray();

                if (timeIdentifiers.Length == 0)
                    throw new VtlOperatorError(expression, this.Name, "Identifier of time data type has been not found.");
                if (timeIdentifiers.Length > 1)
                    throw new VtlOperatorError(expression, this.Name, "Found more than 1 identifier of time data type.");
            }

            return datasetExpr.Structure.GetCopy();
        }
    }
}
