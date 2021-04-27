namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System.Linq;

    /// <summary>
    /// The "Match characters" operator definition.
    /// </summary>
    [OperatorSymbol("match_characters")]
    public class MatchCharactersOperator : IOperatorDefinition
    {
        private readonly IJoinApplyMeasuresOperator _joinApplyMeasuresOp;
        private readonly DataStructureResolver _dsResolver;

        /// <summary>
        /// Initialises a new instance of the <see cref="MatchCharactersOperator"/> class.
        /// </summary>
        /// <param name="joinApplyMeasuresOp">The join apply measure operator.</param>
        /// <param name="dsResolver">The data structure resolver.</param>
        public MatchCharactersOperator(IJoinApplyMeasuresOperator joinApplyMeasuresOp, DataStructureResolver dsResolver)
        {
            this._joinApplyMeasuresOp = joinApplyMeasuresOp;
            this._dsResolver = dsResolver;
        }

        public string Name => "Match characters";

        public string Symbol { get; set; } = "match_characters";

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            if (expression.IsApplyComponent) return this._joinApplyMeasuresOp.GetMeasuresStructure(expression);

            IExpression expr1 = expression.OperandsCollection.ToArray()[0];
            IExpression expr2 = expression.OperandsCollection.ToArray()[1];

            BasicDataType type1;
            BasicDataType type2;

            if (expr1.IsScalar) type1 = expr1.Structure.Components[0].ValueDomain.DataType;
            else
            {
                if (expr1.Structure.Measures.Count > 1) throw new VtlOperatorError(expression, this.Name, "Expected structure with only one measure.");
                type1 = expr1.Structure.Measures[0].ValueDomain.DataType;
            }
            if (expr2.IsScalar) type2 = expr2.Structure.Components[0].ValueDomain.DataType;
            else throw new VtlOperatorError(expression, this.Name, "Expected single component as second parameter.");

            if (!type1.In(BasicDataType.String, BasicDataType.None) || !type2.In(BasicDataType.String, BasicDataType.None)) 
                throw new VtlOperatorError(expression, this.Name, "Expected string components.");

            if (expr1.IsScalar) return this._dsResolver("bool_var", ComponentType.Measure, BasicDataType.Boolean);

            IDataStructure structure = expr1.Structure.GetCopy().WithAttributesOf(expr2.Structure);

            structure.Measures[0].ComponentName = "bool_var";
            structure.Measures[0].ValueDomain = new ValueDomain(BasicDataType.Boolean);
            structure.Measures[0].BaseComponentName = expr1.Structure.GetCopy().Measures[0].ComponentName;

            return structure;
        }
    }
}