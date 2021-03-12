namespace StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure.Attributes
{
    using System;

    /// <summary>
    /// Attribute allows to find the definition of the operator renderer with the specified symbol.
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class OperatorRendererSymbol : Attribute
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="OperatorRendererSymbol"/> class.
        /// </summary>
        /// <param name="symbols">The symbol of the operator renderer used by a "join" operator.</param>
        public OperatorRendererSymbol(params string[] symbols)
        {
            this.Symbols = symbols;
        }

        /// <summary>
        /// Gets symbols array.
        /// </summary>
        public string[] Symbols { get; private set; }
    }
}
