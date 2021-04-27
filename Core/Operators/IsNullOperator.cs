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
    /// The "Isnull" operator definition.
    /// </summary>
    [OperatorSymbol("isnull")]
    public class IsNullOperator : IOperatorDefinition
    {
        private readonly IJoinApplyMeasuresOperator joinApplyMeasuresOp;
        private readonly DataStructureResolver dsResolver;

        /// <summary>
        /// Initialises a new instance of the <see cref="IsNullOperator"/> class.
        /// </summary>
        /// <param name="joinApplyMeasuresOp">The join apply measure operator.</param>
        /// <param name="dsResolver">The data structure resolver.</param>
        public IsNullOperator(IJoinApplyMeasuresOperator joinApplyMeasuresOp, DataStructureResolver dsResolver)
        {
            this.joinApplyMeasuresOp = joinApplyMeasuresOp;
            this.dsResolver = dsResolver;
        }

        public string Name => "Is null";

        public string Symbol { get; set; } = "isnull";

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            if (expression.IsApplyComponent) return this.joinApplyMeasuresOp.GetMeasuresStructure(expression);

            IExpression expr = expression.OperandsCollection.ToArray()[0];

            if (expr.IsScalar) return this.dsResolver("bool_var", ComponentType.Measure, BasicDataType.Boolean);
            else if (expr.Structure.Measures.Count > 1) throw new VtlOperatorError(expression, this.Name, "Expected structure with only one measure.");

            IDataStructure structure = expr.Structure.GetCopy();
            structure.Measures[0].ComponentName = "bool_var";
            structure.Measures[0].ValueDomain = new ValueDomain(BasicDataType.Boolean);
            structure.Measures[0].BaseComponentName = expr.Structure.GetCopy().Measures[0].BaseComponentName;

            return structure;
        }
    }
}