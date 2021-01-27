namespace StatisticsPoland.VtlProcessing.Core.Models.Logical
{
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The VTL 2.0 expression's data structure representation.
    /// </summary>
    public class DataStructure : IDataStructure
    {
        private readonly ILogger<IDataStructure> logger;
        private IList<StructureComponent> identifiers;
        private IList<StructureComponent> measures;
        private IList<StructureComponent> nonViralAttributes;
        private IList<StructureComponent> viralAttributes;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataStructure"/> class.
        /// </summary>
        [JsonConstructor]
        public DataStructure()
        {
            this.DatasetName = string.Empty;
            this.DatasetType = DatasetType.Regular;
            this.identifiers = new List<StructureComponent>();
            this.measures = new List<StructureComponent>();
            this.viralAttributes = new List<StructureComponent>();
            this.nonViralAttributes = new List<StructureComponent>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataStructure"/> class.
        /// </summary>
        /// <param name="logger">The errors logger.</param>
        public DataStructure(ILogger<IDataStructure> logger = null)
            : this()
        {
            this.logger = logger;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataStructure"/> class <b>for a single component structure.</b>.
        /// </summary>
        /// <param name="compName">The name of the component.</param>
        /// <param name="compType">The component type.</param>
        /// <param name="dataType">The data type of the component.</param>
        /// <param name="logger">The errors logger.</param>
        public DataStructure(string compName, ComponentType compType, BasicDataType dataType, ILogger<IDataStructure> logger = null)
            : this(logger)
        {
            switch (compType)
            {
                case ComponentType.Identifier: this.identifiers = new List<StructureComponent>() { new StructureComponent(dataType, compName) }; ; break;
                case ComponentType.Measure: this.measures = new List<StructureComponent>() { new StructureComponent(dataType, compName) }; ; break;
                case ComponentType.NonViralAttribute: this.nonViralAttributes = new List<StructureComponent>() { new StructureComponent(dataType, compName) }; ; break;
                case ComponentType.ViralAttribute: this.viralAttributes = new List<StructureComponent>() { new StructureComponent(dataType, compName) }; ; break;
                default: throw new Exception("Wrong component type.");
            }
        }

        public string DatasetName { get; set; }

        public DatasetType DatasetType { get; set; }

        public bool IsSingleComponent => this.Components.Count == 1;

        public IList<StructureComponent> Identifiers
        { 
            get
            {
                this.identifiers.Where(identifier => identifier.ComponentType != ComponentType.Identifier).ToList().ForEach(identifier => identifier.ComponentType = ComponentType.Identifier);
                this.identifiers = this.identifiers.OrderBy(identifier => identifier.ComponentName).ToList();
                return this.identifiers;
            }
            set => this.identifiers = value;
        }

        public IList<StructureComponent> Measures
        {
            get
            {
                this.measures.Where(measure => measure.ComponentType != ComponentType.Measure).ToList().ForEach(measure => measure.ComponentType = ComponentType.Measure);
                this.measures = this.measures.OrderBy(measure => (measure.ComponentName.Split('#').Length == 1 ? measure.ComponentName : measure.ComponentName.Split('#')[1])).ToList();
                return this.measures;
            }
            set => this.measures = value;
        }

        public IList<StructureComponent> NonViralAttributes
        {
            get
            {
                this.nonViralAttributes.Where(attribute => attribute.ComponentType != ComponentType.NonViralAttribute).ToList().ForEach(attribute => attribute.ComponentType = ComponentType.NonViralAttribute);
                this.nonViralAttributes = this.nonViralAttributes.OrderBy(attribute => (attribute.ComponentName.Split('#').Length == 1 ? attribute.ComponentName : attribute.ComponentName.Split('#')[1])).ToList();
                return this.nonViralAttributes;
            }
            set => this.nonViralAttributes = value;
        }

        public IList<StructureComponent> ViralAttributes
        {
            get
            {
                this.viralAttributes.Where(attribute => attribute.ComponentType != ComponentType.ViralAttribute).ToList().ForEach(attribute => attribute.ComponentType = ComponentType.ViralAttribute);
                this.viralAttributes = this.viralAttributes.OrderBy(attribute => (attribute.ComponentName.Split('#').Length == 1 ? attribute.ComponentName : attribute.ComponentName.Split('#')[1])).ToList();
                return this.viralAttributes;
            }
            set => this.viralAttributes = value;
        }

        public IList<StructureComponent> Components 
        { 
            get
            {
                List<StructureComponent> components = new List<StructureComponent>();
                components.AddRange(this.Identifiers);
                components.AddRange(this.Measures);
                components.AddRange(this.NonViralAttributes);
                components.AddRange(this.ViralAttributes);

                return components;
            }
        }

        public IDataStructure GetCopy(bool copyName = false)
        {
            DataStructure copy = new DataStructure();
            copy.Identifiers = this.GetCopyOfList(this.Identifiers);
            copy.Measures = this.GetCopyOfList(this.Measures);
            copy.ViralAttributes = this.GetCopyOfList(this.ViralAttributes);
            copy.NonViralAttributes = this.GetCopyOfList(this.NonViralAttributes);
            if (copyName) copy.DatasetName = this.DatasetName;

            return copy;
        }

        public IDataStructure WithAttributesOf(IDataStructure dataStructure)
        {
            if (dataStructure != null)
            {
                StructureComponent existingAttribute;
                foreach (StructureComponent attribute in dataStructure.NonViralAttributes)
                {
                    if ((existingAttribute = this.nonViralAttributes.FirstOrDefault(at => at.ComponentName == attribute.ComponentName)) == null)
                        this.nonViralAttributes.Add(attribute);
                    else if (existingAttribute.ValueDomain.DataType != attribute.ValueDomain.DataType)
                    {
                        VtlError warning = new VtlError(null, $"Data type of the non-viral attribute \"{attribute.ComponentName} has been ovverided.\"");
                        this.logger?.LogWarning(warning.Message, warning);
                    }
                }

                foreach (StructureComponent attribute in dataStructure.ViralAttributes)
                {
                    if ((existingAttribute = this.viralAttributes.FirstOrDefault(at => at.ComponentName == attribute.ComponentName)) == null)
                        this.viralAttributes.Add(attribute);
                    else if (existingAttribute.ValueDomain.DataType != attribute.ValueDomain.DataType)
                        throw new VtlError(null, $"Data type of the viral attribute \"{attribute.ComponentName} has been ovverided.\"");
                }
            }

            return this.GetCopy();
        }

        public bool IsSupersetOf(IDataStructure structure, bool checkMeasures = false, bool checkAttributes = false, bool allowNulls = false)
        {
            StructureComponent[] supersetComponents = this.Identifiers.ToArray();
            StructureComponent[] subsetComponents = structure.Identifiers.ToArray();

            for (int i = 0; i < 4; i++)
            {
                if (checkMeasures && i == 1)
                {
                    if (this.Measures.Count != structure.Measures.Count) 
                        return false;

                    supersetComponents = this.Measures.ToArray();
                    subsetComponents = structure.Measures.ToArray();
                }
                else if (checkAttributes && i == 2)
                {
                    if (this.ViralAttributes.Count != structure.ViralAttributes.Count) 
                        return false;

                    supersetComponents = this.ViralAttributes.ToArray();
                    subsetComponents = structure.ViralAttributes.ToArray();
                }

                foreach (StructureComponent subsetComponent in subsetComponents)
                {
                    if (supersetComponents.FirstOrDefault(comp => comp.ComponentName == subsetComponent.ComponentName && 
                            comp.ValueDomain.DataType.EqualsObj(subsetComponent.ValueDomain.DataType, i != 0, i != 0 && allowNulls)) == null)
                        return false;
                }
            }

            return true;
        }

        public void AddStructure(IDataStructure structure)
        {
            (this.Identifiers as List<StructureComponent>).AddRange(structure.Identifiers);
            (this.Measures as List<StructureComponent>).AddRange(structure.Measures);
            (this.NonViralAttributes as List<StructureComponent>).AddRange(structure.NonViralAttributes);
            (this.ViralAttributes as List<StructureComponent>).AddRange(structure.ViralAttributes);

            this.DatasetName = string.Empty;
            this.RemoveComponentDuplicates();
            this.RemoveComponentDuplicates(structure);
        }

        public void RemoveComponentDuplicates()
        {
            this.Identifiers = (this.Identifiers as List<StructureComponent>).GroupBy(g => g.ComponentName).Select(s => s.LastOrDefault(c => c.ValueDomain.DataType != BasicDataType.None) ?? s.Last()).ToList();
            this.Measures = (this.Measures as List<StructureComponent>).GroupBy(g => g.ComponentName).Select(s => s.LastOrDefault(c => c.ValueDomain.DataType != BasicDataType.None) ?? s.Last()).ToList();
            this.NonViralAttributes = (this.NonViralAttributes as List<StructureComponent>).GroupBy(g => g.ComponentName).Select(s => s.LastOrDefault(c => c.ValueDomain.DataType != BasicDataType.None) ?? s.Last()).ToList();
            this.ViralAttributes = (this.ViralAttributes as List<StructureComponent>).GroupBy(g => g.ComponentName).Select(s => s.LastOrDefault(c => c.ValueDomain.DataType != BasicDataType.None) ?? s.Last()).ToList();
        }

        public void RemoveComponentDuplicates(IDataStructure duplicatesDataStructure)
        {
            (this.Identifiers as List<StructureComponent>).RemoveAll(identifier =>
                duplicatesDataStructure.Components.FirstOrDefault(component =>
                    component.ComponentName == identifier.ComponentName && component.ComponentType != identifier.ComponentType) != null);

            (this.Measures as List<StructureComponent>).RemoveAll(measure =>
                duplicatesDataStructure.Components.FirstOrDefault(component =>
                    component.ComponentName == measure.ComponentName && component.ComponentType != measure.ComponentType) != null);

            (this.NonViralAttributes as List<StructureComponent>).RemoveAll(attribute =>
                duplicatesDataStructure.Components.FirstOrDefault(component =>
                    component.ComponentName == attribute.ComponentName && component.ComponentType != attribute.ComponentType) != null);

            (this.ViralAttributes as List<StructureComponent>).RemoveAll(attribute =>
                duplicatesDataStructure.Components.FirstOrDefault(component =>
                    component.ComponentName == attribute.ComponentName && component.ComponentType != attribute.ComponentType) != null);
        }

        /// <summary>
        /// Gets the copy of a given list.
        /// </summary>
        /// <param name="components">The list to copy.</param>
        /// <returns>The copy of the given list.</returns>
        private IList<StructureComponent> GetCopyOfList(IList<StructureComponent> components)
        {
            IList<StructureComponent> copy = new List<StructureComponent>();

            foreach (StructureComponent component in components)
            {
                StructureComponent componentCopy = new StructureComponent(
                    component.ValueDomain.DataType, 
                    component.ComponentName);
                componentCopy.BaseComponentName = component.BaseComponentName;
                copy.Add(componentCopy);
            }

            return copy;
        }
    }
}