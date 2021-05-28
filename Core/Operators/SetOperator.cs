namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;

    /// <summary>
    /// The "Set" operator definition.
    /// </summary>
    [OperatorSymbolAttribute("union", "intersect", "setdiff", "symdiff")]
    public class SetOperator : IOperatorDefinition
    {
        public string Name => "Set";

        public string Symbol { get; set; }

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            IDataStructure ds1 = expression.Operands["ds_1"].Structure.GetCopy();
            IDataStructure ds2 = expression.Operands["ds_2"].Structure.GetCopy();

            if (ds1.IsSingleComponent || ds2.IsSingleComponent) throw new VtlOperatorError(expression, this.Name, "Expected datasets as parameters.");
            if (ds1.Components.Count != ds2.Components.Count) throw new VtlOperatorError(expression, this.Name, "Datasets don't fit.");
            for (int i = 0; i < ds1.Components.Count; i++)
            {
                if (ds1.Components[i].ComponentName != ds2.Components[i].ComponentName ||
                    ds1.Components[i].ComponentType != ds2.Components[i].ComponentType ||
                    ds1.Components[i].ValueDomain.DataType != ds2.Components[i].ValueDomain.DataType)
                {
                    throw new VtlOperatorError(expression, this.Name, "Datasets don't fit.");
                }
            }

            return ds1;
        }
    }
}