namespace StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary.ComponentManagement
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;

    /// <summary>
    /// Definition of an operator that collects components from operand expressions of its epxression.
    /// </summary>
    public class CompCollectorOperator
    {
        private readonly DataStructureResolver _dsResolver;
        private readonly string _name;

        /// <summary>
        /// Initialises a new instance of the <see cref="CompCollectorOperator"/> class.
        /// </summary>
        /// <param name="dsResolver">The data structure resolver.</param>
        /// <param name="name">The name of a main operator.</param>
        public CompCollectorOperator(DataStructureResolver dsResolver, string name)
        {
            this._dsResolver = dsResolver;
            this._name = name;
        }

        /// <summary>
        /// Gets the structure of the resulting operator parameter for specified operands.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>A dynamically defined structure of the output parameter for the given input parameters.</returns>
        public IDataStructure GetOutputStructure(IExpression expression)
        {
            IDataStructure dataStructure = this._dsResolver();
            foreach (IExpression expr in expression.OperandsCollection)
            {
                if (!expr.IsScalar) throw new VtlOperatorError(expression, this._name, "Expected scalar expression.");
                dataStructure.AddStructure(expr.Structure);
            }

            return dataStructure;
        }
    }
}
