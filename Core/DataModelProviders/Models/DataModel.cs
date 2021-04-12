namespace StatisticsPoland.VtlProcessing.Core.DataModelProviders.Models
{
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System;

    public class DataModel : IDataModel
    {
        private readonly IDataModel _rootModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataModel"/> class.
        /// </summary>
        /// <param name="rootModel">The root data model.</param>
        public DataModel(IDataModel rootModel)
        {
            this._rootModel = rootModel;
        }

        public string DefaultNamespace => this._rootModel.DefaultNamespace;

        /// <summary>
        /// Gets or sets the namespace name.
        /// </summary>
        public string Namespace { get; set; }

        public virtual IDataStructure GetDatasetStructure(string datasetName)
        {
            throw new NotImplementedException();
        }
    }
}
