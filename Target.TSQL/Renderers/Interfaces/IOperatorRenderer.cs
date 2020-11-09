namespace Target.TSQL.Renderers.Interfaces
{
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;

    /// <summary>
    /// The operator renderer interface.
    /// </summary>
    public interface IOperatorRenderer
    {
        /// <summary>
        /// Renders a TSQL translated code for the expression.
        /// </summary>
        /// <param name="expr">The expression which parameters shall be used to render.</param>
        /// <param name="component">The selected component to assign in the translated code.</param>
        /// <returns>The TSQL translated code.</returns>
        string Render(IExpression expr, StructureComponent component = null);
    }
}
