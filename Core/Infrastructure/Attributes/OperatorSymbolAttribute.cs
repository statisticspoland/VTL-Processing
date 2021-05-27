namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Attribute allowing to find a definition of an operator with the specified symbol.
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class OperatorSymbolAttribute : Attribute
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="OperatorSymbolAttribute"/> class.
        /// </summary>
        /// <param name="symbols">The symbol of the operator.</param>
        public OperatorSymbolAttribute(params string[] symbols)
        {
            this.Symbols = symbols;
        }

        /// <summary>
        /// Gets the symbol collection.
        /// </summary>
        public ICollection<string> Symbols { get; private set; }
    }
}
