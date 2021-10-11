namespace StatisticsPoland.VtlProcessing.Core.DataModelProviders
{
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System;

    public abstract class DataModelProviderBase : IDataModelProvider
    {
        private readonly IDataModelProvider _rootModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataModelProviderBase"/> class.
        /// </summary>
        /// <param name="rootModel">The root data model.</param>
        public DataModelProviderBase(IDataModelProvider rootModel)
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

        protected void SplitDatasetName(string fullDatasetName, out string namespaceName, out string datasetName)
        {
            string[] split = fullDatasetName.Split(@"\");
            switch (split.Length)
            {
                case 1:
                    namespaceName = this.DefaultNamespace;
                    datasetName = split[0];
                    break;
                case 2:
                    namespaceName = split[0];
                    datasetName = split[1];
                    break;
                default: throw new ArgumentOutOfRangeException("datasetName", $"Invalid DataSet identifier: {fullDatasetName}");
            }
        }
    }
}
