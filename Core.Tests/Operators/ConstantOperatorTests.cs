namespace StatisticsPoland.VtlProcessing.Core.Tests.Operators
{
    using Moq;
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Logical;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators;
    using Xunit;

    public class ConstantOperatorTests
    {
        private readonly ExpressionResolver exprResolver;
        private readonly DataStructureResolver dsResolver;
        private readonly OperatorResolver opResolver;

        public ConstantOperatorTests()
        {
            Mock<ExpressionResolver> exprResolverMock = new Mock<ExpressionResolver>();
            exprResolverMock.Setup(o => o(null)).Returns(() => { return new Expression(); });
            exprResolverMock.Setup(o => o(It.IsNotNull<IExpression>())).Returns((IExpression parentExpr) => { return new Expression(parentExpr); });

            this.exprResolver = exprResolverMock.Object;

            Mock<DataStructureResolver> dsResolverMock = new Mock<DataStructureResolver>();
            dsResolverMock.Setup(o => o(null, null, null)).Returns(() => { return new DataStructure(); });
            dsResolverMock.Setup(o => o(It.IsNotNull<string>(), It.IsNotNull<ComponentType?>(), It.IsNotNull<BasicDataType?>()))
                .Returns((string compName, ComponentType? compType, BasicDataType? dataType) => { return new DataStructure(compName, (ComponentType)compType, (BasicDataType)dataType); });

            this.dsResolver = dsResolverMock.Object;

            Mock<OperatorResolver> opResolverMock = new Mock<OperatorResolver>();
            opResolverMock.Setup(o => o("const")).Returns(() => { return new ConstantOperator(this.dsResolver); });

            this.opResolver = opResolverMock.Object;
        }

        [Theory]
        [InlineData(BasicDataType.Integer, "1")]
        [InlineData(BasicDataType.Number, "1.0")]
        [InlineData(BasicDataType.String, "\"Test\"")]
        [InlineData(BasicDataType.Boolean, "true")]
        [InlineData(BasicDataType.Time, "t\"2020-03-23/2020-04-12\"")]
        [InlineData(BasicDataType.Date, "t\"2020-06-20\"")]
        [InlineData(BasicDataType.TimePeriod, "t\"2010Q2\"")]
        [InlineData(BasicDataType.Duration, "t\"M\"")]
        [InlineData(BasicDataType.None, "null")]
        public void GetOutputStructure_CorrectExpr_CorrectScalarStructure(BasicDataType expectedType, string exprText)
        {
            IExpression constExpr = this.exprResolver();
            constExpr.ExpressionText = exprText;

            constExpr.OperatorDefinition = this.opResolver("const");

            IDataStructure expected = this.dsResolver("const", ComponentType.Measure, expectedType);

            IDataStructure dataStructure = constExpr.OperatorDefinition.GetOutputStructure(constExpr);

            Assert.True(expected.EqualsObj(dataStructure));
        }

        [Theory]
        [InlineData("1 + 2")]
        [InlineData("1,0")]
        [InlineData("Test")]
        [InlineData("t\"Test\"")]
        [InlineData("NULL")]
        public void GetOutputStructure_WrongExpr_ThrowsException(string exprText)
        {
            IExpression constExpr = this.exprResolver();
            constExpr.ExpressionText = exprText;

            constExpr.OperatorDefinition = this.opResolver("const");

            Assert.ThrowsAny<VtlOperatorError>(() => { constExpr.OperatorDefinition.GetOutputStructure(constExpr); });
        }
    }
}
