namespace StatisticsPoland.VtlProcessing.Core.Tests.InfrastructureTests.JoinBuilder.JoinBranches
{
    using Moq;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.JoinBranches;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System.Linq;
    using Xunit;

    public partial class ApplyBranchTests
    {
        [Theory]
        [InlineData("+")]
        [InlineData("||")]
        [InlineData("sqrt")]
        [InlineData("or")]
        public void Build_NotAggrAnalyticOperatorExpr_Expr(string opSymbol)
        {
            ApplyBranch applyBranch = new ApplyBranch(ModelResolvers.ExprResolver, this.exprTextGenerator);

            Mock<IOperatorDefinition> opDefMock = new Mock<IOperatorDefinition>();
            opDefMock.SetupGet(opDef => opDef.Symbol).Returns(opSymbol);
            opDefMock.Setup(opDef => opDef.GetOutputStructure(It.IsAny<IExpression>()))
                .Returns((IExpression expr) => { return ModelResolvers.DsResolver(); });

            IExpression expr = TestExprFactory.GetExpression(opSymbol, ExpressionFactoryNameTarget.OperatorSymbol);
            expr.OperatorDefinition = opDefMock.Object;
            expr.AddOperand("ds", ModelResolvers.ExprResolver());
            expr.AddOperand("group", ModelResolvers.ExprResolver());
            expr.AddOperand("having", ModelResolvers.ExprResolver());
            expr.AddOperand("over", ModelResolvers.ExprResolver());

            IExpression result = applyBranch.Build(expr);

            Assert.NotNull(result.Structure);
            Assert.Equal("Generated text", result.ExpressionText);
            Assert.Equal("Apply", result.ResultName);
            Assert.Equal("apply", result.ParamSignature);
            Assert.Equal(expr.OperatorSymbol, result.OperatorSymbol);
            Assert.Equal(expr.OperandsCollection.Count, result.OperandsCollection.Count);

            for (int i = 0; i < expr.OperandsCollection.Count; i++)
            {
                Assert.Equal(expr.OperandsCollection.ToArray()[i].ParamSignature, result.OperandsCollection.ToArray()[i].ParamSignature);
            }
        }
    }
}
