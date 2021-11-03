namespace StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure.Interfaces
{
    /// <summary>
    /// The interface of the TSQL target renderer configuration.
    /// </summary>
    public interface ITargetConfiguration
    {
        /// <summary>
        /// Gets or sets the value specifying if comments shall be appended in the TSQL rendered code.
        /// </summary>
        bool UseComments { get; set; }

        /// <summary>
        /// The attribute propagation algorithm.
        /// </summary>
        IAttributePropagationAlgorithm AttributePropagationAlgorithm { get; set; }
    }
}
