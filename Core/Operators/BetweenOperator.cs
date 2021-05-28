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
    /// The "Between" operator definition.
    /// </summary>
    [OperatorSymbolAttribute("between")]
    public class BetweenOperator : IOperatorDefinition
    {
        private readonly IJoinApplyMeasuresOperator _joinApplyMeasuresOp;
        private readonly DataStructureResolver _dsResolver;

        /// <summary>
        /// Initialises a new instance of the <see cref="BetweenOperator"/> class.
        /// </summary>
        /// <param name="joinApplyMeasuresOp">The join apply measure operator.</param>
        /// <param name="dsResolver">The data structure resolver.</param>
        public BetweenOperator(IJoinApplyMeasuresOperator joinApplyMeasuresOp, DataStructureResolver dsResolver)
        {
            this._joinApplyMeasuresOp = joinApplyMeasuresOp;
            this._dsResolver = dsResolver;
        }

        public string Name => "Between";

        public string Symbol { get; set; } = "between";

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            if (expression.IsApplyComponent) return this._joinApplyMeasuresOp.GetMeasuresStructure(expression);

            IExpression expr1 = expression.OperandsCollection.ToArray()[0];
            IExpression expr2 = expression.OperandsCollection.ToArray()[1];
            IExpression expr3 = expression.OperandsCollection.ToArray()[2];
            IDataStructure structure;

            if (!expr2.IsScalar) throw new VtlOperatorError(expression, this.Name, "Second parameter must be scalar expression.");
            if (!expr3.IsScalar) throw new VtlOperatorError(expression, this.Name, "Third parameter must be scalar expression.");

            BasicDataType type = expr2.Structure.Components[0].ValueDomain.DataType;
            if (type == BasicDataType.None) type = expr3.Structure.Components[0].ValueDomain.DataType;

            if (!expr3.Structure.Components[0].ValueDomain.DataType.EqualsObj(type) &&
                !BasicDataType.None.In(expr3.Structure.Components[0].ValueDomain.DataType, type))
            {
                throw new VtlOperatorError(expression, this.Name, "Second and third parameter must have the same data types.");
            }

            if (expr1.IsScalar)
            {
                if (!expr1.Structure.Components[0].ValueDomain.DataType.EqualsObj(type) && 
                    !BasicDataType.None.In(expr1.Structure.Components[0].ValueDomain.DataType, type))
                {
                    throw new VtlOperatorError(expression, this.Name, "First parameter must have the same data type as second and third parameter.");
                }

                return this._dsResolver("bool_var", ComponentType.Measure, BasicDataType.Boolean);
            }
            else
            {
                if (expr1.Structure.Measures.Count > 1) throw new VtlOperatorError(expression, this.Name, "Expected structure with only one measure.");
                if (!expr1.Structure.Measures[0].ValueDomain.DataType.EqualsObj(type) &&
                    !BasicDataType.None.In(expr1.Structure.Measures[0].ValueDomain.DataType, type))
                {
                    throw new VtlOperatorError(expression, this.Name, "First parameter must have the same data type as second and third parameter.");
                }
            }

            structure = expr1.Structure.GetCopy();
            structure.Measures[0].ComponentName = "bool_var";
            structure.Measures[0].ValueDomain = new ValueDomain(BasicDataType.Boolean);

            return structure;
        }
    }
}