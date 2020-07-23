namespace StatisticsPoland.VtlProcessing.Core.Models.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// The VTL 2.0 transformation schema representation interface.
    /// </summary>
    public interface ITransformationSchema
    {
        /// <summary>
        /// Gets the assignment objects collection.
        /// </summary>
        AssignmentObjectCollection AssignmentObjects { get; }

        /// <summary>
        /// Gets the operators collection used in the transformation schema.
        /// </summary>
        ICollection<string> UsedOperators { get; }

        /// <summary>
        /// Gets the VTL expressions collection.
        /// </summary>
        /// <returns>The VTL expressions collection.</returns>
        ICollection<IExpression> GetExpressions();
    }
}
