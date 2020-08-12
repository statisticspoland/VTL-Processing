namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.MiddleEnd.Tools;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System.Linq;

    /// <summary>
    /// The "Arithmetic" operator definition.
    /// </summary>
    [OperatorSymbol("+", "-", "*", "/")]
    public class ArithmeticOperator : IOperatorDefinition
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ArithmeticOperator"/> class.
        /// </summary>
        /// <param name="joinApplyMeasuresOp">The join apply measure operator.</param>
        /// <param name="symbol">The symbol of the operator.</param>
        public ArithmeticOperator(string symbol)
        {
            this.Symbol = symbol;
        }

        public string Name => "Arithmetic";

        public string Symbol { get; }

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            IDataStructure ds1 = expression.OperandsCollection.ToArray()[0].Structure;
            IDataStructure ds2 = expression.OperandsCollection.ToArray()[1].Structure;

            if (!ds1.IsNumericStructure(true) || !ds2.IsNumericStructure(true)) throw new VtlOperatorError(expression, this.Name, "Expected numeric components.");

            if (!ds1.IsSingleComponent && ds2.IsSingleComponent) return NumericStructure.GetDatasetScalarMixedStructure(ds1, ds2);
            else if (ds1.IsSingleComponent && !ds2.IsSingleComponent) return NumericStructure.GetDatasetScalarMixedStructure(ds2, ds1);
            else if (!ds1.IsSingleComponent && !ds2.IsSingleComponent)
            {
                int attributeErrors = 0;
                IDataStructure structure;

                if (ds1.IsSupersetOf(ds2, true, false, true)) structure = NumericStructure.GetDatasetsMixedStructure(ds1.WithAttributesOf(ds2, attributeErrors, out attributeErrors), ds2);
                else if (ds2.IsSupersetOf(ds1, true, false, true)) structure = NumericStructure.GetDatasetsMixedStructure(ds2.WithAttributesOf(ds1, attributeErrors, out attributeErrors), ds1);
                else throw new VtlOperatorError(expression, this.Name, "Structures of datasets don't match.");
                
                VtlOperatorError.ProcessAttributeErrors(attributeErrors, expression, this.Name);
                return structure;
            }

            if (!ds1.Components[0].ValueDomain.DataType.In(BasicDataType.Integer, BasicDataType.Number, BasicDataType.None) ||
                !ds2.Components[0].ValueDomain.DataType.In(BasicDataType.Integer, BasicDataType.Number, BasicDataType.None)) throw new VtlOperatorError(expression, this.Name, "Expected numeric scalar values.");

            if (ds2.Components[0].ValueDomain.DataType == BasicDataType.Number) return ds2.GetCopy();
            return ds1.GetCopy();
        }
    }
}
