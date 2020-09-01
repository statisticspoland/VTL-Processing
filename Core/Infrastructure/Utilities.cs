namespace StatisticsPoland.VtlProcessing.Core.Infrastructure
{
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using System;

    /// <summary>
    /// Class with all-purpose methods and fields.
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Checks if instance is equal to any of params objects.
        /// </summary>
        /// <param name="instance">Checking instance.</param>
        /// <param name="args">objects to compare with the instance</param>
        /// <returns>Result of comparing.</returns>
        public static bool In<T>(this T instance, params T[] args)
        {
            foreach (T arg in args)
            {
                if ((instance == null && arg == null) || instance?.Equals(arg) == true)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets name of <see cref="Enum"/> field.
        /// </summary>
        /// <param name="instance">Enum field to get name from.</param>
        /// <returns>Name of enum field.</returns>
        public static string GetName(this Enum instance)
        {
            return Enum.GetName(instance.GetType(), instance);
        }

        /// <summary>
        /// Gets variable name of basic data type.
        /// </summary>
        /// <param name="instance">Basic data type to get variable name from.</param>
        /// <returns>Variable name of basic data type.</returns>
        public static string GetVariableName(this BasicDataType instance)
        {
            switch (instance)
            {
                case BasicDataType.Integer: return "int_var";
                case BasicDataType.Number: return "num_var"; 
                case BasicDataType.String: return "string_var"; 
                case BasicDataType.Boolean: return "bool_var"; 
                case BasicDataType.Time: return "time_var"; 
                case BasicDataType.Date: return "date_var"; 
                case BasicDataType.TimePeriod: return "period_var"; 
                case BasicDataType.Duration: return "duration_var";
                case BasicDataType.None: return "NULL";
                default: throw new Exception("Unknown data type.");
            }
        }
    }
}