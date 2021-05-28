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
    /// The "Comparison" operator definition.
    /// </summary>
    [OperatorSymbolAttribute("=", "<>", "<", "<=", ">", ">=")]
    public class ComparisonOperator : IOperatorDefinition
    {
        private readonly IJoinApplyMeasuresOperator _joinApplyMeasuresOp;
        private readonly DataStructureResolver _dsResolver;

        /// <summary>
        /// Initialises a new instance of the <see cref="ComparisonOperator"/> class.
        /// </summary>
        /// <param name="joinApplyMeasuresOp">The join apply measure operator.</param>
        /// <param name="dsResolver">The data structure resolver.</param>
        /// <param name="symbol">The symbol of the operator.</param>
        public ComparisonOperator(IJoinApplyMeasuresOperator joinApplyMeasuresOp, DataStructureResolver dsResolver)
        {
            this._joinApplyMeasuresOp = joinApplyMeasuresOp;
            this._dsResolver = dsResolver;
        }

        public string Name => "Comparison";

        public string Symbol { get; set; }

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
            else
            {
                if (expr2.Structure.Measures.Count > 1) throw new VtlOperatorError(expression, this.Name, "Expected structure with only one measure.");
                type2 = expr2.Structure.Measures[0].ValueDomain.DataType;
            }

            if (!type1.EqualsObj(type2) && !BasicDataType.None.In(type1, type2))
                throw new VtlOperatorError(expression, this.Name, "Data types of parameters are not equal.");
                    
            IDataStructure structure;
            IDataStructure ds1 = expr1.Structure.GetCopy();
            IDataStructure ds2 = expr2.Structure.GetCopy();

            if (expr1.IsScalar && expr2.IsScalar) return this._dsResolver("bool_var", ComponentType.Measure, BasicDataType.Boolean);
            if (!expr1.IsScalar && expr2.IsScalar) structure = ds1.WithAttributesOf(ds2);
            else if (expr1.IsScalar && !expr2.IsScalar) structure = ds2.WithAttributesOf(ds1);
            else
            {
                if (ds1.Measures[0].ComponentName != ds2.Measures[0].ComponentName) throw new VtlOperatorError(expression, this.Name, "Datasets doesn't fit.");

                if (ds1.IsSupersetOf(ds2, true, false, true)) structure = ds1.WithAttributesOf(ds2);
                else if (ds2.IsSupersetOf(ds1, true, false, true)) structure = ds2.WithAttributesOf(ds1);
                else throw new VtlOperatorError(expression, this.Name, "Datasets doesn't fit.");
            }

            structure.Measures.Clear();
            structure.Measures.Add(new StructureComponent(BasicDataType.Boolean, "bool_var"));
            structure.Measures[0].BaseComponentName = expr1.Structure.GetCopy().Measures[0].ComponentName;

            return structure;
        }
    }
}
