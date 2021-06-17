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
        /// <summary>
        /// Initializes a new instance of the <see cref="EnvironmentMapper"/> class.
        /// </summary>
        public EnvironmentMapper()
        {
            this.Mapping = new Dictionary<string, string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnvironmentMapper"/> class.
        /// </summary>
        /// <param name="namespaceMapping">The dictionary of mapped names.</param>
        public EnvironmentMapper(Dictionary<string, string> namespaceMapping)
        {
            if (namespaceMapping == null) throw new ArgumentNullException("namespaceMapping");
            this.Mapping = namespaceMapping;
        }

        public Dictionary<string, string> Mapping { get; set; }

        public string Map(string datasetName)
        {
            string[] split = datasetName.Split('\\');

            switch (split.Length)
            {
                case 1: return datasetName;
                case 2:
                    if (!this.Mapping.ContainsKey(split[0]))
                        throw new ArgumentOutOfRangeException("datasetName", $"DataSet identifier {datasetName} has been not found in the environment mapper dictionary.");
                    return $"{this.Mapping[split[0]]}{split[1]}";
                default: throw new ArgumentOutOfRangeException("datasetName", $"Invalid DataSet identifier: {datasetName}.");
            }
        }
    }
}
