namespace StatisticsPoland.VtlProcessing.Core.DataModelProviders.Infrastructure.Interfaces
{
    using StatisticsPoland.VtlProcessing.Core.Models.Types;

    /// <summary>
    /// The VTL 2.0 regular model configuration interface.
    /// </summary>
    public interface IRegularModelConfiguration
    {
        /// <summary>
        /// Adds a dataset to the model.
        /// </summary>
        /// <param name="namespace">The name of namespace.</param>
        /// <param name="name">The name of dataset.</param>
        /// <param name="componentSetting">The tuple defining an element of structure of the dataset.</param>
        IRegularModelConfiguration AddDataSet(string @namespace, string name, params (ComponentType, BasicDataType, string)[] componentSetting);
    }
}
