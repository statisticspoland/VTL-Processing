namespace StatisticsPoland.VtlProcessing.Core.DataModelProviders.Models
{
    using Newtonsoft.Json;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Logical;
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// The JSON VTL 2.0 data model.
    /// </summary>
    public class DataModelJson: DataModel
    {
        private Dictionary<string, IDataStructure> dataStructures;

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
        /// <param name="jsonFilePath">The JSON file path.</param>
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
                default: throw new Exception($"Invalid DataSet identifier: {datasetName}");
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
            if (File.Exists(jsonFilePath))
            {
                try
                {
                    DataModelJson jsonObject = JsonConvert.DeserializeObject<Dictionary<string, DataModelJson>>(File.ReadAllText(jsonFilePath))["DataModel"];
                    this.Namespace = jsonObject.Namespace;
                    this.DataStructuresCollection = jsonObject.DataStructuresCollection;

                    foreach (DataStructure ds in this.DataStructuresCollection)
                    {
                        this.dataStructures.Add(ds.DatasetName, ds);
                    }
                }
                catch
                {
                    throw new NullReferenceException("Dane zawarte w pliku JSON nie pasują do modelu.");
                }
            }
            else
            {
                throw new FileNotFoundException("Nie znaleziono pliku", jsonFilePath);
            }
        }
    }
}
