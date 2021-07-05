namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System.Linq;

    /// <summary>
    /// The "Pivoting" operator definition.
    /// </summary>
    [OperatorSymbolAttribute("pivot")]
    public class PivotOperator : IOperatorDefinition
    {
        private readonly DataStructureResolver _dsResolver;

        /// <summary>
        /// Initialises a new instance of the <see cref="PivotOperator"/> class.
        /// </summary>
        /// <param name="dsResolver">The data structure resolver.</param>
        public PivotOperator(DataStructureResolver dsResolver)
        {
            this._dsResolver = dsResolver;
        }

        public string Name => "Pivot";

        public string Symbol { get; set; } = "pivot";

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            IDataStructure structure = this._dsResolver();

            if (expression.OperandsCollection.Count != 2) throw new VtlOperatorError(expression, this.Name, "Expected two components.");

            IExpression identiefierExpr = expression.OperandsCollection.First();
            IExpression measureExpr = expression.OperandsCollection.Last();

            if (!identiefierExpr.IsScalar || !measureExpr.IsScalar) throw new VtlOperatorError(expression, this.Name, "Expected scalar expression.");

            if (identiefierExpr.Structure.Identifiers.Count() != 1) throw new VtlOperatorError(expression, this.Name, "Expected identifier.");
            if (measureExpr.Structure.Measures.Count() != 1) throw new VtlOperatorError(expression, this.Name, "Expected measure.");

            structure.AddStructure(measureExpr.Structure.GetCopy());
            structure.Measures.First().ComponentName = $"pivot_{identiefierExpr.Structure.Identifiers.First().ComponentName}";

            structure.DatasetType = DatasetType.Pivoted;

            return structure;
        }
    }
}
