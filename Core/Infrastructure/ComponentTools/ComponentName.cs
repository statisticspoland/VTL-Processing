namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.ComponentTools
{
    /// <summary>
    /// Extension methods for component names.
    /// </summary>
    public static class ComponentName
    {
        /// <summary>
        /// Gets the name without an alias.
        /// </summary>
        /// <param name="name">The name of a component.</param>
        /// <returns>The name withot an alias.</returns>
        public static string GetNameWithoutAlias(this string name)
        {
            if (name?.Split('#').Length == 2)
                return name.Split('#')[1];
            return name;
        }
    }
}
