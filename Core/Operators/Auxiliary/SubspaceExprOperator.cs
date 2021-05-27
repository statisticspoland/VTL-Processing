namespace StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;

    /// <summary>
    /// The "Subspace expression" operator definition.
    /// </summary>
    [OperatorSymbolAttribute("subExpr")]
    public class SubspaceExprOperator : IOperatorDefinition
    {
        public string Name => "Subspace expression";

        public string Symbol { get; set; } = "="; // it has to be "='

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            IExpression compExpr = expression.Operands["ds_1"];
            IExpression constExpr = expression.Operands["ds_2"];

            IDataStructure structure = compExpr.Structure.GetCopy();
            structure.Components[0].ValueDomain = new ValueDomain(compExpr.Structure.Components[0].ValueDomain.DataType);

            if (!structure.Components[0].ValueDomain.DataType.EqualsObj(constExpr.Structure.Components[0].ValueDomain.DataType))
                throw new VtlOperatorError(expression, this.Name, "Data types of parameters are not the same.");

            return structure;
        }
    }
}