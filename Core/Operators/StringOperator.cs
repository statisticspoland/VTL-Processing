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
    /// The "String" operator definition.
    /// </summary>
    [OperatorSymbol("||", "trim", "rtrim", "ltrim", "upper", "lower", "substr", "replace", "instr", "length")]
    public class StringOperator : IOperatorDefinition
    {
        private readonly IJoinApplyMeasuresOperator joinApplyMeasuresOp;
        private readonly DataStructureResolver dsResolver;

        /// <summary>
        /// Initialises a new instance of the <see cref="StringOperator"/> class.
        /// </summary>
        /// <param name="joinApplyMeasuresOp">The join apply measure operator.</param>
        /// <param name="dsResolver">The data structure resolver.</param>
        /// <param name="symbol">The symbol of the operator.</param>
        public StringOperator(IJoinApplyMeasuresOperator joinApplyMeasuresOp, DataStructureResolver dsResolver, string symbol)
        {
            this.joinApplyMeasuresOp = joinApplyMeasuresOp;
            this.dsResolver = dsResolver;
            this.Symbol = symbol;
        }

        public string Name => "String";

        public string Symbol { get; }

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            if (expression.IsApplyComponent) return this.joinApplyMeasuresOp.GetMeasuresStructure(expression);

            int attributeErrors = 0;
            IDataStructure structure = null;
            IExpression expr1 = expression.OperandsCollection.ToArray()[0];
            IExpression expr2 =
                expression.OperandsCollection.ToArray().Length > 1 ?
                expression.OperandsCollection.ToArray()[1] :
                null;
            IExpression expr3 =
                expression.OperandsCollection.ToArray().Length > 2 ?
                expression.OperandsCollection.ToArray()[2] :
                null;
            IExpression expr4 =
                expression.OperandsCollection.ToArray().Length > 3 ?
                expression.OperandsCollection.ToArray()[3] :
                null;

            this.ValidateDataStructure(expression, expr1.Structure);

            if (expr2 != null)
            {
                if (expr2.ExpressionText != "_")
                {
                    if (this.Symbol.In("substr", "replace", "instr") && !(expr2.Structure.IsSingleComponent)) throw new VtlOperatorError(expression, this.Name, $"Expected single component (arg2)");
                    if (this.Symbol.In("replace", "instr") && !expr2.Structure.Components.First().ValueDomain.DataType.In(BasicDataType.String, BasicDataType.None)) throw new VtlOperatorError(expression, this.Name, $"Type of component must be string (arg2)");
                    if (this.Symbol.In("substr") && !expr2.Structure.Components.First().ValueDomain.DataType.In(BasicDataType.Integer, BasicDataType.None)) throw new VtlOperatorError(expression, this.Name, $"Type of component must be integer (arg2)");
                    if (this.Symbol == "||")
                    {
                        this.ValidateDataStructure(expression, expr2.Structure);
                        if (!expr2.IsScalar && expr1.IsScalar) structure = expr2.Structure.GetCopy();
                        else if (!expr1.IsScalar && !expr2.IsScalar)
                        {
                            IDataStructure ds1 = expr1.Structure.GetCopy();
                            IDataStructure ds2 = expr2.Structure.GetCopy();

                            if (ds1.IsSupersetOf(ds2, true, false, true)) structure = ds1.WithAttributesOf(ds2);
                            else if (ds2.IsSupersetOf(ds1, true, false, true)) structure = ds2.WithAttributesOf(ds1);
                            else throw new VtlOperatorError(expression, this.Name, "Structures of datasets don't match.");
                        }
                    }
                }
            }
            if (expr3 != null)
            {
                if (expr3.ExpressionText != "_")
                {
                    if (this.Symbol.In("substr", "replace", "instr") && !(expr3.Structure.IsSingleComponent)) throw new VtlOperatorError(expression, this.Name, $"Expected single component (arg3)");
                    if (this.Symbol.In("substr", "instr") && !expr3.Structure.Components.First().ValueDomain.DataType.In(BasicDataType.Integer, BasicDataType.None)) throw new VtlOperatorError(expression, this.Name, $"Type of component must be integer (arg3)");
                    if (this.Symbol.In("replace") && !expr3.Structure.Components.First().ValueDomain.DataType.In(BasicDataType.String, BasicDataType.None)) throw new VtlOperatorError(expression, this.Name, $"Type of component must be string (arg3)");
                }
            }
            if (expr4 != null)
            {
                if (expr4.ExpressionText != "_")
                {
                    if (this.Symbol == "instr" && !(expr4.Structure.IsSingleComponent)) throw new VtlOperatorError(expression, this.Name, $"Expected single component (arg4)");
                    if (this.Symbol == "instr" && !expr4.Structure.Components.First().ValueDomain.DataType.In(BasicDataType.Integer, BasicDataType.None)) throw new VtlOperatorError(expression, this.Name, $"Type of component must be integer (arg4)");
                }
            }

            if (structure == null) structure = expr1.Structure.GetCopy().WithAttributesOf(expr2?.Structure);
            
            if (this.Symbol.In("instr", "length"))
            {
                if (expr1.Structure.Measures.Count > 1) throw new VtlOperatorError(expression, this.Name, "Expected structure with only one measure.");
                structure.Measures.Clear();
                structure.Measures.Add(new StructureComponent(BasicDataType.Integer, "int_var"));
                structure.Measures[0].BaseComponentName = expr1.Structure.GetCopy().Measures[0].ComponentName;
            }
            else if (!structure.IsSingleComponent)
            {
                foreach (StructureComponent measure in structure.Measures)
                {
                    measure.ValueDomain = new ValueDomain(BasicDataType.String);
                }
            }
            else return this.dsResolver("string_var", ComponentType.Measure, BasicDataType.String);

            return structure;
        }

        /// <summary>
        /// Validates a given data structure.
        /// </summary>
        /// <param name="expr">The source expression for throwing exceptions.</param>
        /// <param name="structure">The data structure to validate.</param>
        private void ValidateDataStructure(IExpression expr, IDataStructure structure)
        {
            if (structure.IsSingleComponent)
            {
                if (!structure.Components.First().ValueDomain.DataType.In(BasicDataType.String, BasicDataType.None)) 
                    throw new VtlOperatorError(expr, this.Name, $"Expected string components");
            }
            else
            {
                foreach (StructureComponent measure in structure.Measures)
                {
                    if (!measure.ValueDomain.DataType.In(BasicDataType.String, BasicDataType.None)) 
                        throw new VtlOperatorError(expr, this.Name, $"Expected string components");
                }
            }
        }
    }
}
