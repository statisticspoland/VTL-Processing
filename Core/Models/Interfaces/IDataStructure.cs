namespace StatisticsPoland.VtlProcessing.Core.Models.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// The VTL 2.0 expression's data structure representation interface.
    /// </summary>
    public interface IDataStructure
    {
        /// <summary>
        /// Gets or sets the dataset name.
        /// </summary>
        string DatasetName { get; set; }

        /// <summary>
        /// Gets the value specyfing if the data structure is a single component.
        /// </summary>
        bool IsSingleComponent { get; }

        /// <summary>
        /// Gets or sets identifier components.
        /// </summary>
        /// <value>
        /// Collection of identifier components.
        /// </value>
        IList<StructureComponent> Identifiers { get; set; }

        /// <summary>
        /// Gets or sets measure components.
        /// </summary>
        /// <value>
        /// Collection of measure components.
        /// </value>
        IList<StructureComponent> Measures { get; set; }

        /// <summary>
        /// Gets or sets non viral attribute components.
        /// </summary>
        IList<StructureComponent> NonViralAttributes { get; set; }

        /// <summary>
        /// Gets or sets viral attribute components.
        /// </summary>
        IList<StructureComponent> ViralAttributes { get; set; }

        /// <summary>
        /// Gets all components.
        /// </summary>
        IList<StructureComponent> Components { get; }

        /// <summary>
        /// Gets the copy of this data structure.
        /// </summary>
        /// <param name="copyName">Specifies if copy the name of the data structure.</param>
        /// <returns>The copy of this data structure.</returns>
        IDataStructure GetCopy(bool copyName = false);

        /// <summary>
        /// Adds attributes of a given data structure and returns a new instance.
        /// </summary>
        /// <param name="dataStructure">The structure with attributes to add.</param>
        /// <param name="errorsNumberIn">The current number of errors.</param>
        /// <param name="errorsNumberOut">The number of errors after merging attributes.</param>
        /// <returns>The datastructure.</returns>
        IDataStructure WithAttributesOf(IDataStructure dataStructure, int errorsNumberIn, out int errorsNumberOut);

        /// <summary>
        /// Checks if this dataset is a superset of a given dataset.
        /// </summary>
        /// <param name="structure">The dataset to compare.</param>
        /// <param name="checkMeasures">Specifies if measures should be checked.</param>
        /// <param name="checkAttributes">Specifies if viral attributes should be checked.</param>
        /// <param name="allowNulls">Specifies if null values are equal to every type.</param>
        /// <returns>The value specyfing if it's a superset.</returns>
        bool IsSupersetOf(IDataStructure structure, bool checkMeasures = false, bool checkAttributes = false, bool allowNulls = false);
    }
}
