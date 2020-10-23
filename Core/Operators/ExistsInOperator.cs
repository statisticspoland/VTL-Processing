namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System.Linq;

    /// <summary>
    /// The "Exists in" operator definition.
    /// </summary>
    [OperatorSymbol("exists_in")]
    public class ExistsInOperator : IOperatorDefinition
    {
        private readonly DataStructureResolver dsResolver;

        /// <summary>
        /// Initialises a new instance of the <see cref="ExistsInOperator"/> class.
        /// </summary>
        /// <param name="dsResolver">The data structure resolver.</param>
        public ExistsInOperator(DataStructureResolver dsResolver)
        {
            this.dsResolver = dsResolver;
        }

        public string Name => "Exists in";

        public string Symbol => "exists_in";

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            IExpression expr1 = expression.OperandsCollection.ToArray()[0];
            IExpression expr2 = expression.OperandsCollection.ToArray()[1];

            if (expr1.IsScalar || expr2.IsScalar) throw new VtlOperatorError(expression, this.Name, "Expeceted 2 datasets.");

            if (expr1.Structure.Identifiers.Count > expr2.Structure.Identifiers.Count)
                throw new VtlOperatorError(expression, this.Name, "Second dataset's identifiers number must be greater or equal to identifiers number of first dataset's.");

            if (expression.CurrentJoinExpr != null) return this.dsResolver("bool_var", ComponentType.Measure, BasicDataType.Boolean);

            IDataStructure structure = expr1.Structure.GetCopy().WithAttributesOf(expr2.Structure);
            StructureComponent measure = structure.Measures[0];

            structure.Measures.Clear();
            structure.Measures.Add(measure);
            structure.Measures[0].ComponentName = "bool_var";
            structure.Measures[0].ValueDomain = new ValueDomain(BasicDataType.Boolean);
            structure.Measures[0].BaseComponentName = expr1.Structure.GetCopy().Measures[0].BaseComponentName;

            return structure;
        }
    }
}