namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.MiddleEnd.Utilities;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The "Numeric" operator definition.
    /// </summary>
    [OperatorSymbol("ceil", "floor", "abs", "exp", "ln", "sqrt", "mod", "round", "power", "log", "trunc")]
    public class NumericOperator : IOperatorDefinition
    {
        private readonly DataStructureResolver dsResolver;

        /// <summary>
        /// Initialises a new instance of the <see cref="NumericOperator"/> class.
        /// </summary>
        /// <param name="dsResolver">The data structure resolver.</param>
        /// <param name="symbol">The symbol of the operator.</param>
        public NumericOperator(DataStructureResolver dsResolver, string symbol)
        {
            this.dsResolver = dsResolver;
            this.Symbol = symbol;
        }

        public string Name => "Numeric";

        public string Symbol { get; }

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            IExpression expr1 = expression.OperandsCollection.ToArray()[0];
            IExpression expr2 =
                expression.OperandsCollection.ToArray().Length > 1 ?
                expression.OperandsCollection.ToArray()[1] :
                null;

            if (!expr1.Structure.IsNumericStructure(true)) throw new VtlOperatorError(expression, this.Name, "Expected numeric components.");

            IDataStructure ds1 = expr1.Structure.GetCopy();

            if (expr2 != null)
            {
                IDataStructure ds2 = expr2.Structure.GetCopy();

                if (expr2.ExpressionText != "_")
                {
                    if (this.Symbol.In("power", "log", "round", "trunc") && !ds2.IsSingleComponent) throw new VtlOperatorError(expression, this.Name, "Expected single component.");
                    if (this.Symbol.In("log", "round", "trunc") && (!ds2.IsSingleComponent || !ds2.Components.First().ValueDomain.DataType.In(BasicDataType.Integer, BasicDataType.None))) throw new VtlOperatorError(expression, this.Name, "Type of component must be integer.");
                    if (this.Symbol.In("mod", "power") && !ds2.IsNumericStructure(true)) throw new VtlOperatorError(expression, this.Name, "Expected numeric components.");
                }

                if (!ds2.IsSingleComponent)
                {
                    int attributeErrors = 0;
                    if (ds1.IsSupersetOf(ds2, true, false, true)) ds1 = NumericStructure.GetDatasetsMixedStructure(ds1.WithAttributesOf(ds2, attributeErrors, out attributeErrors), ds2);
                    else if (ds2.IsSupersetOf(ds1, true, false, true)) ds1 = NumericStructure.GetDatasetsMixedStructure(ds2.WithAttributesOf(ds1, attributeErrors, out attributeErrors), ds1);
                    else if (ds1.IsSingleComponent) ds1 = NumericStructure.GetDatasetScalarMixedStructure(ds2.WithAttributesOf(ds1, attributeErrors, out attributeErrors), ds1);
                    else throw new VtlOperatorError(expression, this.Name, "Structures of datasets don't match.");

                    VtlOperatorError.ProcessAttributeErrors(attributeErrors, expression, this.Name);
                }
            }

            if (ds1.IsSingleComponent)
            {
                if (this.Symbol.In("ceil", "floor")) return this.dsResolver("int_var", ComponentType.Measure, BasicDataType.Integer);
                return this.dsResolver("num_var", ComponentType.Measure, BasicDataType.Number);
            }
            else
            {
                List<StructureComponent> components = ds1.Measures.ToList();
                for (int i = 0; i < components.Count; i++)
                {
                    if (this.Symbol.In("ceil", "floor")) components[i].ValueDomain = new ValueDomain(BasicDataType.Integer);
                    else components[i].ValueDomain = new ValueDomain(BasicDataType.Number);
                }
            }

            return ds1;
        }
    }
}
