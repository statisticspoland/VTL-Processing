namespace StatisticsPoland.VtlProcessing.Core.Modifiers.Utilities.Interfaces
{
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;

    /// <summary>
    /// The component type inference interface.
    /// </summary>
    public interface IComponentTypeInference
    {
        /// <summary>
        /// Infers a basic data type of the component.
        /// </summary>
        /// <param name="expr">The component expression.</param>
        /// <param name="componentType">The type of a component.</param>
        /// <returns>The basic data type type of a component.</returns>
        BasicDataType InferTypeOfComponent(IExpression expr, ComponentType? componentType = null);
    }
}
