namespace StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure
{
    using Interfaces;

    /// <summary>
    /// The TSQL target renderer configuration.
    /// </summary>
    internal class TargetConfiguration : ITargetConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TargetConfiguration"/> class.
        /// </summary>
        /// <param name="useComments">The value specifying if comments shall be appended in the TSQL rendered code.</param>
        public TargetConfiguration( bool useComments = false)
        {
            this.AttributePropagationAlgorithm = new AttributePropagationAlgorithm();
            this.UseComments = useComments;
        }

        public bool UseComments { get; set; }

        public IAttributePropagationAlgorithm AttributePropagationAlgorithm { get; set; }
    }
}
