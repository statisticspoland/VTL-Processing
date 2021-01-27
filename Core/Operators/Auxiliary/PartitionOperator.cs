namespace StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary.ComponentManagement;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;

    /// <summary>
    /// The "Partition" operator definition class.
    /// </summary>
    [OperatorSymbol("partition")]
    public class PartitionOperator : CompCollectorOperator, IOperatorDefinition
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PartitionOperator"/> class.
        /// </summary>
        /// <param name="dsResolver">The data structure resolver.</param>
        public PartitionOperator(DataStructureResolver dsResolver)
            : base(dsResolver, "Partition")
        {
        }

        public string Name => "Partition";

        public string Symbol => "partition";

        public string Keyword { get; set; }
    }
}
