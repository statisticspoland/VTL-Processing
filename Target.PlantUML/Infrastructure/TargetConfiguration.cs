namespace Target.PlantUML.Infrastructure
{
    using Target.PlantUML.Infrastructure.Interfaces;

    internal class TargetConfiguration : ITargetConfiguration
    {
        public TargetConfiguration(bool horizontal = false, bool showNL = false, bool showDS = false, string arrow = "--")
        {
            this.UseHorizontalView = horizontal;
            this.ShowDataStructure = showDS;
            this.ShowNumberLine = showNL;
            this.Arrow = arrow;
        }

        public bool UseHorizontalView { get; set; }

        public bool ShowDataStructure { get; set; }

        public bool ShowNumberLine { get; set; }

        public string Arrow { get; set; }
    }
}
