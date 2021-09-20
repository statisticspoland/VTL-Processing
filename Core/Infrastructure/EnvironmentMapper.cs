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
            this.DefaultNamespace = null;
            this.DefaultTargetPrefix = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnvironmentMapper"/> class.
        /// </summary>
        /// <param name="namespaceMapping">The dictionary of mapped names.</param>
        public EnvironmentMapper(Dictionary<string, string> namespaceMapping) : this()
        {
            if (namespaceMapping == null) throw new ArgumentNullException("namespaceMapping");
            this.Mapping = namespaceMapping;
        }

        public Dictionary<string, string> Mapping { get; set; }
        public string DefaultNamespace { get; set; }
        public string DefaultTargetPrefix { get; set; }

        public string Map(string datasetName)
        {
            string[] split = datasetName.Split('\\');
            string ns = split.Length == 2 ? split[0] : this.DefaultNamespace;
            string ds = split[split.Length - 1];

            if (split.Length > 2)
            {
                throw new ArgumentOutOfRangeException("datasetName", $"Invalid DataSet identifier: {datasetName}.");
            }

            if (ns != null && this.Mapping.ContainsKey(ns))
            {
                return $"{this.Mapping[ns]}{ds}";
            }
            else
            {   if (this.DefaultTargetPrefix == null)
                {
                    throw new ArgumentOutOfRangeException("datasetName", $"DataSet identifier {datasetName} has been not found in the environment mapper dictionary.");
                }
                else
                {
                    return $"{this.DefaultTargetPrefix}{ds}";
                }
            }
        }
    }
}
