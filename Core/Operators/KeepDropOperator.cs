namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System.Linq;

    /// <summary>
    /// The "Maintaining/Removing Components" operator definition.
    /// </summary>
    [OperatorSymbolAttribute("keep", "drop")]
    public class KeepDropOperator : IOperatorDefinition
    {
        private readonly DataStructureResolver _dsResolver;

        /// <summary>
        /// Initialises a new instance of the <see cref="KeepDropOperator"/> class.
        /// </summary>
        /// <param name="dsResolver">The data structure resolver.</param>
        /// <param name="symbol">The symbol of the operator.</param>
        public KeepDropOperator(DataStructureResolver dsResolver)
        {
            this._dsResolver = dsResolver;
        }

        public string Name => "Keep / Drop";

        public string Symbol { get; set; }

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            IDataStructure structure = this._dsResolver();
            foreach (IExpression expr in expression.OperandsCollection)
            {
                if (!expr.IsScalar) throw new VtlOperatorError(expression, this.Name, "Expected scalar expression.");
                if (expr.OperatorSymbol == "#")
                {
                    string compName = $"{expr.OperandsCollection.ToArray()[0].ExpressionText}#{expr.Operands["ds_2"].ExpressionText}";
                    expr.Structure.Components[0].BaseComponentName = compName;
                    expr.Structure.Components[0].ComponentName = compName;
                }

                structure.AddStructure(expr.Structure);
            }

            if (structure.Identifiers.Count != 0) throw new VtlOperatorError(expression, this.Name, $"Identifiers cannot be included to {this.Symbol.ToLower()} branch of the \"join\" operator expression.");

            return structure;
        }
    }
}
