namespace Target.TSQL.Infrastructure
{
    /// <summary>
    /// The temporary tables data.
    /// </summary>
    public class TemporaryTables
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TemporaryTables"/> class.
        /// </summary>
        public TemporaryTables()
        {
            this.Count = 0;
            this.Name = "#VTLProcessingTmp";
        }

        /// <summary>
        /// Gets the count of used temporary tables.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets the name of temporary tables.
        /// </summary>
        public string Name { get; set; }
    }
}
