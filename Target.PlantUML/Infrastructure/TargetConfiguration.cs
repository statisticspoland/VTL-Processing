namespace StatisticsPoland.VtlProcessing.Target.PlantUML.Infrastructure
{
    using Interfaces;

    internal class TargetConfiguration : ITargetConfiguration
    {
        public TargetConfiguration(
            ExpressionsType exprType = ExpressionsType.Standard,
            bool horizontal = false, 
            bool showNL = false,
            bool showDS = false,
            string arrow = "--")
        {
            this.ExprType = exprType;
            this.UseHorizontalView = horizontal;
            this.ShowDataStructure = showDS;
            this.ShowNumberLine = showNL;
            this.Arrow = arrow;
        }

        public bool UseHorizontalView { get; set; }

        public bool ShowDataStructure { get; set; }

        public bool ShowNumberLine { get; set; }

        public string Arrow { get; set; }

        public ExpressionsType ExprType { get; set; }
    }
}
