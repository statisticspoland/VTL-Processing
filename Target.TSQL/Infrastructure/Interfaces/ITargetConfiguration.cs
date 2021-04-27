namespace StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure.Interfaces
{
    /// <summary>
    /// The target configuration interface.
    /// </summary>
    public interface ITargetConfiguration
    {
        /// <summary>
        /// Gets or sets the value specifying if comments shall be appended in the TSQL rendered code.
        /// </summary>
        bool UseComments { get; set; }

        IAttributePropagationAlgorithm AttributePropagationAlgorithm { get; set; }
    }
}
