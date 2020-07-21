namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System;

    /// <summary>
    /// Membership operator definition class.
    /// </summary>
    [OperatorSymbol("#")]
    public class MembershipOperator : IOperatorDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MembershipOperator"/> class.
        /// </summary>
        public MembershipOperator()
        {
            this.Keyword = "Standard";
        }

        public string Name => "Membership";

        public string Symbol => "#";

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            throw new NotImplementedException();
        }
    }
}
