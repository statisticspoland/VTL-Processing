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
        /// <summary>
        /// Initializes a new instance of nthe <see cref="DataModelAggregator"/> class.
        /// </summary>
        public DataModelAggregator(IEnvironmentMapper mapper)
        {
            this.EnvironmentMapper = mapper;
            this.DataModels = new List<IDataModel>();
        }

        /// <summary>
        /// Initializes a new instance of nthe <see cref="DataModelAggregator"/> class.
        /// </summary>
        /// <param name="defaultNamespace">The default namespace name.</param>
        /// <param name="mapper">The environment names mapper.</param>
        public DataModelAggregator(string defaultNamespace, IEnvironmentMapper mapper)
            : this(mapper)
        {
            this.DefaultNamespace = defaultNamespace;
            this.EnvironmentMapper = mapper;
        }

        /// <summary>
        /// Initializes a new instance of nthe <see cref="DataModelAggregator"/> class.
        /// </summary>
        /// <param name="mapper">The environment names mapper.</param>
        /// <param name="dataModels">The data model collection.</param>
        public DataModelAggregator(IEnvironmentMapper mapper, params IDataModel[] dataModels)
            : this(mapper)
        {
            if (dataModels != null) this.DataModels = dataModels.ToList();
        }

        /// <summary>
        /// Initializes a new instance of nthe <see cref="DataModelAggregator"/> class.
        /// </summary>
        /// <param name="defaultNamespace">The default namespace name.</param>
        /// <param name="mapper">The environment names mapper.</param>
        /// <param name="dataModels">The data model collection.</param>
        public DataModelAggregator(string defaultNamespace, IEnvironmentMapper mapper, params IDataModel[] dataModels)
            : this(defaultNamespace, mapper)
        {
            if (dataModels != null) this.DataModels = dataModels.ToList();
        }

        public string DefaultNamespace { get; set;  }

        public ICollection<IDataModel> DataModels { get; set; }

        public IEnvironmentMapper EnvironmentMapper { get; }

        public IDataStructure GetDatasetStructure(string datasetName)
        {
            IDataStructure structure;
            if (this.DataModels == null) throw new ArgumentNullException("datasetName", "DataModels in DataModelAggregator.");
            foreach (IDataModel dataModel in this.DataModels)
            {
                structure = dataModel.GetDatasetStructure(datasetName);
                if (structure != null) return structure;
            }

            string[] split = datasetName.Split(@"\");
            switch (split.Length)
            {
                case 1: throw new ArgumentOutOfRangeException("datasetName", $@"Dataset {this.DefaultNamespace}\{datasetName} has been not found in any data model.");
                default: throw new ArgumentOutOfRangeException("datasetName", $"Dataset {datasetName} has been not found in any data model.");
            }
        }
    }
}
