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
        /// Checks if an instance is equal to any of params objects.
        /// </summary>
        /// <param name="instance">The checking instance.</param>
        /// <param name="args">Objects to compare with the instance.</param>
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
        /// Returns a string of whitespaces in a given amount.
        /// </summary>
        /// <param name="space">The amount of whitespaces.</param>
        /// <returns>String of whitespaces.</returns>
        public static string Space(int space)
        {
            if (space < 0)
            {
                throw new ArgumentOutOfRangeException("space", "Podana liczba musi być nieujemna.");
            }

            string result = string.Empty;
            for (int i = 0; i < space; i++)
            {
                result += " ";
            }

            return result;
        }

        /// <summary>
        /// Gets the name of an <see cref="Enum"/> field.
        /// </summary>
        /// <param name="instance">The enum field to get the name from.</param>
        /// <returns>The name of an enum field.</returns>
        public static string GetName(this Enum instance)
        {
            return Enum.GetName(instance.GetType(), instance);
        }

        /// <summary>
        /// Gets the variable name of a basic data type.
        /// </summary>
        /// <param name="instance">The basic data type to get a variable name from.</param>
        /// <returns>The variable name of a basic data type.</returns>
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
                default: throw new ArgumentOutOfRangeException("instance", "Unknown data type.");
            }
        }
    }
}