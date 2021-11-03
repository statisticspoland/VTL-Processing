namespace StatisticsPoland.VtlProcessing.Core.DataModelProviders
{
    using Newtonsoft.Json;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Logical;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;

    /// <summary>
    /// The JSON VTL 2.0 data model.
    /// </summary>
    public class JsonDataModelProvider: DataModelProviderBase
    {
        private readonly Dictionary<string, IDataStructure> dataStructures;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonDataModelProvider"/> class.
        /// </summary>
        /// <param name="rootModel">The root data model.</param>
        [JsonConstructor]
        private JsonDataModelProvider(IDataModelProvider rootModel = null) : base(rootModel)
        {
            this.dataStructures = new Dictionary<string, IDataStructure>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonDataModelProvider"/> class.
        /// </summary>
        /// <param name="rootModel">The root data model.</param>
        /// <param name="jsonFilePath">The JSON file path/url.</param>
        public JsonDataModelProvider(IDataModelProvider rootModel, string jsonFilePath) : this(rootModel)
        {
            this.LoadData(jsonFilePath);
        }

        /// <summary>
        /// The collection of data structures.
        /// </summary>
        public ICollection<DataStructure> DataStructuresCollection { get; set; }

        public override IDataStructure GetDatasetStructure(string datasetName)
        {
            string @namespace;
            this.SplitDatasetName(datasetName, out @namespace, out datasetName);

            if (@namespace == this.Namespace && this.dataStructures.ContainsKey(datasetName))
                return this.dataStructures[datasetName];

            return null;
        }

        /// <summary>
        /// Loads a data from a JSON file
        /// </summary>
        /// <param name="jsonFilePath">The JSON file path</param>
        private void LoadData(string jsonFilePath)
        {
            JsonDataModelProvider jsonObject;

            try
            {
                if (File.Exists(jsonFilePath))
                    jsonObject = JsonConvert.DeserializeObject<Dictionary<string, JsonDataModelProvider>>(File.ReadAllText(jsonFilePath))["DataModel"];
                else
                {
                    jsonObject = this.GetFromUrl(jsonFilePath);
                }
            
                this.Namespace = jsonObject.Namespace;
                this.DataStructuresCollection = jsonObject.DataStructuresCollection;

                foreach (DataStructure ds in this.DataStructuresCollection)
                {
                    this.dataStructures.Add(ds.DatasetName, ds);
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(FileNotFoundException)) throw;
                throw new InvalidDataException("Invalid model of JSON file");
            }
        }

        /// <summary>
        /// Loads a data from a url address.
        /// </summary>
        /// <param name="url">The url address</param>
        private JsonDataModelProvider GetFromUrl(string url)
        {
            try
            {
                return JsonConvert.DeserializeObject<Dictionary<string, JsonDataModelProvider>>(new WebClient().DownloadString(url))["DataModel"];
            }
            catch(Exception ex)
            {
                throw new IOException($"Error loading model definition: {url}", ex);
            }
        }
    }
}
