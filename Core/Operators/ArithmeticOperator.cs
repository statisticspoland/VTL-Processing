namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.MiddleEnd.Utilities;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System.Linq;

    /// <summary>
    /// The "Arithmetic" operator definition.
    /// </summary>
    [OperatorSymbolAttribute("+", "-", "*", "/")]
    public class ArithmeticOperator : IOperatorDefinition
    {
        private readonly IJoinApplyMeasuresOperator _joinApplyMeasuresOp;

        /// <summary>
        /// Initialises a new instance of the <see cref="ArithmeticOperator"/> class.
        /// </summary>
        /// <param name="joinApplyMeasuresOp">The join apply measure operator.</param>
        /// <param name="symbol">The symbol of the operator.</param>
        public ArithmeticOperator(IJoinApplyMeasuresOperator joinApplyMeasuresOp)
        {
            this._joinApplyMeasuresOp = joinApplyMeasuresOp;
        }

        public string Name => "Arithmetic";

        public string Symbol { get; set; }

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            if (expression.IsApplyComponent) return this._joinApplyMeasuresOp.GetMeasuresStructure(expression);

            IDataStructure ds1 = expression.OperandsCollection.ToArray()[0].Structure;
            IDataStructure ds2 = expression.OperandsCollection.ToArray()[1].Structure;

            if (!ds1.IsNumericStructure(true) || !ds2.IsNumericStructure(true)) throw new VtlOperatorError(expression, this.Name, "Expected numeric components.");

            if (!ds1.IsSingleComponent && ds2.IsSingleComponent) return NumericStructure.GetDatasetScalarMixedStructure(ds1, ds2);
            else if (ds1.IsSingleComponent && !ds2.IsSingleComponent) return NumericStructure.GetDatasetScalarMixedStructure(ds2, ds1);
            else if (!ds1.IsSingleComponent && !ds2.IsSingleComponent)
            {
                IDataStructure structure;

                if (ds1.IsSupersetOf(ds2, true, false, true)) structure = NumericStructure.GetDatasetsMixedStructure(ds1.WithAttributesOf(ds2), ds2);
                else if (ds2.IsSupersetOf(ds1, true, false, true)) structure = NumericStructure.GetDatasetsMixedStructure(ds2.WithAttributesOf(ds1), ds1);
                else throw new VtlOperatorError(expression, this.Name, "Structures of datasets don't match.");
                
                return structure;
            }

            if (!ds1.Components[0].ValueDomain.DataType.In(BasicDataType.Integer, BasicDataType.Number, BasicDataType.None) ||
                !ds2.Components[0].ValueDomain.DataType.In(BasicDataType.Integer, BasicDataType.Number, BasicDataType.None)) throw new VtlOperatorError(expression, this.Name, "Expected numeric scalar values.");

            if (ds2.Components[0].ValueDomain.DataType == BasicDataType.Number) return ds2.GetCopy();
            return ds1.GetCopy();
        }
    }
}
