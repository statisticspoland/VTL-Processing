namespace StatisticsPoland.VtlProcessing.Core.MiddleEnd.Modifiers
{
    using Microsoft.Extensions.Logging;
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.MiddleEnd.Modifiers.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Applier of a complex transformation of a VTL 2.0 transformation schema.
    /// </summary>
    public class SchemaModifiersApplier : ISchemaModifiersApplier
    {
        private readonly IEnumerable<ISchemaModifier> modifiers;
        private readonly ILogger<ISchemaModifiersApplier> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SchemaModifiersApplier"/> class.
        /// </summary>
        /// <param name="mods">The collection of modifiers to apply.</param>
        /// <param name="logger">The errors logger.</param>
        public SchemaModifiersApplier(IEnumerable<ISchemaModifier> mods, ILogger<ISchemaModifiersApplier> logger = null)
        {
            this.logger = logger;
            this.modifiers = mods;
        }

        /// <summary>
        /// Performs processing of the schema.
        /// </summary>
        /// <param name="schema">The schema object to be processed.</param>
        public void Process(ITransformationSchema schema)
        {
            foreach(ISchemaModifier mod in this.modifiers)
            {
                try
                {
                    mod.Modify(schema);
                }
                catch (VtlOperatorError ex)
                {
                    this.logger?.LogError(ex, ex.Message);
                }
                catch (Exception ex)
                {
                    this.logger?.LogCritical(ex, ex.Message);
                }
            }
        }
    }
}
