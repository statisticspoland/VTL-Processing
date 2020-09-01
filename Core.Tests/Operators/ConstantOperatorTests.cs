namespace StatisticsPoland.VtlProcessing.Core.Tests.Operators
{
    using Moq;
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System;
    using Xunit;

    public class ConstantOperatorTests
    {
        private readonly OperatorResolver opResolver;

        public ConstantOperatorTests()
        {
            Mock<OperatorResolver> opResolverMock = new Mock<OperatorResolver>();
            opResolverMock.Setup(o => o("const")).Returns(() => { return new ConstantOperator(ModelResolvers.DsResolver); });

            this.opResolver = opResolverMock.Object;
        }

        [Theory]
        [InlineData(TestExprType.Integer, "1")]
        [InlineData(TestExprType.Number, "1.0")]
        [InlineData(TestExprType.String, "\"Test\"")]
        [InlineData(TestExprType.Boolean, "true")]
        [InlineData(TestExprType.Time, "t\"2020-03-23/2020-04-12\"")]
        [InlineData(TestExprType.Date, "t\"2020-06-20\"")]
        [InlineData(TestExprType.TimePeriod, "t\"2010Q2\"")]
        [InlineData(TestExprType.Duration, "t\"M\"")]
        [InlineData(TestExprType.None, "null")]
        public void GetOutputStructure_CorrectExpr_CorrectScalarStructure(TestExprType expectedType, string exprText)
        {
            IExpression constExpr = ModelResolvers.ExprResolver();
            constExpr.ExpressionText = exprText;

            constExpr.OperatorDefinition = this.opResolver("const");

            IDataStructure dataStructure = constExpr.OperatorDefinition.GetOutputStructure(constExpr);

            Assert.True(TestExprFactory.GetExpression(expectedType).Structure.EqualsObj(dataStructure));
        }

        [Theory]
        [InlineData("1 + 2")]
        [InlineData("1,0")]
        [InlineData("Test")]
        [InlineData("t\"Test\"")]
        [InlineData("NULL")]
        public void GetOutputStructure_WrongExpr_ThrowsException(string exprText)
        {
            IExpression constExpr = ModelResolvers.ExprResolver();
            constExpr.ExpressionText = exprText;

            constExpr.OperatorDefinition = this.opResolver("const");

            Assert.ThrowsAny<VtlOperatorError>(() => { constExpr.OperatorDefinition.GetOutputStructure(constExpr); });
        }
    }
}
