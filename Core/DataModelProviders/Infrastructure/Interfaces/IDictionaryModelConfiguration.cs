namespace StatisticsPoland.VtlProcessing.Core.DataModelProviders.Infrastructure.Interfaces
{
    using StatisticsPoland.VtlProcessing.Core.Models.Types;

    /// <summary>
    /// The VTL 2.0 dictionary model configuration interface.
    /// </summary>
    public interface IDictionaryModelConfiguration
    {
        /// <summary>
        /// Adds a dataset to the model.
        /// </summary>
        /// <param name="name">The name of dataset.</param>
        /// <param name="componentSettings">The tuple defining an element of structure of the dataset.</param>
        IDictionaryModelConfiguration AddDataSet(string name, params (ComponentType, BasicDataType, string)[] componentSettings);
    }
}
