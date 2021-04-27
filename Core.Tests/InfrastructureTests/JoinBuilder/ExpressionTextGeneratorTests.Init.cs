namespace StatisticsPoland.VtlProcessing.Core.Tests.InfrastructureTests.JoinBuilder
{
    using Moq;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System.Collections.Generic;

    public partial class ExpressionTextGeneratorTests
    {
        private readonly ExpressionTextGenerator _exprTextGenerator;

        public ExpressionTextGeneratorTests()
        {
            this._exprTextGenerator = new ExpressionTextGenerator();
        }

        private IExpression GetExpr(string opSymbol)
        {
            Mock<IExpression> exprMock = new Mock<IExpression>();
            exprMock.SetupAllProperties();
            exprMock.SetupGet(o => o.Operands).Returns(new Dictionary<string, IExpression>());
            exprMock.SetupGet(o => o.OperatorSymbol).Returns(opSymbol);
            exprMock.SetupGet(o => o.OperandsCollection).Returns(exprMock.Object.Operands.Values);
            
            exprMock.Setup(o => o.AddOperand(It.IsAny<string>(), It.IsAny<IExpression>()))
                .Callback((string signature, IExpression expression) =>
                { 
                    exprMock.Object.Operands.Add(signature, expression);
                });
            
            exprMock.Setup(o => o.GetDescendantExprs(It.IsAny<string>()))
                .Returns((string resultName) =>
                {
                    List<IExpression> descendantExprs = new List<IExpression>();
                    foreach (IExpression expr in exprMock.Object.OperandsCollection)
                    {
                        if (expr.ResultName == resultName) descendantExprs.Add(expr);
                        descendantExprs.AddRange(expr.GetDescendantExprs(resultName));
                    }

                    return descendantExprs;
                });

            IExpression expr = exprMock.Object;
            expr.AddOperand("ds_1", ModelResolvers.ExprResolver());
            expr.AddOperand("ds_2", ModelResolvers.ExprResolver());
            expr.AddOperand("ds_3", ModelResolvers.ExprResolver());
            expr.Operands["ds_1"].ExpressionText = "expr1";
            expr.Operands["ds_2"].ExpressionText = "expr2";
            expr.Operands["ds_3"].ExpressionText = "expr3";

            return expr;
        }
    }
}
