﻿namespace StatisticsPoland.VtlProcessing.Core.Models.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// The single VTL 2.0 "join" operator expression representation interface.
    /// </summary>
    public interface IJoinExpression: IExpression
    {
        /// <summary>
        /// Gets or sets the basic structure.
        /// </summary>
        IDataStructure BasicStructure { get; set; }

        /// <summary>
        /// Gets the structure of the alias, whose structure is a superset of others.
        /// </summary>
        /// <returns>The structure of the alias, whose structure is a superset of others.</returns>
        IDataStructure GetSupersetAliasStructure();

        /// <summary>
        /// Gets the structure of the alias, whose structure is a subset of others.
        /// </summary>
        /// <returns>The structure of the alias, whose structure is a subset of others.</returns>
        IDataStructure GetSubsetAliasStructure();

        /// <summary>
        /// Checks if the expression is part of a branch with a given name.
        /// </summary>
        /// <param name="branchName">The name of the branch.</param>
        /// <param name="expr">The expression.</param>
        /// <returns>Value specifying if the expression is part of a branch with a given name.</returns>
        bool IsPartOfBranch(string branchName, IExpression expr);

        /// <summary>
        /// Gets aliases signatures whose structures contains a component with a given name. <br />
        /// The signature expression must be a "get", "ref", or "join" operator expression.
        /// </summary>
        /// <param name="compName">The name of the component.</param>
        /// <returns>Names of signatures.</returns>
        string[] GetAliasesSignatures(string compName = null);

        /// <summary>
        /// Gets the alias expression with a given name.
        /// </summary>
        /// <param name="name">The name of the alias.</param>
        /// <returns>The alias expression.</returns>
        IExpression GetAliasExpression(string name);
    }
}
