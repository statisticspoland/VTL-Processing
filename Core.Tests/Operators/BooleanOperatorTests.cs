﻿namespace StatisticsPoland.VtlProcessing.Core.Tests.Operators
{
    using Moq;
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators;
    using StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public partial class BooleanOperatorTests
    {
        private readonly List<string> operators;
        private readonly OperatorResolver opResolver;

        public BooleanOperatorTests()
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

            opResolverMock.Setup(o => o("and")).Returns(() => { return new BooleanOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver, "and"); });
            opResolverMock.Setup(o => o("or")).Returns(() => { return new BooleanOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver, "or"); });
            opResolverMock.Setup(o => o("xor")).Returns(() => { return new BooleanOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver, "xor"); });

            this.opResolver = opResolverMock.Object;

            this.operators = new List<string>() { "and", "or", "xor" };
        }

        [Theory]
        [InlineData(TestExprType.BoolsDataset, TestExprType.BoolsDataset)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.BoolsDataset)]
        public void GetOutputStructure_Correct2DatasetsExpr_BoolScalarStructre(params TestExprType[] types)
        {
            foreach (string opSymbol in operators)
            {
                IExpression boolExpr = TestExprFactory.GetExpression(types);
                boolExpr.OperatorDefinition = this.opResolver(opSymbol);

                IExpression expected = TestExprFactory.GetExpression(TestExprType.BoolsDataset);

                boolExpr.Operands["ds_1"].Structure.Measures.RemoveAt(1);
                boolExpr.Operands["ds_2"].Structure.Measures.RemoveAt(1);
                expected.Structure.Measures.RemoveAt(1);

                IDataStructure dataStructure = boolExpr.OperatorDefinition.GetOutputStructure(boolExpr);

                Assert.True(expected.Structure.EqualsObj(dataStructure));
            }
        }

        [Theory]
        [InlineData(TestExprType.BoolsDataset, TestExprType.Boolean)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.None)]
        [InlineData(TestExprType.NonesDataset, TestExprType.None)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Boolean)]
        public void GetOutputStructure_CorrectDatasetScalarExpr_BoolScalarStructre(params TestExprType[] types)
        {
            foreach (string opSymbol in this.operators)
            {
                IExpression boolExpr = TestExprFactory.GetExpression(types);
                boolExpr.OperatorDefinition = this.opResolver(opSymbol);

                IExpression expected = TestExprFactory.GetExpression(TestExprType.BoolsDataset);

                boolExpr.Operands["ds_1"].Structure.Measures.RemoveAt(1);
                expected.Structure.Measures.RemoveAt(1);

                IDataStructure dataStructure = boolExpr.OperatorDefinition.GetOutputStructure(boolExpr);

                Assert.True(expected.Structure.EqualsObj(dataStructure));
            }
        }

        [Theory]
        [InlineData(TestExprType.Boolean, TestExprType.BoolsDataset)]
        [InlineData(TestExprType.Boolean, TestExprType.NonesDataset)]
        [InlineData(TestExprType.None, TestExprType.NonesDataset)]
        [InlineData(TestExprType.None, TestExprType.BoolsDataset)]
        public void GetOutputStructure_CorrectScalarDatasetExpr_BoolScalarStructre(params TestExprType[] types)
        {
            foreach (string opSymbol in this.operators)
            {
                IExpression boolExpr = TestExprFactory.GetExpression(types);
                boolExpr.OperatorDefinition = this.opResolver(opSymbol);

                IExpression expected = TestExprFactory.GetExpression(TestExprType.BoolsDataset);

                boolExpr.Operands["ds_2"].Structure.Measures.RemoveAt(1);
                expected.Structure.Measures.RemoveAt(1);

                IDataStructure dataStructure = boolExpr.OperatorDefinition.GetOutputStructure(boolExpr);

                Assert.True(expected.Structure.EqualsObj(dataStructure));
            }
        }

        [Theory]
        [InlineData(TestExprType.Boolean, TestExprType.Boolean)]
        [InlineData(TestExprType.Boolean, TestExprType.None)]
        [InlineData(TestExprType.None, TestExprType.None)]
        [InlineData(TestExprType.None, TestExprType.Boolean)]
        public void GetOutputStructure_Correct2ScalarsExpr_BoolScalarStructre(params TestExprType[] types)
        {
            foreach (string opSymbol in this.operators)
            {
                IExpression boolExpr = TestExprFactory.GetExpression(types[0], types[1]);
                boolExpr.OperatorDefinition = this.opResolver(opSymbol);

                IDataStructure dataStructure = boolExpr.OperatorDefinition.GetOutputStructure(boolExpr);

                Assert.Equal(BasicDataType.Boolean, dataStructure.Components.First().ValueDomain.DataType);
            }
        }

        [Fact]
        public void GetOutputStructure_WrongArgsExpr_ThrowsException()
        {
            TestExprType[][] correctCombs = new TestExprType[][] {
                new TestExprType[] { TestExprType.Boolean, TestExprType.Boolean },
                new TestExprType[] { TestExprType.Boolean, TestExprType.None },
                new TestExprType[] { TestExprType.None, TestExprType.Boolean },
                new TestExprType[] { TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.Boolean, TestExprType.BoolsDataset },
                new TestExprType[] { TestExprType.Boolean, TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.None, TestExprType.BoolsDataset },
                new TestExprType[] { TestExprType.None, TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.BoolsDataset, TestExprType.Boolean },
                new TestExprType[] { TestExprType.BoolsDataset, TestExprType.None },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.Boolean },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.None },
                new TestExprType[] { TestExprType.BoolsDataset, TestExprType.BoolsDataset },
                new TestExprType[] { TestExprType.BoolsDataset, TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.BoolsDataset },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.NonesDataset }
            };

            TestExprType[][] combinations = Enum.GetValues(typeof(TestExprType)).Cast<TestExprType>().GetCombinations(2);
            TestExprType[][] wrongCombs = combinations.Without(correctCombs);

            foreach (string opSymbol in this.operators)
            {
                foreach (TestExprType[] wrongComb in wrongCombs.Where(wrongComb => (int)wrongComb[0] < 18 && (int)wrongComb[1] < 18)) // No mixed datasets
                {
                    IExpression boolExpr = TestExprFactory.GetExpression(wrongComb);
                    boolExpr.OperatorDefinition = this.opResolver(opSymbol);

                    if (!boolExpr.Operands["ds_1"].IsScalar) boolExpr.Operands["ds_1"].Structure.Measures.RemoveAt(1);
                    if (!boolExpr.Operands["ds_2"].IsScalar) boolExpr.Operands["ds_2"].Structure.Measures.RemoveAt(1);

                    // Debug condition example: wrongComb[0] == TestExprType.Integer && wrongComb[1] == TestExprType.Integer
                    Assert.ThrowsAny<VtlOperatorError>(() => { boolExpr.OperatorDefinition.GetOutputStructure(boolExpr); });
                }
            }
        }
    }
}
