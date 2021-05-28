namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;

    /// <summary>
    /// The "Current date" operator definition.
    /// </summary>
    [OperatorSymbolAttribute("current_date")]
    public class CurrentDateOperator : IOperatorDefinition
    {
        private readonly DataStructureResolver _dsResolver;

        /// <summary>
        /// Initialises a new instance of the <see cref="CurrentDateOperator"/> class.
        /// </summary>
        /// <param name="dsResolver">The data structure resolver.</param>
        public CurrentDateOperator(DataStructureResolver dsResolver)
        {
            this._dsResolver = dsResolver;
        }

        public string Name => "Current date";

        public string Symbol { get; set; } = "current_date";

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            return this._dsResolver("date_var", ComponentType.Measure, BasicDataType.Date);
        }
    }
}
