namespace StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary.ComponentManagement
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;

    /// <summary>
    /// Definition of an operator that expressions creates new components.
    /// </summary>
    public class CompCreateOperator
    {
        private readonly DataStructureResolver _dsResolver;
        private readonly string _name;

        /// <summary>
        /// Initialises a new instance of the <see cref="CompCreateOperator"/> class.
        /// </summary>
        /// <param name="dsResolver">The data structure resolver.</param>
        /// <param name="name">The name of a main operator.</param>
        public CompCreateOperator(DataStructureResolver dsResolver, string name)
        {
            this._dsResolver = dsResolver;
            this._name = name;
        }

        /// <summary>
        /// Gets or sets the operator keyword.
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// Gets the structure of the resulting operator parameter for specified operands.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>A dynamically defined structure of the output parameter for the given input parameters.</returns>
        public IDataStructure GetOutputStructure(IExpression expression)
        {
            IExpression expr1 = expression.Operands["ds_1"];
            IExpression expr2 = expression.Operands["ds_2"];
            BasicDataType type1 = expr1.Structure.Components[0].ValueDomain.DataType;
            BasicDataType type2 = expr2.Structure.Components[0].ValueDomain.DataType;

            if (!expr1.IsScalar || expr1.OperatorSymbol != "comp")
                throw new VtlOperatorError(expression, this._name, "Expected component operator expression as the first parameter.");

            if (!expr2.IsScalar)
                throw new VtlOperatorError(expression, this._name, "Expected scalar expression as the second parameter.");

            if (expr1.Structure.Identifiers.Count != 0 && expr1.Structure.Identifiers[0].ValueDomain.DataType != BasicDataType.None)
                throw new VtlOperatorError(expression, this._name, "Identifier of any dataset can not be modify.");

            if (!type1.EqualsObj(type2))
                throw new VtlOperatorError(expression, this._name, "Types of parameters don't match.");

            BasicDataType type = type2;
            if (type == BasicDataType.None || (type2 == BasicDataType.Integer && type1 == BasicDataType.Number)) type = type1;

            IDataStructure dataStructure = this._dsResolver();
            switch (this.Keyword)
            {
                case "identifier":
                    if (this.AreNullValues(expr2)) throw new VtlOperatorError(expression, this._name, "Possible putting null values into an identifier component.");
                    dataStructure.Identifiers.Add(
                        new StructureComponent(type, expr1.ExpressionText));
                    break;
                case "measure":
                    dataStructure.Measures.Add(
                        new StructureComponent(type, expr1.ExpressionText));
                    break;
                case "viral attribute":
                    dataStructure.ViralAttributes.Add(
                        new StructureComponent(type, expr1.ExpressionText));
                    break;
                case "attribute":
                    dataStructure.NonViralAttributes.Add(
                        new StructureComponent(type, expr1.ExpressionText));
                    break;
                default: throw new VtlOperatorError(expression, this._name, $"Unknown keyword of an operator: {this.Keyword}");
            }

            return dataStructure;
        }

        /// <summary>
        /// Checks if expression or its operands could have null values.
        /// </summary>
        /// <param name="expression">Expression to check.</param>
        /// <returns>Value indicating if expression or its operands could have null values</returns>
        private bool AreNullValues(IExpression expression)
        {
            foreach (IExpression expr in expression.OperandsCollection)
            {
                if (this.AreNullValues(expr)) return true;
            }

            if (expression.Structure.Components[0].ValueDomain.DataType == BasicDataType.None) return true;
            return false;
        }
    }
}
