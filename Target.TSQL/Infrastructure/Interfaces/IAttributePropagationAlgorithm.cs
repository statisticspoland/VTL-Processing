namespace StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure.Interfaces
{
    using StatisticsPoland.VtlProcessing.Core.Models;
    using System.Collections.Generic;

    /// <summary>
    /// The attribute propagation algorithm interface.
    /// </summary>
    public interface IAttributePropagationAlgorithm
    {
        /// <summary>
        /// Propagates an attribute from a given aliases collection and returns a TSQL translated code..
        /// </summary>
        /// <param name="attribute">The attribute to propagate.</param>
        /// <param name="aliases">The aliases collection.</param>
        /// <returns>The TSQL translated code.</returns>
        string Propagate(StructureComponent attribute, ICollection<string> aliases);
    }
}
