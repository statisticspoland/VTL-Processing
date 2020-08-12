namespace StatisticsPoland.VtlProcessing.Core.MiddleEnd.Tools
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;

    /// <summary>
    /// Methods for numeric structures.
    /// </summary>
    public static class NumericStructure
    {
        /// <summary>
        /// Check if a given structure is a numeric structure.
        /// </summary>
        /// <param name="structure">The structure to check.</param>
        /// <param name="allowNulls">Specifies if nulls are equal to numerics.</param>
        /// <returns>The value specyfing if a given structure is a numeric structure.</returns>
        public static bool IsNumericStructure(this IDataStructure structure, bool allowNulls = false)
        {
            if (structure.IsSingleComponent)
            {
                if (!structure.Components[0].ValueDomain.DataType.In(BasicDataType.Integer, BasicDataType.Number) &&
                    (!allowNulls || (allowNulls && structure.Components[0].ValueDomain.DataType != BasicDataType.None))) return false;
            }
            else
            {
                foreach (StructureComponent measure in structure.Measures)
                {
                    if (!measure.ValueDomain.DataType.In(BasicDataType.Integer, BasicDataType.Number) &&
                    (!allowNulls || (allowNulls && measure.ValueDomain.DataType != BasicDataType.None))) return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Gets a fused structure from given dataset and scalar.
        /// </summary>
        /// <param name="dataset">The dataset.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>The data structure.</returns>
        public static IDataStructure GetDatasetScalarMixedStructure(IDataStructure dataset, IDataStructure scalar)
        {
            IDataStructure ds = dataset.GetCopy();
            foreach (StructureComponent measure in ds.Measures)
            {
                if (scalar.Components[0].ValueDomain.DataType == BasicDataType.Number)
                    measure.ValueDomain = new ValueDomain(BasicDataType.Number);
                if (measure.ValueDomain.DataType == BasicDataType.None)
                    measure.ValueDomain = new ValueDomain(BasicDataType.Integer);
            }

            return ds;
        }

        /// <summary>
        /// Gets a fused structure from given super data set and sub data set.
        /// </summary>
        /// <param name="superset">The superdataset.</param>
        /// <param name="subset">The subset.</param>
        /// <returns>The data structure.</returns>
        public static IDataStructure GetDatasetsMixedStructure(IDataStructure superset, IDataStructure subset)
        {
            IDataStructure result = superset.GetCopy();
            result.Measures.Clear();

            for (int i = 0; i < superset.Measures.Count; i++)
            {
                StructureComponent measure = new StructureComponent(superset.Measures[i].ValueDomain.DataType, superset.Measures[i].ComponentName);
                result.Measures.Add(measure);
                if (subset.Measures[i].ValueDomain.DataType == BasicDataType.Number) measure.ValueDomain = new ValueDomain(BasicDataType.Number);
                if (measure.ValueDomain.DataType == BasicDataType.None)
                    measure.ValueDomain = new ValueDomain(BasicDataType.Integer);
            }

            return result;
        }
    }
}
