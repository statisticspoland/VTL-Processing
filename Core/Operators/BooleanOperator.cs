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
    using System.Linq;

    /// <summary>
    /// The "Boolean" operator definition.
    /// </summary>
    [OperatorSymbol("and", "or", "xor", "not")]
    public class BooleanOperator : IOperatorDefinition
    {
        private readonly DataStructureResolver dsResolver;

        /// <summary>
        /// Initialises a new instance of the <see cref="BooleanOperator"/> class.
        /// </summary>
        /// <param name="dsResolver">The data structure resolver.</param>
        /// <param name="symbol">The symbol of the operator.</param>
        public BooleanOperator(DataStructureResolver dsResolver, string symbol)
        {
            this.dsResolver = dsResolver;
            this.Symbol = symbol;
        }

        public string Name => "Boolean";

        public string Symbol { get; }

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            IExpression expr1 = expression.OperandsCollection.ToArray()[0];
            IExpression expr2 =
                expression.OperandsCollection.ToArray().Length > 1 ?
                expression.OperandsCollection.ToArray()[1] :
                null;

            if (this.Symbol.In("and", "or", "xor"))
            {
                if (expr1.IsScalar) this.ValdiateComponent(expression, expr1.Structure.Components[0]);
                else
                {
                    if (expr1.Structure.Measures.Count > 1) throw new VtlOperatorError(expression, this.Name, "Expected structure with only one measure.");
                    this.ValdiateComponent(expression, expr1.Structure.Measures[0]);
                }

                if (expr2.IsScalar) this.ValdiateComponent(expression, expr2.Structure.Components[0]);
                else
                {
                    if (expr2.Structure.Measures.Count > 1) throw new VtlOperatorError(expression, this.Name, "Expected structure with only one measure.");
                    this.ValdiateComponent(expression, expr2.Structure.Measures[0]);
                }

                int attributeErrors = 0;
                IDataStructure structure;
                IDataStructure ds1 = expr1.Structure.GetCopy();
                IDataStructure ds2 = expr2.Structure.GetCopy();

                if (!ds1.IsSingleComponent) ds1.Measures[0].ValueDomain = new ValueDomain(BasicDataType.Boolean);
                if (!ds2.IsSingleComponent) ds2.Measures[0].ValueDomain = new ValueDomain(BasicDataType.Boolean);

                if (expr1.IsScalar && expr2.IsScalar) structure = this.dsResolver("bool_var", ComponentType.Measure, BasicDataType.Boolean);
                else if (!expr1.IsScalar && expr2.IsScalar) structure = ds1.WithAttributesOf(ds2, attributeErrors, out attributeErrors);
                else if (expr1.IsScalar && !expr2.IsScalar) structure = ds2.WithAttributesOf(ds1, attributeErrors, out attributeErrors);
                else
                {
                    if (ds1.IsSupersetOf(ds2, true, false, true)) structure = ds1.WithAttributesOf(ds2, attributeErrors, out attributeErrors);
                    else if (ds2.IsSupersetOf(ds1, true, false, true)) structure = ds2.WithAttributesOf(ds1, attributeErrors, out attributeErrors);
                    else throw new VtlOperatorError(expression, this.Name, "Datasets doesn't fit.");
                }

                VtlOperatorError.ProcessAttributeErrors(attributeErrors, expression, this.Name);
                return structure;
            }
            else if (this.Symbol == "not")
            {
                if (expr1.IsScalar) this.ValdiateComponent(expression, expr1.Structure.Components[0]);
                else
                {
                    if (expr1.Structure.Measures.Count > 1) throw new VtlOperatorError(expression, this.Name, "Expected structure with only one measure.");
                    this.ValdiateComponent(expression, expr1.Structure.Measures[0]);
                }

                IDataStructure ds = expr1.Structure.GetCopy();
                ds.Measures[0].ValueDomain = new ValueDomain(BasicDataType.Boolean);

                return ds;
            }

            throw new VtlOperatorError(expression, this.Name, $"Unknown operator symbol: \"{this.Symbol}\"");
        }

        /// <summary>
        /// Validates if a given component is boolean data type.
        /// </summary>
        /// <param name="exp">The expression source to throw exception.</param>
        /// <param name="component">The component to validate.</param>
        private void ValdiateComponent(IExpression exp, StructureComponent component)
        {
            if (!component.ValueDomain.DataType.In(BasicDataType.Boolean, BasicDataType.None)) throw new VtlOperatorError(exp, this.Name, "Expected boolean value.");
        }
    }
}
