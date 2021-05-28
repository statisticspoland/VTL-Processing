namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary.ComponentManagement;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;

    /// <summary>
    /// The "Subspace" operator definition.
    /// </summary>
    [OperatorSymbolAttribute("sub")]
    public class SubspaceOperator : CompCollectorOperator, IOperatorDefinition
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="SubspaceOperator"/> class.
        /// </summary>
        /// <param name="dsResolver">The data structure resolver.</param>
        public SubspaceOperator(DataStructureResolver dsResolver)
            : base(dsResolver, "Subspace")
        {
        }

        public string Name => "Subspace";

        public string Symbol { get; set; } = "sub";

        public string Keyword { get; set; }

        public new IDataStructure GetOutputStructure(IExpression expression)
        {
            IDataStructure structure = base.GetOutputStructure(expression);

            foreach (StructureComponent component in structure.Components)
            {
                if (component.ComponentType != ComponentType.Identifier) 
                    throw new VtlOperatorError(expression, this.Name, "Component must be an identifier.");
            }

            return structure;
        }
    }
}
