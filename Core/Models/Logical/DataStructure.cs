namespace StatisticsPoland.VtlProcessing.Core.Models.Logical
{
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
        private IList<StructureComponent> identifiers;
        private IList<StructureComponent> measures;
        private IList<StructureComponent> nonViralAttributes;
        private IList<StructureComponent> viralAttributes;

        /// <summary>
        /// Initializes new instance of the <see cref="DataStructure"/> class.
        /// </summary>
        public DataStructure()
        {
            this.DatasetName = string.Empty;
            this.identifiers = new List<StructureComponent>();
            this.measures = new List<StructureComponent>();
            this.viralAttributes = new List<StructureComponent>();
            this.nonViralAttributes = new List<StructureComponent>();
        }

        /// <summary>
        /// Initializes new instance of the <see cref="DataStructure"/> class <b>for a single component structure.</b>.
        /// </summary>
        /// <param name="compName">The name of the component.</param>
        /// <param name="compType">The component type.</param>
        /// <param name="dataType">The data type of the component.</param>
        public DataStructure(string compName, ComponentType compType, BasicDataType dataType)
            : this()
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