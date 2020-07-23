namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Attribute allows to find the definition of the operator with the specified symbol.
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class OperatorSymbol : Attribute
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="OperatorSymbol"/> class.
        /// </summary>
        /// <param name="symbols">Symbol of the operator.</param>
        public OperatorSymbol(params string[] symbols)
        {
            this.Symbols = symbols;
        }

        /// <summary>
        /// Gets the symbols collection.
        /// </summary>
        public ICollection<string> Symbols { get; private set; }
    }
}
