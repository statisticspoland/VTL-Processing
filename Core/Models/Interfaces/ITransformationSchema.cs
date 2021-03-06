﻿namespace StatisticsPoland.VtlProcessing.Core.Models.Interfaces
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
        /// Gets the rulesets collection.
        /// </summary>
        ICollection<IRuleset> Rulesets { get; }

        /// <summary>
        /// Gets the VTL expressions collection.
        /// </summary>
        /// <returns>The VTL expressions collection.</returns>
        ICollection<IExpression> GetExpressions();
    }
}
