namespace StatisticsPoland.VtlProcessing.Target.PlantUML.Infrastructure.Interfaces
{
    public interface ITargetBuilder
    {
        ITargetBuilder UseHorizontalView();

        ITargetBuilder AddDataStructureObject();

        ITargetBuilder UseArrowFirstToLast();

        ITargetBuilder UseArrowLastToFirst();

        ITargetBuilder ShowNumberLine();

        ITargetBuilder UseRuleExpressionsModel();
    }
}
