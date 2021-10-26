namespace StatisticsPoland.VtlProcessing.Target.PlantUML.Infrastructure.Interfaces
{
    /// <summary>
    /// The builder interface for the PlantUML target renderer.
    /// </summary>
    public interface ITargetBuilder
    {
        /// <summary>
        /// Changes a graphic presentation method of a transformation schema to horizontal view.
        /// </summary>
        /// <returns>The PlantUML target renderer builder.</returns>
        ITargetBuilder UseHorizontalView();

        /// <summary>
        /// Adds to a graphic presentation of a transformation schema the objects describing data structures of expressions.
        /// </summary>
        /// <returns>The PlantUML target renderer builder.</returns>
        ITargetBuilder AddDataStructureObject();

        /// <summary>
        /// Changes lines of a graphic presentation of a transformation schema to arrows pointing by the method "from the root to leafs".
        /// </summary>
        /// <returns>The PlantUML target renderer builder.</returns>
        ITargetBuilder UseArrowFirstToLast();

        /// <summary>
        /// Changes lines of a graphic presentation of a transformation schema to arrows pointing by the method "from leafs to the root".
        /// </summary>
        /// <returns>The PlantUML target renderer builder.</returns>
        ITargetBuilder UseArrowLastToFirst();

        /// <summary>
        /// Adds to graphic presentation of a transformation schema expressions a field describing a line number of an expression.
        /// </summary>
        /// <returns>The PlantUML target renderer builder.</returns>
        ITargetBuilder ShowNumberLine();

        /// <summary>
        /// Changes a graphic presentation method of a transformation schema to a rulesets expressions view.
        /// </summary>
        /// <returns>The PlantUML target renderer builder.</returns>
        ITargetBuilder UseRuleExpressionsModel();
    }
}
