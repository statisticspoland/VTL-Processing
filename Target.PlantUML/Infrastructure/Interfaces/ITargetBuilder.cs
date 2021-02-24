namespace Target.PlantUML.Infrastructure.Interfaces
{
    using Microsoft.Extensions.DependencyInjection;

    public interface ITargetBuilder
    {
        IServiceCollection Services { get; }

        ITargetBuilder UseHorizontalView();

        ITargetBuilder AddDataStructureObject();

        ITargetBuilder UseArrowFirstToLast();

        ITargetBuilder UseArrowLastToFirst();

        ITargetBuilder ShowNumberLine();

        ITargetBuilder UseRuleExpressionsModel();
    }
}
