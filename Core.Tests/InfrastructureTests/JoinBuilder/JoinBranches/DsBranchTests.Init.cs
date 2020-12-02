namespace StatisticsPoland.VtlProcessing.Core.Tests.InfrastructureTests.JoinBuilder.JoinBranches
{
    using Moq;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;

    public partial class DsBranchTests
    {
        private readonly IExpressionTextGenerator exprTextGenerator;
        private readonly IExpressionFactory exprFac;

        public DsBranchTests()
        {
            Mock<IExpressionTextGenerator> exprTextGeneratorMock = new Mock<IExpressionTextGenerator>();
            exprTextGeneratorMock.Setup(etg => etg.GenerateRecursively(It.IsAny<IExpression>()))
                .Callback((IExpression expr) =>
                {
                    expr.ExpressionText = "Generated text";
                });

            this.exprTextGenerator = exprTextGeneratorMock.Object;

            Mock<IExpressionFactory> exprFactoryMock = new Mock<IExpressionFactory>();
            exprFactoryMock.SetupGet(e => e.OperatorResolver).Returns(ModelResolvers.OperatorResolver);
            exprFactoryMock.SetupGet(e => e.ExprResolver).Returns(ModelResolvers.ExprResolver);
            exprFactoryMock.Setup(e => e.GetExpression(It.IsAny<string>(), ExpressionFactoryNameTarget.OperatorSymbol)).Returns((string opSymbol, ExpressionFactoryNameTarget nameTarget) =>
            {
                IExpression expr = ModelResolvers.ExprResolver();

                Mock<IOperatorDefinition> opDefMock = new Mock<IOperatorDefinition>();
                opDefMock.SetupGet(opDef => opDef.Symbol).Returns(opSymbol);
                opDefMock.Setup(opDef => opDef.GetOutputStructure(It.IsAny<IExpression>())).Returns(ModelResolvers.DsResolver());

                expr.OperatorDefinition = opDefMock.Object;
                return expr;
            });

            this.exprFac = exprFactoryMock.Object;
        }
    }
}
