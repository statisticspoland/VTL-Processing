namespace StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;

    /// <summary>
    /// The "Collection" operator definition.
    /// </summary>
    [OperatorSymbol("collection")]
    public class CollectionOperator : IOperatorDefinition
    {
        private readonly DataStructureResolver dsResolver;

        /// <summary>
        /// Initialises a new instance of the <see cref="CollectionOperator"/> class.
        /// </summary>
        /// <param name="dsResolver">The data structure resolver.</param>
        public CollectionOperator(DataStructureResolver dsResolver)
        {
            this.dsResolver = dsResolver;
        }

        public string Name => "Collection";

        public string Symbol { get; set; } = "collection";

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            IDataStructure dataStructure = this.dsResolver();
            foreach (IExpression expr in expression.OperandsCollection)
            {
                StructureComponent currentComp = expr.Structure.GetCopy().Components[0];

                if (!expr.IsScalar) throw new VtlOperatorError(expression, this.Name, "Expected scalar expression.");
                if (expression.ParentExpression == null || !expression.ParentExpression.OperatorSymbol.In("check_datapoint", "check_hierarchy", "check"))
                {
                    foreach (StructureComponent component in dataStructure.Components)
                    {
                        if (!currentComp.ValueDomain.DataType.EqualsObj(component.ValueDomain.DataType))
                            throw new VtlOperatorError(expression, this.Name, "Data types of all items of collection must be the same.");
                    }
                }

                dataStructure.Measures.Add(currentComp);
            }

            return dataStructure;
        }
    }
}
