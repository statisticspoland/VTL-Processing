namespace StatisticsPoland.VtlProcessing.Target.PlantUML.Infrastructure.Interfaces
{
    /// <summary>
    /// The interface of the PlantUML target renderer configuration.
    /// </summary>
    public interface ITargetConfiguration
    {
        /// <summary>
        /// The value specifying if a graphic presentation method of a transformation schema is a horizontal view.
        /// </summary>
        bool UseHorizontalView { get; set; }

        /// <summary>
        /// The value specifying if to show objects of a graphic presentation of a transformation schema, describing data structures of expressions.
        /// </summary>
        bool ShowDataStructure { get; set; }

        /// <summary>
        /// The value specifying if to show fields of expressions of a graphic presentation of a transformation schema, describing a line number of an expression.
        /// </summary>
        bool ShowNumberLine { get; set; }

        /// <summary>
        /// The style of line connections of a graphic presentation of a transformation schema. 
        /// </summary>
        string LineConnection { get; set; }

        /// <summary>
        /// The type of expressions to show in a graphic presentation of a transformation schema.
        /// </summary>
        ExpressionsType ExprType { get; set; }
    }
}
