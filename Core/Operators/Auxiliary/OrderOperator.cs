namespace StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary.ComponentManagement;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;

    /// <summary>
    /// The "Order" operator definition.
    /// </summary>
    [OperatorSymbolAttribute("order")]
    public class OrderOperator : CompCollectorOperator, IOperatorDefinition
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="OrderOperator"/> class.
        /// </summary>
        /// <param name="dsResolver">The data structure resolver.</param>
        public OrderOperator(DataStructureResolver dsResolver)
            : base(dsResolver, "Order")
        {
        }

        public string Name => "Order";

        public string Symbol { get; set; } = "order";

        public string Keyword { get; set; }
    }
}
