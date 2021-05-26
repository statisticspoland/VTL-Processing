namespace StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure
{
    using Interfaces;

    internal class TargetConfiguration : ITargetConfiguration
    {
        public TargetConfiguration( bool useComments = false)
        {
            this.AttributePropagationAlgorithm = new AttributePropagationAlgorithm();
            this.UseComments = useComments;
        }

        public bool UseComments { get; set; }

        public IAttributePropagationAlgorithm AttributePropagationAlgorithm { get; set; }
    }
}
