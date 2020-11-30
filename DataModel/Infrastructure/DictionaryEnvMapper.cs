namespace StatisticsPoland.VtlProcessing.DataModel.Infrastructure
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The dictionary environment names mapper.
    /// </summary>
    public class DictionaryEnvMapper : IEnvironmentMapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryEnvMapper"/> class.
        /// </summary>
        /// <param name="namespaceMapping">The dictionary of mapped names.</param>
        public DictionaryEnvMapper(Dictionary<string, string> namespaceMapping)
        {
            if (namespaceMapping == null) throw new ArgumentNullException("mapping");
            this.Mapping = namespaceMapping;
        }

        /// <summary>
        /// Gets the dictionary of mapped names
        /// </summary>
        public Dictionary<string, string> Mapping { get; }

        public string Map(string datasetName)
        {
            string[] split = datasetName.Split('\\');

            switch (split.Length)
            {
                case 1: return datasetName;
                case 2: 
                    if (!this.Mapping.ContainsKey(split[0]))
                        throw new Exception($"DataSet identifier {datasetName} has been not found in the environment mapper dictionary.");
                    return $"{this.Mapping[split[0]]}{split[1]}";
                default: throw new Exception($"Invalid DataSet identifier: {datasetName}.");
            }
        }
    }
}
