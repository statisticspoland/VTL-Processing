namespace StatisticsPoland.VtlProcessing.Core.MiddleEnd.Utilities
{
    /// <summary>
    /// The class containing operator collections that dataset expressions of one of them operator are convertible to "join" operator expressions.
    /// </summary>
    public static class JoinOperators
    {
        /// <summary>
        /// Gets operator array that expressions of one of them operator are convertible to "join" operator expressions.
        /// </summary>
        public static string[] Operators => 
            new string[] { "+", "-", "*", "/", "||", "=", "<>", "<", "<=", ">", ">=", "and", "or", "xor", "mod", "datasetClause", "if",
                "count", "min", "max", "median", "sum", "avg", "stddev_pop", "stddev_samp", "var_pop", "var_samp", "first_value", "last_value", "lag", "rank", "ratio_to_report", "lead"};

        /// <summary>
        /// Gets comparison operators array that expressions of one of them operator are convertible to "join" operator expressions.
        /// </summary>
        public static string[] ComparisonOperators => new string[] { "=", "<>", "<", "<=", ">", ">=", };
    }
}
