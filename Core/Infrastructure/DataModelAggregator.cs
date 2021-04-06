namespace StatisticsPoland.VtlProcessing.Core.Infrastructure
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The aggregator of data models.
    /// </summary>
    internal class DataModelAggregator : IDataModelAggregator
    {
        private Func<string> getDefaultNamespace;

        /// <summary>
        /// Initializes a new instance of nthe <see cref="DataModelAggregator"/> class.
        /// </summary>
        public DataModelAggregator()
        {
            this.DataModels = new List<IDataModel>();
        }

        /// <summary>
        /// Initializes a new instance of nthe <see cref="DataModelAggregator"/> class.
        /// </summary>
        /// <param name="defaultNamespace">The default namespace name.</param>
        public DataModelAggregator(Func<string> defaultNamespace)
            : this()
        {
            this.getDefaultNamespace = defaultNamespace;
        }

        /// <summary>
        /// Initializes a new instance of nthe <see cref="DataModelAggregator"/> class.
        /// </summary>
        /// <param name="defaultNamespace">The default namespace name.</param>
        /// <param name="dataModels">The data model collection.</param>
        public DataModelAggregator(string defaultNamespace, params IDataModel[] dataModels)
            : this()
        {
            this.DefaultNamespace = defaultNamespace;

            if (dataModels != null) this.DataModels = dataModels.ToList();
        }

        public string DefaultNamespace
        {
            get => this.getDefaultNamespace();
            set => this.getDefaultNamespace = new Func<string>(() => { return value; });
        }

        public ICollection<IDataModel> DataModels { get; set; }

        public IDataStructure GetDatasetStructure(string datasetName)
        {
            IDataStructure structure;
            if (this.DataModels == null) throw new NullReferenceException("DataModels in DataModelAggregator.");
            foreach (IDataModel dataModel in this.DataModels)
            {
                structure = dataModel.GetDatasetStructure(datasetName);
                if (structure != null) return structure;
            }

            string[] split = datasetName.Split(@"\");
            switch (split.Length)
            {
                case 1: throw new Exception($@"Dataset {this.DefaultNamespace}\{datasetName} has been not found in any data model.");
                default: throw new Exception($"Dataset {datasetName} has been not found in any data model.");
            }
        }
    }
}
