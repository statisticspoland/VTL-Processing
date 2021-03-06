﻿namespace StatisticsPoland.VtlProcessing.Core.Tests.InfrastructureTests.JoinBuilder.JoinBranches
{
    using Moq;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;

    public partial class CalcBranchTests
    {
        private readonly IExpressionTextGenerator _exprTextGenerator;
        private readonly IExpressionFactory _exprFac;

        public CalcBranchTests()
        {
            Mock<IExpressionTextGenerator> exprTextGeneratorMock = new Mock<IExpressionTextGenerator>();
            exprTextGeneratorMock.Setup(etg => etg.GenerateRecursively(It.IsAny<IExpression>()))
                .Callback((IExpression expr) =>
                {
                    expr.ExpressionText = "Generated text";
                });

            this._exprTextGenerator = exprTextGeneratorMock.Object;

            Mock<IExpressionFactory> exprFactoryMock = new Mock<IExpressionFactory>();
            exprFactoryMock.SetupGet(e => e.OperatorResolver).Returns(ModelResolvers.OperatorResolver);
            exprFactoryMock.SetupGet(e => e.ExprResolver).Returns(ModelResolvers.ExprResolver);
            exprFactoryMock.Setup(e => e.GetExpression(It.IsAny<string>(), ExpressionFactoryNameTarget.OperatorSymbol)).Returns((string opSymbol, ExpressionFactoryNameTarget nameTarget) =>
            {
                IExpression expr = ModelResolvers.ExprResolver();
                
                Mock<IOperatorDefinition> opDefMock = new Mock<IOperatorDefinition>();
                opDefMock.SetupGet(opDef => opDef.Symbol).Returns(opSymbol);

                expr.OperatorDefinition = opDefMock.Object;
                return expr;
            });

            this._exprFac = exprFactoryMock.Object;
        }
    }
}
