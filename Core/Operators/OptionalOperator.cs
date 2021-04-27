namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;

    /// <summary>
    /// The "Optional" operator definition.
    /// </summary>
    [OperatorSymbol("optional")]
    public class OptionalOperator : IOperatorDefinition
    {
        private readonly DataStructureResolver dsResolver;

        /// <summary>
        /// Initialises a new instance of the <see cref="OptionalOperator"/> class.
        /// </summary>
        /// <param name="dsResolver">The data structure resolver.</param>
        public OptionalOperator(DataStructureResolver dsResolver)
        {
            this.dsResolver = dsResolver;
        }

        public string Name => "Optional";

        public string Symbol { get; set; } = "opt";

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            return this.dsResolver(expression.OperatorSymbol, ComponentType.Measure, BasicDataType.None);
        }
    }
}