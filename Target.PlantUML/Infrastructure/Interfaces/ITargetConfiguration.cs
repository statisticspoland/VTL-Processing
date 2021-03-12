namespace StatisticsPoland.VtlProcessing.Target.PlantUML.Infrastructure.Interfaces
{
    public interface ITargetConfiguration
    {
        bool UseHorizontalView { get; set; }
        
        bool ShowDataStructure { get; set; }
        
        bool ShowNumberLine { get; set; }
        
        string Arrow { get; set; }

        ExpressionsType ExprType { get; set; }
    }
}
