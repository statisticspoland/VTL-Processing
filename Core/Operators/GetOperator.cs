namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;

    /// <summary>
    /// The "Get" operator definition.
    /// </summary>
    [OperatorSymbolAttribute("get")]
    public class GetOperator : IOperatorDefinition
    {
        private readonly IDataModelProvider _dataModel;

        /// <summary>
        /// Initialises a new instance of the <see cref="GetOperator"/> class.
        /// </summary>
        public GetOperator(IDataModelProvider dataModel)
        {
            this._dataModel = dataModel;
        }

        public string Name => "Get";

        public string Symbol { get; set; } = "get";

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            IDataStructure structure = this._dataModel.GetDatasetStructure(expression.ExpressionText);

            string[] split = expression.ExpressionText.Split(@"\");
            if (split.Length == 1)
                expression.ExpressionText = $@"{this._dataModel.DefaultNamespace}\{expression.ExpressionText}";

            return structure;
        }
    }
}
