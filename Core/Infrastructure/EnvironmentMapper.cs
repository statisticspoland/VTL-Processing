namespace StatisticsPoland.VtlProcessing.Core.DataModelProviders.Infrastructure
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The dictionary environment names mapper.
    /// </summary>
    public class EnvironmentMapper : IEnvironmentMapper
    {
        private readonly IDataModelAggregator _dataModelAggregator;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnvironmentMapper"/> class.
        /// </summary>
        /// <param name="dataModelAggregator">The data models aggregator.</param>
        public EnvironmentMapper(IDataModelAggregator dataModelAggregator)
        {
            this._dataModelAggregator = dataModelAggregator;
            this.Mapping = new Dictionary<string, string>();
            this.DefaultTargetPrefix = string.Empty;
        }

        public Dictionary<string, string> Mapping { get; set; }

        public string DefaultNamespace => this._dataModelAggregator.DefaultNamespace;

        public string DefaultTargetPrefix { get; set; }

        public string Map(string datasetName)
        {
            string[] split = datasetName.Split(@"\");
            string namespaceName;
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
                default: throw new ArgumentOutOfRangeException("datasetName", $"Invalid DataSet identifier: {datasetName}");
            }

            if (namespaceName != null && this.Mapping.ContainsKey(namespaceName))
                return $"{this.Mapping[namespaceName]}{datasetName}";

            if (this.DefaultTargetPrefix != null)
                return $"{this.DefaultTargetPrefix}{datasetName}";
            else throw new KeyNotFoundException($"Invalid DataSet identifier: {datasetName}. Default target prefix is not set.");
        }
    }
}
