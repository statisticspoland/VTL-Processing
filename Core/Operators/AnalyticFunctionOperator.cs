﻿namespace StatisticsPoland.VtlProcessing.Core.Operators
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
    /// The "Analytic function" operator definition.
    /// </summary>
    [OperatorSymbol("first_value", "last_value", "lag", "rank", "ratio_to_report", "lead")]
    public class AnalyticFunctionOperator : IOperatorDefinition
    {
        private readonly IJoinApplyMeasuresOperator joinApplyMeasuresOp;
        private readonly DataStructureResolver dsResolver;

        /// <summary>
        /// Initialises a new instance of the <see cref="AnalyticFunctionOperator"/> class.
        /// </summary>
        /// <param name="joinApplyMeasuresOp">The join apply measure operator.</param>
        /// <param name="dsResolver">The data structure resolver.</param>
        /// <param name="symbol">The symbol of the operator.</param>
        public AnalyticFunctionOperator(IJoinApplyMeasuresOperator joinApplyMeasuresOp, DataStructureResolver dsResolver, string symbol)
        {
            this.joinApplyMeasuresOp = joinApplyMeasuresOp;
            this.dsResolver = dsResolver;
            this.Symbol = symbol;
        }

        /// <summary>
        /// Gets operator symbols of analytic operators.
        /// </summary>
        public static string[] Symbols => new string[] { "first_value", "last_value", "lag", "rank", "ratio_to_report", "lead" };

        public string Name => "Analytic function";

        public string Symbol { get; private set; }

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            if (expression.IsApplyComponent) return this.joinApplyMeasuresOp.GetMeasuresStructure(expression);

            IExpression operand = 
                expression.Operands.ContainsKey("ds_1") ?
                expression.Operands["ds_1"] :
                null;

            if (this.Symbol == "ratio_to_report")
            {
                foreach (StructureComponent measure in operand.Structure.Measures)
                {
                    if (!measure.ValueDomain.DataType.In(BasicDataType.Number, BasicDataType.Integer)) throw new VtlOperatorError(expression, this.Symbol, "Expected numeric measures.");
                    measure.ValueDomain = new ValueDomain(BasicDataType.Number);
                }
            }

            if (operand == null || operand.IsScalar)
            {
                if (this.Symbol == "rank") return this.dsResolver("int_var", ComponentType.Measure, BasicDataType.Integer);
                
                StructureComponent component = operand.Structure.Components[0];
                return this.dsResolver(component.ComponentName, component.ComponentType, component.ValueDomain.DataType);
            }

            return operand.Structure.GetCopy();
        }
    }
}