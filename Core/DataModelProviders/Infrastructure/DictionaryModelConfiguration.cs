namespace StatisticsPoland.VtlProcessing.Core.DataModelProviders.Infrastructure
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Logical;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.DataModelProviders.Infrastructure.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The VTL 2.0 regular model configuration.
    /// </summary>
    public class DictionaryModelConfiguration : IDictionaryModelConfiguration
    {
        private readonly Dictionary<string, IDataStructure> dataStructures;

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryModelConfiguration"/> class.
        /// </summary>
        /// <param name="dataStructures">The dictionary of structures.</param>
        public DictionaryModelConfiguration(Dictionary<string, IDataStructure> dataStructures)
        {
            this.dataStructures = dataStructures;
        }

        /// <summary>
        /// Adds a dataset to the model.
        /// </summary>
        /// <param name="namespace">The name of namespace.</param>
        /// <param name="name">The name of dataset.</param>
        /// <param name="componentSettings">Tuple defining the element of structure of the dataset (Component Type, VTL data type, Component name).</param>
        public IDictionaryModelConfiguration AddDataSet(string name, params (ComponentType, BasicDataType, string)[] componentSettings)
        {
            DataStructure structure = new DataStructure();
            structure.DatasetName = name;

            structure.Identifiers = GetStructureComponentsByType(ComponentType.Identifier, componentSettings);
            structure.Measures = GetStructureComponentsByType(ComponentType.Measure, componentSettings);
            structure.NonViralAttributes = GetStructureComponentsByType(ComponentType.NonViralAttribute, componentSettings);
            structure.ViralAttributes = GetStructureComponentsByType(ComponentType.ViralAttribute, componentSettings);

            this.dataStructures.Add(structure.DatasetName, structure);

            return this;
        }

        /// <summary>
        /// Gets the structure component list of a given component type.
        /// </summary>
        /// <param name="type">The component type.</param>
        /// <param name="componentSettings">The component settings.</param>
        /// <returns>The structure component list.</returns>
        private IList<StructureComponent> GetStructureComponentsByType(ComponentType type, (ComponentType, BasicDataType, string)[] componentSettings)
        {
            List<StructureComponent> comps = new List<StructureComponent>();
            foreach ((ComponentType, BasicDataType, string) tuple in componentSettings.Where(t => t.Item1 == type))
            {
                comps.Add(new StructureComponent(tuple.Item2, tuple.Item3));
            }

            return comps;
        }
    }
}
