namespace StatisticsPoland.VtlProcessing.Core.Tests.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The testing data type.
    /// </summary>
    public enum TestExprType
    {
        None = 0,
        Integer = 1,
        Number = 2,
        String = 3,
        Boolean = 4,
        Time = 5,
        Date = 6,
        TimePeriod = 7,
        Duration = 8,     
        IntsDataset = 9,
        NumbersDataset = 10,
        StringsDataset = 11,
        BoolsDataset = 12,
        TimesDataset = 13,
        DatesDataset = 14,
        TimePeriodsDataset = 15,
        DurationsDataset = 16,
        NonesDataset = 17,
        MixedIntNumDataset = 18,
        MixedNumStrDataset = 19,
        MixedNoneIntDataset = 20,
        MixedNoneNumDataset = 21,
        MixedNoneStrDataset = 22,
        MixedNoneBoolDataset = 23,
        MixedNoneTimeDataset = 24,
        MixedNoneDateDataset = 25,
        MixedNoneTimePerDataset = 26,
        MixedNoneDurDataset = 27
    }

    /// <summary>
    /// The tessting data type extensions.
    /// </summary>
    public static class TestExprTypeHelper
    {
        /// <summary>
        /// Gets all combinations of testing data types.
        /// </summary>
        /// <param name="list">The list of testing data types.</param>
        /// <param name="length">The length of combinations.</param>
        /// <returns>All combinations of testing data types.</returns>
        public static TestExprType[][] GetCombinations(this IEnumerable<TestExprType> list, int length)
        {
            if (length == 1) return list.Select(t => new TestExprType[] { t }).ToArray();

            return GetCombinations(list, length - 1)
                .SelectMany(t => list, (t1, t2) => t1.Concat(new TestExprType[] { t2 }).ToArray()).ToArray();
        }

        /// <summary>
        /// Removes items, which items match any item of the checked array.
        /// </summary>
        /// <param name="instance">The main array.</param>
        /// <param name="withoutArray">The checked array.</param>
        /// <returns>Array of test expression types.</returns>
        public static TestExprType[][] Without(this TestExprType[][] instance, TestExprType[][] withoutArray)
        {
            if (instance[0].Length != withoutArray[0].Length) throw new Exception();

            List<TestExprType[]> removedItems = new List<TestExprType[]>();
            foreach (TestExprType[] item in instance)
            {
                foreach (TestExprType[] withoutItem in withoutArray)
                {
                    bool toRemove = true;
                    for (int i = 0; i < item.Length; i++)
                    {
                        if (item[i] != withoutItem[i])
                        {
                            toRemove = false;
                            break;
                        }
                    }

                    if (toRemove)
                    {
                        removedItems.Add(item);
                        break;
                    }
                }
            }

            return instance.Except(removedItems).ToArray();
        }

        /// <summary>
        /// Gets the variable name of a basic data type.
        /// </summary>
        /// <param name="instance">The basic data type to get variable name from.</param>
        /// <returns>The variable name of the basic data type.</returns>
        public static string GetVariableName(this TestExprType instance)
        {
            switch (instance)
            {
                case TestExprType.Integer: return "int_var";
                case TestExprType.Number: return "num_var";
                case TestExprType.String: return "string_var";
                case TestExprType.Boolean: return "bool_var";
                case TestExprType.Time: return "time_var";
                case TestExprType.Date: return "date_var";
                case TestExprType.TimePeriod: return "period_var";
                default: throw new Exception("Unknown basic data type.");
            }
        }
    }
}
