namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.JoinBranches
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.JoinBranches.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;

    /// <summary>
    /// The "calc" branch of the "join" operator expression builded from a dataset expression.
    /// </summary>
    public sealed class CalcBranch : IJoinBranch
    {
        private readonly IExpressionFactory exprFactory;
        private IExpressionTextGenerator exprTextGen;

        /// <summary>
        /// Inittializes a new instance of the <see cref="CalcBranch"/> class.
        /// </summary>
        /// <param name="exprFactory">The expression factory.</param>
        /// <param name="exprTextGen">The epression text generator.</param>
        public CalcBranch(IExpressionFactory exprFactory, IExpressionTextGenerator exprTextGen)
        {
            this.exprFactory = exprFactory;
            this.exprTextGen = exprTextGen;
        }

        public string Signature => "calc";

        public IExpression Build(IExpression datasetExpr)
        {
            /*
            This method is using mainly for aggregate invocation function "count". Example: DS_r := count(X group by Id1);
            The method can be used to transform "aggr" expressions to "calc" expressions instead of single "apply" expression.
            */

            IExpression calcBranch = this.exprFactory.GetExpression("calc", ExpressionFactoryNameTarget.OperatorSymbol);
            calcBranch.OperatorDefinition.Keyword = "Aggr Built";

            for (int i = 0; i < datasetExpr.Operands["ds_1"].Structure.Measures.Count; i++)
            {
                StructureComponent measure = datasetExpr.Operands["ds_1"].Structure.Measures[i];
                IExpression calcExpr = this.exprFactory.GetExpression("calcExpr", ExpressionFactoryNameTarget.OperatorSymbol);
                IExpression aggrFunctionExpr = this.exprFactory.GetExpression(datasetExpr.OperatorSymbol, ExpressionFactoryNameTarget.OperatorSymbol);
                IExpression compExpr1 = this.exprFactory.GetExpression("comp", ExpressionFactoryNameTarget.OperatorSymbol);
                IExpression compExpr2 = this.exprFactory.GetExpression("comp", ExpressionFactoryNameTarget.OperatorSymbol);

                calcBranch.LineNumber = datasetExpr.LineNumber;
                calcExpr.LineNumber = datasetExpr.LineNumber;
                aggrFunctionExpr.LineNumber = datasetExpr.LineNumber;
                compExpr1.LineNumber = datasetExpr.LineNumber;
                compExpr2.LineNumber = datasetExpr.LineNumber;

                compExpr1.ExpressionText = measure.BaseComponentName;
                compExpr2.ExpressionText = measure.BaseComponentName;

                aggrFunctionExpr.AddOperand("ds_1", compExpr1);
                calcExpr.AddOperand("ds_1", compExpr2);
                calcExpr.AddOperand("ds_2", aggrFunctionExpr);

                calcExpr.OperatorDefinition.Keyword = "measure";
                calcBranch.AddOperand($"ds_{i + 1}", calcExpr);

                if (datasetExpr.OperatorSymbol == "count")
                {
                    compExpr2.ExpressionText = "int_var";
                    aggrFunctionExpr.ExpressionText = "count()";
                    aggrFunctionExpr.Operands.Clear();
                    this.exprTextGen.Generate(calcExpr);
                    break;
                }
            }

            this.exprTextGen.GenerateRecursively(calcBranch);
            return calcBranch;
        }
    }
}
