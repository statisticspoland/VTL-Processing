namespace StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary.ComponentManagement;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;

    /// <summary>
    /// The "Calc expression" operator definition.
    /// </summary>
    [OperatorSymbolAttribute("calcExpr")]
    public class CalcExprOperator : CompCreateOperator, IOperatorDefinition
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="CalcExprOperator"/> class.
        /// </summary>
        /// <param name="dsResolver">The data structure resolver.</param>
        public CalcExprOperator(DataStructureResolver dsResolver)
            : base(dsResolver, "Calc expression")
        {
        }

        public string Name => "Calc expression";

        public string Symbol { get; set; } = "calcExpr";
    }
}
