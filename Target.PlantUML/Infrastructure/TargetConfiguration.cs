namespace StatisticsPoland.VtlProcessing.Target.PlantUML.Infrastructure
{
    using Interfaces;

    /// <summary>
    /// The PlantUML target renderer configuration.
    /// </summary>
    internal class TargetConfiguration : ITargetConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TargetConfiguration"/> class.
        /// </summary>
        /// <param name="exprType">The expressions type.</param>
        /// <param name="horizontal">TThe value specifying if a graphic presentation method of a transformation schema has to be a horizontal view.</param>
        /// <param name="showNL">The value specifying if to show fields of expressions of a graphic presentation of a transformation schema, describing a line number of an expression.</param>
        /// <param name="showDS">The value specifying if to show objects of a graphic presentation of a transformation schema, describing data structures of expressions.</param>
        /// <param name="lineConnection">The style of line connections of a graphic presentation of a transformation schema. </param>
        public TargetConfiguration(
            ExpressionsType exprType = ExpressionsType.Standard,
            bool horizontal = false, 
            bool showNL = false,
            bool showDS = false,
            string lineConnection = "--")
        {
            this.ExprType = exprType;
            this.UseHorizontalView = horizontal;
            this.ShowDataStructure = showDS;
            this.ShowNumberLine = showNL;
            this.LineConnection = lineConnection;
        }

        public bool UseHorizontalView { get; set; }

        public bool ShowDataStructure { get; set; }

        public bool ShowNumberLine { get; set; }

        public string LineConnection { get; set; }

        public ExpressionsType ExprType { get; set; }
    }
}
