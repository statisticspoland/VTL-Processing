namespace StatisticsPoland.VtlProcessing.Core.Models
{
    using StatisticsPoland.VtlProcessing.Core.Models.Types;

    /// <summary>
    /// The VTL 2.0 structure component representation.
    /// </summary>
    public class StructureComponent
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="StructureComponent"/> class.
        /// </summary>
        public StructureComponent()
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="StructureComponent"/> class.
        /// </summary>
        /// <param name="valueDomain">The value domain.</param>
        /// <param name="name">The component name.</param>
        /// <param name="compType">The component type.</param>
        public StructureComponent(ValueDomain valueDomain, string name = "VALUE", ComponentType compType = ComponentType.Measure)
        {
            this.ValueDomain = valueDomain;
            this.ComponentType = compType;
            this.ComponentName = name;
            this.BaseComponentName = name;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="StructureComponent"/> class.
        /// </summary>
        /// <param name="dataType">The data type.</param>
        /// <param name="name">The component name.</param>
        /// <param name="compType">The component type.</param>
        public StructureComponent(BasicDataType dataType, string name = "VALUE", ComponentType compType = ComponentType.Measure)
            : this(new ValueDomain(dataType), name, compType)
        {
        }

        /// <summary>
        /// Gets or sets the name of the component.
        /// </summary>
        public string ComponentName { get; set; }

        /// <summary>
        /// Gets or sets the base name of the component.
        /// </summary>
        public string BaseComponentName { get; set; }

        /// <summary>
        /// Gets or sets the component name on the target storage.
        /// </summary>
        public string MappedName { get; set; }

        /// <summary>
        /// Gets or sets the type of the component.
        /// </summary>
        public ComponentType ComponentType { get; set; }

        /// <summary>
        /// Gets or sets the value domain.
        /// </summary>
        public ValueDomain ValueDomain { get; set; }
    }
}