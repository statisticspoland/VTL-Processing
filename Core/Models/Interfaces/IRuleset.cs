namespace StatisticsPoland.VtlProcessing.Core.Models.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// The VTL 2.0 ruleset representation interface.
    /// </summary>
    public interface IRuleset
    {
        /// <summary>
        /// Gets or sets the name of the ruleset.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the ruleset defining text.
        /// </summary>
        string RulesetText { get; set; }

        /// <summary>
        /// Gets or sets the mapped variables collection.
        /// </summary>
        Dictionary<string, string> Variables { get; set; }

        /// <summary>
        /// Gets or sets the mapped value domains collection.
        /// </summary>
        Dictionary<string, ValueDomain> ValueDomains { get; set; }

        /// <summary>
        /// Gets or sets the rules dictionary.
        /// </summary>
        Dictionary<string, IRuleExpression> Rules { get; set; }

        /// <summary>
        /// Gets or sets the rules collection. <br />
        /// The field is assignable, but methods changing the collection instance shall not work.
        /// </summary>
        ICollection<IRuleExpression> RulesCollection { get; set; }

        /// <summary>
        /// Gets or sets the structure.
        /// </summary>
        IDataStructure Structure { get; set; }

        /// <summary>
        /// Infers types of the ruleset structure.
        /// </summary>
        void InferStructure();
    }
}
