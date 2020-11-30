namespace StatisticsPoland.VtlProcessing.Core.Tests.InfrastructureTests
{
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using Xunit;

    public class ExpressionFactoryTests
    {

        [Theory]
        [InlineData("The name")]
        [InlineData("Testing")]
        public void GetExpression_ResultName_ExprWithName(string name)
        {
            IExpression expr = TestExprFactory.GetExpression(name, ExpressionFactoryNameTarget.ResultName);
            
            Assert.Equal(name, expr.ResultName);
            Assert.Null(expr.ExpressionText);
            Assert.Null(expr.OperatorSymbol);
            Assert.Null(expr.OperatorDefinition);
        }

        [Theory]
        [InlineData("+")]
        [InlineData("=")]
        public void GetExpression_OperatorSymbol_ExprWithNameAndOpDefinition(string opSymbol)
        {
            IExpression expr = TestExprFactory.GetExpression(opSymbol, ExpressionFactoryNameTarget.OperatorSymbol);

            Assert.Equal(opSymbol == "+" ? "Arithmetic" : "Comparison", expr.ResultName);
            Assert.Null(expr.ExpressionText);
            Assert.Equal(opSymbol, expr.OperatorSymbol);
            Assert.NotNull(expr.OperatorDefinition);
        }
    }
}
