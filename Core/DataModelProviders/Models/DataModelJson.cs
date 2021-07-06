namespace StatisticsPoland.VtlProcessing.Core.DataModelProviders.Models
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
    public class DataModelJson: DataModel
    {
        private readonly Dictionary<string, IDataStructure> dataStructures;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataModelJson"/> class.
        /// </summary>
        /// <param name="rootModel">The root data model.</param>
        [JsonConstructor]
        private DataModelJson(IDataModel rootModel = null) : base(rootModel)
        {
            this.dataStructures = new Dictionary<string, IDataStructure>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataModelJson"/> class.
        /// </summary>
        /// <param name="rootModel">The root data model.</param>
        /// <param name="jsonFilePath">The JSON file path/url.</param>
        public DataModelJson(IDataModel rootModel, string jsonFilePath) : this(rootModel)
        {
            this.LoadData(jsonFilePath);
        }

        public ICollection<DataStructure> DataStructuresCollection { get; set; }

        public override IDataStructure GetDatasetStructure(string datasetName)
        {
            string[] split = datasetName.Split(@"\");
            switch (split.Length)
            {
                case 1:
                    if (this.DefaultNamespace != this.Namespace) return null;
                    break;
                case 2:
                    if (split[0] != this.Namespace) return null;
                    datasetName = split[1];
                    break;
                default: throw new ArgumentOutOfRangeException("datasetName", $"Invalid DataSet identifier: {datasetName}");
            }

            if (this.dataStructures.ContainsKey(datasetName))
                return this.dataStructures[datasetName];

            return null;
        }

        /// <summary>
        /// Loads a data from a JSON file
        /// </summary>
        /// <param name="jsonFilePath">The JSON file path</param>
        private void LoadData(string jsonFilePath)
        {
            DataModelJson jsonObject;

            try
            {
                if (File.Exists(jsonFilePath))
                    jsonObject = JsonConvert.DeserializeObject<Dictionary<string, DataModelJson>>(File.ReadAllText(jsonFilePath))["DataModel"];
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

        private DataModelJson GetFromUrl(string url)
        {
            try
            {
                return JsonConvert.DeserializeObject<Dictionary<string, DataModelJson>>(new WebClient().DownloadString(url))["DataModel"];
            }
            catch(Exception ex)
            {
                throw new IOException($"Error loading model definition: {url}", ex);
            }
        }
    }
}
