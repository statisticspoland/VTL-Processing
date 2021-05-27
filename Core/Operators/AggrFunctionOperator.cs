namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;

    /// <summary>
    /// The "Aggregation function" operator definition.
    /// </summary>
    [OperatorSymbolAttribute("count", "min", "max", "median", "sum", "avg", "stddev_pop", "stddev_samp", "var_pop", "var_samp")]
    public class AggrFunctionOperator : IOperatorDefinition
    {
        private readonly IJoinApplyMeasuresOperator _joinApplyMeasuresOp;
        private readonly DataStructureResolver _dsResolver;

        /// <summary>
        /// Initialises a new instance of the <see cref="AggrFunctionOperator"/> class.
        /// </summary>
        /// <param name="joinApplyMeasuresOp">The join apply measure operator.</param>
        /// <param name="dsResolver">The data structure resolver.</param>
        /// <param name="symbol">The symbol of the operator.</param>
        public AggrFunctionOperator(IJoinApplyMeasuresOperator joinApplyMeasuresOp, DataStructureResolver dsResolver)
        {
            this._joinApplyMeasuresOp = joinApplyMeasuresOp;
            this._dsResolver = dsResolver;
        }

        /// <summary>
        /// Gets operator symbols using a dataset clause operator.
        /// </summary>
        public static string[] Symbols => new string[] { "count", "min", "max", "median", "sum", "avg", "stddev_pop", "stddev_samp", "var_pop", "var_samp" };

        public string Name => "Aggregation function";

        public string Symbol { get; set; }

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            if (expression.IsApplyComponent) return this._joinApplyMeasuresOp.GetMeasuresStructure(expression);

            if (expression.OperandsCollection.Count == 0)
            {
                if (this.Symbol == "count") return this._dsResolver("int_var", ComponentType.Measure, BasicDataType.Integer);
                throw new VtlOperatorError(expression, this.Name, "Expected any function argument.");
            }

            IExpression operand = expression.Operands["ds_1"];

            if (operand.IsScalar)
            {
                StructureComponent component = operand.Structure.Components[0];
                if (this.Symbol != "count" && !component.ValueDomain.DataType.In(BasicDataType.Integer, BasicDataType.Number, BasicDataType.None))
                    throw new VtlOperatorError(expression, this.Name, "Expected numeric component.");

                if (this.Symbol.In("count", "sum")) return this._dsResolver(component.ComponentName, component.ComponentType, BasicDataType.Integer);
                else if (this.Symbol.In("min", "max")) return this._dsResolver(component.ComponentName, component.ComponentType, component.ValueDomain.DataType);
                return this._dsResolver(component.ComponentName, component.ComponentType, BasicDataType.Number);
            }

            IDataStructure structure = operand.Structure.GetCopy();
            if (this.Symbol == "count")
            {
                structure.Measures.Clear();
                structure.Measures.Add(new StructureComponent(BasicDataType.Integer, "int_var"));
            }
            else
            {
                foreach (StructureComponent measure in structure.Measures)
                {
                    if (!measure.ValueDomain.DataType.In(BasicDataType.Integer, BasicDataType.Number, BasicDataType.None))
                        throw new VtlOperatorError(expression, this.Name, "Expected numeric measures.");

                    if (this.Symbol != "sum") measure.ValueDomain = new ValueDomain(BasicDataType.Number);
                }
            }

            return structure;
        }
    }
}
