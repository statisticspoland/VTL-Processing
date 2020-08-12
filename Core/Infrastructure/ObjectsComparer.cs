namespace StatisticsPoland.VtlProcessing.Core.Infrastructure
{
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using System.Collections.Generic;
    using System.Linq;

    public static class ObjectsComparer
    {
        /// <summary>
        /// Checks if the dataset is equal to another. <b>Method skips a names comparison.</b>
        /// </summary>
        /// <param name="instance">The compared dataset.</param>
        /// <param name="structure">The dataset to compare.</param>
        /// <returns>A value specyfing an equality.</returns>
        public static bool EqualsObj(this IDataStructure instance, IDataStructure structure)
        {
            if (instance.IsSingleComponent != structure.IsSingleComponent ||
                instance.Identifiers.Count != structure.Identifiers.Count ||
                instance.Measures.Count != structure.Measures.Count ||
                instance.NonViralAttributes.Count != structure.NonViralAttributes.Count ||
                instance.ViralAttributes.Count != structure.ViralAttributes.Count) return false;

            foreach (StructureComponent component in instance.Components)
            {
                if (structure.Components
                    .Where(c => c.ComponentType == component.ComponentType && c.ValueDomain.DataType == component.ValueDomain.DataType).Count() !=
                    instance.Components
                    .Where(c => c.ComponentType == component.ComponentType && c.ValueDomain.DataType == component.ValueDomain.DataType).Count())
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if the component collection is equal to another. <b>Method skips a names comparison.</b>
        /// </summary>
        /// <param name="instance">The compared collection.</param>
        /// <param name="collection">The collection to compare.</param>
        /// <returns>A value specyfing an equality.</returns>
        public static bool EqualsObj(this ICollection<StructureComponent> instance, ICollection<StructureComponent> collection)
        {
            if (instance.Count != collection.Count) return false;
            for (int i = 0; i < instance.Count; i++)
                if (!instance.ToArray()[i].EqualsObj(collection.ToArray()[i])) return false;

            return true;
        }

        /// <summary>
        /// Checks if the component is equal to another. <b>Method skips a names comparison.</b>
        /// </summary>
        /// <param name="instance">The compared component.</param>
        /// <param name="component">The component to compare.</param>
        /// <returns>A Value specyfing an equality.</returns>
        public static bool EqualsObj(this StructureComponent instance, StructureComponent component)
        {
            if (instance.ComponentType != component.ComponentType ||
                !instance.ValueDomain.EqualsObj(component.ValueDomain)) return false;

            return true;
        }

        /// <summary>
        /// Checks if the value domain is equal to another. <b>Method skips a names comparison.</b>
        /// </summary>
        /// <param name="instance">The cmpared value domain.</param>
        /// <param name="valueDomain">The value domain to compare.</param>
        /// <param name="allNumsEqual">Specifies if an integer data type is equal to a number data type.</param>
        /// <param name="allEqualsToNull">Specifies if a none data type is equal to other.</param>
        /// <returns>A value specyfing equality.</returns>
        public static bool EqualsObj(this ValueDomain instance, ValueDomain valueDomain, bool allNumsEqual = false, bool allEqualsToNull = false)
        {
            if (!instance.DataType.EqualsObj(valueDomain.DataType, allNumsEqual, allEqualsToNull) || instance.Signature != valueDomain.Signature) return false;
            return true;
        }

        /// <summary>
        /// Checks if the data type is equal to another.
        /// </summary>
        /// <param name="instance">The compared data type.</param>
        /// <param name="dataType">The data type to compare.</param>
        /// <param name="allNumsEqual">Specifies if an integer data type is equal to a number data type.</param>
        /// <param name="allEqualsToNull">Specifies if a none data type is equal to other.</param>
        /// <returns>A value specyfing equality.</returns>
        public static bool EqualsObj(this BasicDataType instance, BasicDataType dataType, bool allNumsEqual = true, bool allEqualsToNull = true)
        {
            if (instance != dataType
                && (!allNumsEqual || (allNumsEqual &&
                !(instance == BasicDataType.Integer && dataType == BasicDataType.Number) &&
                !(instance == BasicDataType.Number && dataType == BasicDataType.Integer)))
                && (!allEqualsToNull || (allEqualsToNull && instance != BasicDataType.None && dataType != BasicDataType.None))) return false;

            return true;
        }
    }
}
