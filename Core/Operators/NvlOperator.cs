namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System.Linq;

    /// <summary>
    /// The "Nvl" operator definition.
    /// </summary>
    [OperatorSymbol("nvl")]
    public class NvlOperator : IOperatorDefinition
    {
        private readonly IJoinApplyMeasuresOperator joinApplyMeasuresOp;

        /// <summary>
        /// Initialises a new instance of the <see cref="NvlOperator"/> class.
        /// </summary>
        /// <param name="joinApplyMeasuresOp">The join apply measure operator.</param>
        public NvlOperator(IJoinApplyMeasuresOperator joinApplyMeasuresOp)
        {
            this.joinApplyMeasuresOp = joinApplyMeasuresOp;
        }

        public string Name => "Nvl";

        public string Symbol { get; set; } = "nvl";

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            if (expression.IsApplyComponent) return this.joinApplyMeasuresOp.GetMeasuresStructure(expression);

            IDataStructure structure;
            IDataStructure ds1 = expression.OperandsCollection.ToArray()[0].Structure.GetCopy();
            IDataStructure ds2 = expression.OperandsCollection.ToArray()[1].Structure.GetCopy();

            if (!ds1.IsSingleComponent && ds1.Measures.Count > 1) throw new VtlOperatorError(expression, this.Name, "Expected single measure structure.");
            if (!ds2.IsSingleComponent && ds2.Measures.Count > 1) throw new VtlOperatorError(expression, this.Name, "Expected single measure structure.");
            if (!ds1.IsSingleComponent && !ds2.IsSingleComponent)
            {
                if (!ds1.IsSupersetOf(ds2, true, false, true) && !ds2.IsSupersetOf(ds1, true, false, true)) throw new VtlOperatorError(expression, this.Name, "Datasets doesn't fit.");

                if (!ds1.Measures[0].ValueDomain.DataType.In(BasicDataType.None, BasicDataType.Integer) || ds2.Measures[0].ValueDomain.DataType == BasicDataType.None)
                    structure =  ds1.WithAttributesOf(ds2);
                else structure = ds2.WithAttributesOf(ds1);

                return structure;
            }

            if (!ds1.IsSingleComponent || !ds2.IsSingleComponent)
            {
                structure = ds1.GetCopy().WithAttributesOf(ds2);
                BasicDataType scalarType = ds2.Components[0].ValueDomain.DataType;
                if (!ds2.IsSingleComponent)
                {
                    structure = ds2.GetCopy().WithAttributesOf(ds1);
                    scalarType = ds1.Components[0].ValueDomain.DataType;
                }

                if (!structure.Measures[0].ValueDomain.DataType.EqualsObj(scalarType))
                    throw new VtlOperatorError(expression, this.Name, "Data types of parameters are not equal.");

                if (structure.Measures[0].ValueDomain.DataType.In(BasicDataType.None, BasicDataType.Integer) && scalarType != BasicDataType.None)
                    structure.Measures[0].ValueDomain = new ValueDomain(scalarType);
                return structure;
            }

            if (!ds1.Components[0].ValueDomain.DataType.EqualsObj(ds2.Components[0].ValueDomain.DataType))
                throw new VtlOperatorError(expression, this.Name, "Data types of parameters are not equal.");

            if (!ds1.Components[0].ValueDomain.DataType.In(BasicDataType.None, BasicDataType.Integer) || ds2.Components[0].ValueDomain.DataType == BasicDataType.None)
                structure = ds1.WithAttributesOf(ds2);
            else structure = ds2.WithAttributesOf(ds1);

            return structure;
        }
    }
}