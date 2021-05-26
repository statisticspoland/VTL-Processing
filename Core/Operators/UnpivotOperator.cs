namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System.Linq;

    /// <summary>
    /// The "Unpivoting" operator definition.
    /// </summary>
    [OperatorSymbol("unpivot")]
    public class UnpivotOperator : IOperatorDefinition
    {
        private readonly DataStructureResolver _dsResolver;

        /// <summary>
        /// Initialises a new instance of the <see cref="UnpivotOperator"/> class.
        /// </summary>
        /// <param name="dsResolver">The data structure resolver.</param>
        public UnpivotOperator(DataStructureResolver dsResolver)
        {
            this._dsResolver = dsResolver;
        }

        public string Name => "Unpivot";

        public string Symbol { get; set; } = "unpivot";

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            IDataStructure structure = this._dsResolver();

            if (expression.OperandsCollection.Count() != 2) throw new VtlOperatorError(expression, this.Name, "Expected two components.");

            IExpression identiefierExpr = expression.OperandsCollection.First();
            IExpression measureExpr = expression.OperandsCollection.Last();

            if (!identiefierExpr.IsScalar || !measureExpr.IsScalar) throw new VtlOperatorError(expression, this.Name, "Expected scalar expression.");

            if (identiefierExpr.Structure.Identifiers.Count() != 1) throw new VtlOperatorError(expression, this.Name, "Expected identifier.");
            if (measureExpr.Structure.Measures.Count() != 1) throw new VtlOperatorError(expression, this.Name, "Expected measure.");

            if (expression.ParentExpression?.Operands["ds_1"]?.Structure.Measures.GroupBy(g => g.ValueDomain.DataType).Count() != 1)
                throw new VtlOperatorError(expression, this.Name, "All types of Measure must be that same");


            structure.AddStructure(identiefierExpr.Structure.GetCopy());
            structure.AddStructure(measureExpr.Structure.GetCopy());

            return structure;
        }
    }
}
