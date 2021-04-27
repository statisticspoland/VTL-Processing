namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System.Linq;

    /// <summary>
    /// The "Element of" operator definition.
    /// </summary>
    [OperatorSymbol("in", "not_in")]
    public class InOperator : IOperatorDefinition
    {
        private readonly IJoinApplyMeasuresOperator _joinApplyMeasuresOp;
        private readonly DataStructureResolver _dsResolver;

        /// <summary>
        /// Initialises a new instance of the <see cref="InOperator"/> class.
        /// </summary>
        /// <param name="joinApplyMeasuresOp">The join apply measure operator.</param>
        /// <param name="dsResolver">The data structure resolver.</param>
        /// <param name="symbol">The symbol of the operator.</param>
        public InOperator(IJoinApplyMeasuresOperator joinApplyMeasuresOp, DataStructureResolver dsResolver)
        {
            this._joinApplyMeasuresOp = joinApplyMeasuresOp;
            this._dsResolver = dsResolver;
        }

        public string Name => "(Not) In";

        public string Symbol { get; set; }

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            if (expression.IsApplyComponent) return this._joinApplyMeasuresOp.GetMeasuresStructure(expression);

            IExpression expr1 = expression.OperandsCollection.ToArray()[0];
            IExpression expr2 = expression.OperandsCollection.ToArray()[1];

            if (!expr1.IsScalar && expr1.Structure.Measures.Count > 1) 
                throw new VtlOperatorError(expression, this.Name, "Expected structure with only one measure.");

            if (expr2.OperatorSymbol != "collection")
                throw new VtlOperatorError(expression, this.Name, "Expected collection operator expression as second parameter.");

            if (expr1.IsScalar) return this._dsResolver("bool_var", ComponentType.Measure, BasicDataType.Boolean);

            IDataStructure structure = expr1.Structure.GetCopy();
            structure.Measures[0].ComponentName = "bool_var";
            structure.Measures[0].ValueDomain = new ValueDomain(BasicDataType.Boolean);

            return structure;
        }
    }
}
