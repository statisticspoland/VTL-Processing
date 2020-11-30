namespace StatisticsPoland.VtlProcessing.Core.Tests.InfrastructureTests.JoinBuilder.JoinBranches
{
    using Moq;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;

    public partial class UsingBranchTests
    {
        private readonly IExpressionFactory exprFac;

        public UsingBranchTests()
        {
            Mock<IExpressionFactory> exprFactoryMock = new Mock<IExpressionFactory>();
            exprFactoryMock.SetupGet(e => e.ExprResolver).Returns(ModelResolvers.ExprResolver);
            exprFactoryMock.Setup(e => e.GetExpression(It.IsAny<string>(), ExpressionFactoryNameTarget.ResultName)).Returns((string name, ExpressionFactoryNameTarget nameTarget) =>
            {
                return TestExprFactory.GetExpression(name, nameTarget);
            });

            this.exprFac = exprFactoryMock.Object;
        }
    }
}
