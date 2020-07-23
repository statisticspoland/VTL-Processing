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
    }
}
