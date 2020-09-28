namespace StatisticsPoland.VtlProcessing.Core.MiddleEnd.Utilities
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Modifiers.Utilities.Interfaces;
    using System.Linq;

    /// <summary>
    /// Infers basic data types of components.
    /// </summary>
    public class ComponentTypeInference : IComponentTypeInference
    {
        private DataStructureResolver dsResolver;

        /// <summary>
        /// Initialises a new instance of the <see cref="ComponentTypeInference"/> class.
        /// </summary>
        /// <param name="dsResolver">The data structure resolver.</param>
        public ComponentTypeInference(DataStructureResolver dsResolver)
        {
            this.dsResolver = dsResolver;
        }

        public BasicDataType InferTypeOfComponent(IExpression expr, ComponentType? componentType = null)
        {
            BasicDataType? dataType = this.InferByJoinDsBranch(expr, componentType);

            if (dataType != null) return (BasicDataType)dataType;

            string typePrefix = componentType != null ? " of a type " : string.Empty;
            throw new VtlOperatorError(expr, expr.OperatorSymbol, $"Component{typePrefix}{componentType} {expr.ExpressionText} has been not found in any dataset.");
        }

        /// <summary>
        /// Infers a basic data type of a component by a "join ds" branch expression.
        /// </summary>
        /// <param name="expr">The component expression.</param>
        /// <param name="componentType">The type of a component.</param>
        /// <returns>The basic data type type of a component.</returns>
        private BasicDataType? InferByJoinDsBranch(IExpression expr, ComponentType? componentType)
        {
            BasicDataType? dataType = null;
            if (expr.CurrentJoinExpr?.Operands["ds"]?.OperandsCollection?.Count >= 0)
            {
                foreach (IExpression alias in expr.CurrentJoinExpr.Operands["ds"].OperandsCollection)
                {
                    dataType = alias.Structure?.Components.FirstOrDefault(comp => comp.ComponentName == expr.ExpressionText && (comp.ComponentType == componentType || componentType == null))?.ValueDomain.DataType;
                    if (dataType != null)
                    {
                        componentType = alias.Structure.Components.First(comp => comp.ComponentName == expr.ExpressionText && (comp.ComponentType == componentType || componentType == null)).ComponentType;
                        expr.Structure = this.dsResolver(expr.ExpressionText, (ComponentType)componentType, (BasicDataType)dataType);

                        return dataType;
                    }
                }
            }

            return dataType;
        }
    }
}
