namespace StatisticsPoland.VtlProcessing.Core.Tests.InfrastructureTests.JoinBuilder.JoinBranches
{
    using Moq;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;

    public partial class ApplyBranchTests
    {
        private readonly IExpressionTextGenerator exprTextGenerator;

        public ApplyBranchTests()
        {
            Mock<IExpressionTextGenerator> exprTextGeneratorMock = new Mock<IExpressionTextGenerator>();
            exprTextGeneratorMock.Setup(etg => etg.Generate(It.IsAny<IExpression>()))
                .Callback((IExpression expr) => 
                {
                    expr.ExpressionText = "Generated text";
                });

            this.exprTextGenerator = exprTextGeneratorMock.Object;
        }
    }
}
