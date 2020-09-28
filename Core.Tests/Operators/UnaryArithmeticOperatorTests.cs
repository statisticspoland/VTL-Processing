namespace StatisticsPoland.VtlProcessing.Core.Tests.Operators
{
    using Moq;
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators;
    using StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary.ComponentManagement;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class UnaryArithmeticOperatorTests
    {
        private readonly List<string> operators;
        private readonly OperatorResolver opResolver;

        public UnaryArithmeticOperatorTests()
        {
            Mock<OperatorResolver> opResolverMock = new Mock<OperatorResolver>();
            Mock<IExpressionFactory> exprFacMock = new Mock<IExpressionFactory>();

            exprFacMock.Setup(o => o.GetExpression(It.IsAny<string>(), It.IsAny<ExpressionFactoryNameTarget>()))
                .Returns((string name, ExpressionFactoryNameTarget field) =>
                {
                    IExpression expr = ModelResolvers.ExprResolver();
                    if (field == ExpressionFactoryNameTarget.ResultName) expr.ResultName = name;
                    else expr.OperatorDefinition = opResolverMock.Object(name);
                    return expr;
                });
            exprFacMock.Setup(o => o.OperatorResolver).Returns(opResolverMock.Object);

            IJoinApplyMeasuresOperator joinApplyMeasuresOp = new JoinApplyMeasuresOperator(
                exprFacMock.Object,
                ModelResolvers.DsResolver);

            opResolverMock.Setup(o => o("minus")).Returns(() => { return new UnaryArithmeticOperator(joinApplyMeasuresOp, "minus"); });
            opResolverMock.Setup(o => o("plus")).Returns(() => { return new UnaryArithmeticOperator(joinApplyMeasuresOp, "plus"); });

            this.opResolver = opResolverMock.Object;

            this.operators = new List<string>() { "minus", "plus" };
        }

        [Theory]
        [InlineData(TestExprType.Integer)]
        [InlineData(TestExprType.Number)]
        [InlineData(TestExprType.None)]
        [InlineData(TestExprType.IntsDataset)]
        [InlineData(TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.NonesDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset)]
        public void GetOutputStructure_CorrectExpr_DataStructure(TestExprType type)
        {
            foreach (string opSymbol in this.operators)
            {
                IExpression unaryArithmeticExpr = ModelResolvers.ExprResolver();
                unaryArithmeticExpr.OperatorDefinition = this.opResolver(opSymbol);
                unaryArithmeticExpr.AddOperand("ds_1", TestExprFactory.GetExpression(type));

                IDataStructure dataStructure = unaryArithmeticExpr.OperatorDefinition.GetOutputStructure(unaryArithmeticExpr);

                Assert.True(unaryArithmeticExpr.Operands["ds_1"].Structure.EqualsObj(dataStructure));
            }
        }

        [Fact]
        public void GetOutputStructure_WrongArgExpr_ThrowsException()
        {
            TestExprType[][] correctCombs = new TestExprType[][] {
                new TestExprType[] { TestExprType.Integer },
                new TestExprType[] { TestExprType.Number },
                new TestExprType[] { TestExprType.None },
                new TestExprType[] { TestExprType.IntsDataset },
                new TestExprType[] { TestExprType.NumbersDataset },
                new TestExprType[] { TestExprType.MixedIntNumDataset },
                new TestExprType[] { TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.MixedNoneIntDataset },
                new TestExprType[] { TestExprType.MixedNoneNumDataset }
            };

            TestExprType[][] combinations = Enum.GetValues(typeof(TestExprType)).Cast<TestExprType>().GetCombinations(1);
            TestExprType[][] wrongCombs = combinations.Without(correctCombs);

            foreach (string opSymbol in this.operators)
            {
                foreach (TestExprType[] wrongComb in wrongCombs)
                {
                    IExpression unaryArithmeticExpr = TestExprFactory.GetExpression(wrongComb);
                    unaryArithmeticExpr.OperatorDefinition = this.opResolver(opSymbol);

                    // Debug condition example: wrongComb[0] == TestExprType.Integer
                    Assert.ThrowsAny<VtlOperatorError>(() => { unaryArithmeticExpr.OperatorDefinition.GetOutputStructure(unaryArithmeticExpr); });
                }
            }
        }
    }
}
