namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary.ComponentManagement;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;

    /// <summary>
    /// The "Calculation of a Component" operator definition.
    /// </summary>
    [OperatorSymbol("calc")]
    public class CalcOperator : CompCollectorOperator, IOperatorDefinition
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="CalcOperator"/> class.
        /// </summary>
        /// <param name="dsResolver">The data structure resolver.</param>
        public CalcOperator(DataStructureResolver dsResolver)
            : base(dsResolver, "Calc")
        {
        }

        public string Name => "Calc";

        public string Symbol => "calc";

        public string Keyword { get; set; }
    }
}
