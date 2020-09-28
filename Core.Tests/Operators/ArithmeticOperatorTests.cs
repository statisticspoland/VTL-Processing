namespace StatisticsPoland.VtlProcessing.Core.Tests.Operators
{
    using Moq;
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models;
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

    public class ArithmeticOperatorTests
    {
        private readonly List<string> operators;
        private readonly OperatorResolver opResolver;

        public ArithmeticOperatorTests()
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

            opResolverMock.Setup(o => o("+")).Returns(() => { return new ArithmeticOperator(joinApplyMeasuresOp, "+"); });
            opResolverMock.Setup(o => o("-")).Returns(() => { return new ArithmeticOperator(joinApplyMeasuresOp, "-"); });
            opResolverMock.Setup(o => o("*")).Returns(() => { return new ArithmeticOperator(joinApplyMeasuresOp, "*"); });
            opResolverMock.Setup(o => o("/")).Returns(() => { return new ArithmeticOperator(joinApplyMeasuresOp, "/"); });

            this.opResolver = opResolverMock.Object;

            this.operators = new List<string>() { "+", "-", "*", "/" };
        }

        [Theory]
        [InlineData(TestExprType.IntsDataset, TestExprType.IntsDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.NumbersDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.MixedNoneIntDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.MixedNoneNumDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.IntsDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.MixedNoneIntDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.MixedNoneNumDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.IntsDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.MixedNoneIntDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.MixedNoneNumDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.IntsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneIntDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneNumDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.IntsDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.MixedNoneIntDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.MixedNoneNumDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.IntsDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.MixedNoneIntDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.MixedNoneNumDataset)]
        public void GetOutputStructure_Correct1Superset1SubsetExpr_SupersetModified(params TestExprType[] types)
        {
            foreach (string opSymbol in this.operators)
            {
                IExpression arithmeticExpr = TestExprFactory.GetExpression(types);
                arithmeticExpr.OperatorDefinition = this.opResolver(opSymbol);

                arithmeticExpr.Operands["ds_1"].Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id2"));

                IDataStructure expectedStructure = arithmeticExpr.Operands[$"ds_1"].Structure.GetCopy();
                for (int i = 0; i < expectedStructure.Measures.Count; i++)
                {
                    BasicDataType dataType = arithmeticExpr.Operands[$"ds_2"].Structure.Measures[i].ValueDomain.DataType;
                    if (dataType == BasicDataType.Number) expectedStructure.Measures[i].ValueDomain = new ValueDomain(dataType);
                    if (expectedStructure.Measures[i].ValueDomain.DataType == BasicDataType.None)
                        expectedStructure.Measures[i].ValueDomain = new ValueDomain(BasicDataType.Integer);
                }

                IDataStructure dataStructure = arithmeticExpr.OperatorDefinition.GetOutputStructure(arithmeticExpr);

                Assert.True(expectedStructure.EqualsObj(dataStructure));
            }
        }

        [Theory]
        [InlineData(TestExprType.IntsDataset, TestExprType.IntsDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.NumbersDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.MixedNoneIntDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.MixedNoneNumDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.IntsDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.MixedNoneIntDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.MixedNoneNumDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.IntsDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.MixedNoneIntDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.MixedNoneNumDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.IntsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneIntDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneNumDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.IntsDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.MixedNoneIntDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.MixedNoneNumDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.IntsDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.MixedNoneIntDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.MixedNoneNumDataset)]
        public void GetOutputStructure_Correct1Subset1SupersetExpr_SupersetModifiedStructure( params TestExprType[] types)
        {
            foreach (string opSymbol in this.operators)
            {
                IExpression arithmeticExpr = TestExprFactory.GetExpression(types);
                arithmeticExpr.OperatorDefinition = this.opResolver(opSymbol);

                arithmeticExpr.Operands["ds_2"].Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id2"));

                IDataStructure expectedStructure = arithmeticExpr.Operands[$"ds_2"].Structure.GetCopy();
                for (int i = 0; i < expectedStructure.Measures.Count; i++)
                {
                    BasicDataType dataType = arithmeticExpr.Operands[$"ds_1"].Structure.Measures[i].ValueDomain.DataType;
                    if (dataType == BasicDataType.Number) expectedStructure.Measures[i].ValueDomain = new ValueDomain(dataType);
                    if (expectedStructure.Measures[i].ValueDomain.DataType == BasicDataType.None)
                        expectedStructure.Measures[i].ValueDomain = new ValueDomain(BasicDataType.Integer);
                }

                IDataStructure dataStructure = arithmeticExpr.OperatorDefinition.GetOutputStructure(arithmeticExpr);

                Assert.True(expectedStructure.EqualsObj(dataStructure));
            }
        }

        [Theory]
        [InlineData(TestExprType.IntsDataset, TestExprType.IntsDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.NumbersDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.MixedNoneIntDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.MixedNoneNumDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.IntsDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.MixedNoneIntDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.MixedNoneNumDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.IntsDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.MixedNoneIntDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.MixedNoneNumDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.IntsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneIntDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneNumDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.IntsDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.MixedNoneIntDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.MixedNoneNumDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.IntsDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.MixedNoneIntDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.MixedNoneNumDataset)]
        public void GetOutputStructure_2NotMatchDatasetsExpr_ThrowsException(params TestExprType[] types)
        {
            foreach (string opSymbol in this.operators)
            {
                IExpression arithmeticExpr = TestExprFactory.GetExpression(types);
                arithmeticExpr.OperatorDefinition = this.opResolver(opSymbol);

                arithmeticExpr.Operands["ds_1"].Structure.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me3"));

                Assert.ThrowsAny<VtlOperatorError>(() => { arithmeticExpr.OperatorDefinition.GetOutputStructure(arithmeticExpr); });
            }
        }

        [Theory]
        [InlineData(TestExprType.IntsDataset, TestExprType.IntsDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.MixedNoneIntDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.MixedNoneNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.IntsDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.MixedNoneIntDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.MixedNoneNumDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.IntsDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.MixedNoneIntDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.MixedNoneNumDataset, TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.IntsDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneIntDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.IntsDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.MixedNoneIntDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.MixedNoneNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.IntsDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.MixedNoneIntDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.MixedNoneNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        public void GetOutputStructure_Correct2DatasetsExpr_DataStructure(params TestExprType[] types)
        {
            foreach (string opSymbol in this.operators)
            {
                IExpression arithmeticExpr = TestExprFactory.GetExpression(types);
                arithmeticExpr.OperatorDefinition = this.opResolver(opSymbol);

                IExpression expected = TestExprFactory.GetExpression(types[2]);

                IDataStructure dataStructure = arithmeticExpr.OperatorDefinition.GetOutputStructure(arithmeticExpr);

                Assert.True(expected.Structure.EqualsObj(dataStructure));
            }
        }

        [Theory]
        [InlineData(TestExprType.IntsDataset, TestExprType.Integer, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.Number, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.None, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.Integer, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.Number, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.None, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.Integer, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.Number, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.None, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Integer, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Number, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.None, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.Integer, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.Number, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.None, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.Integer, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.Number, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.None, /*result:*/ TestExprType.MixedIntNumDataset)]
        public void GetOutputStructure_Correct1Dataset1ScalarExpr_DataStructure(params TestExprType[] types)
        {
            foreach (string opSymbol in this.operators)
            {
                IExpression arithmeticExpr = TestExprFactory.GetExpression(types);
                arithmeticExpr.OperatorDefinition = this.opResolver(opSymbol);

                IExpression expected = TestExprFactory.GetExpression(types[2]);

                IDataStructure dataStructure = arithmeticExpr.OperatorDefinition.GetOutputStructure(arithmeticExpr);

                Assert.True(expected.Structure.EqualsObj(dataStructure));
            }
        }

        [Theory]
        [InlineData(TestExprType.Integer, TestExprType.Integer, /*result:*/ TestExprType.Integer)]
        [InlineData(TestExprType.Integer, TestExprType.Number, /*result:*/ TestExprType.Number)]
        [InlineData(TestExprType.Integer, TestExprType.None, /*result:*/ TestExprType.Integer)]
        [InlineData(TestExprType.Number, TestExprType.Number, /*result:*/ TestExprType.Number)]
        [InlineData(TestExprType.Number, TestExprType.Integer, /*result:*/ TestExprType.Number)]
        [InlineData(TestExprType.Number, TestExprType.None, /*result:*/ TestExprType.Number)]
        public void GetOutputStructure_Correct2ScalarsExpr_DataStructure(params TestExprType[] types)
        {
            foreach (string opSymbol in this.operators)
            {
                IExpression arithmeticExpr = TestExprFactory.GetExpression(types);
                arithmeticExpr.OperatorDefinition = this.opResolver(opSymbol);

                IExpression expected = TestExprFactory.GetExpression(types[2]);

                IDataStructure dataStructure = arithmeticExpr.OperatorDefinition.GetOutputStructure(arithmeticExpr);

                Assert.True(expected.Structure.EqualsObj(dataStructure));
            }
        }

        [Theory]
        [InlineData(TestExprType.Integer, TestExprType.IntsDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.Number, TestExprType.IntsDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Integer, TestExprType.NumbersDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.Number, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Integer, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.Number, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.NumbersDataset)]
        public void GetOutputStructure_Correct1Scalar1DatasetExpr_DataStructure(params TestExprType[] types)
        {
            foreach (string opSymbol in this.operators)
            {
                IExpression arithmeticExpr = TestExprFactory.GetExpression(types);
                arithmeticExpr.OperatorDefinition = this.opResolver(opSymbol);

                IDataStructure expectedStructure = arithmeticExpr.Operands["ds_2"].Structure.GetCopy();
                if (types[0] == TestExprType.Number)
                {
                    foreach (StructureComponent measure in expectedStructure.Measures)
                        measure.ValueDomain = new ValueDomain(BasicDataType.Number);
                }

                IDataStructure dataStructure = arithmeticExpr.OperatorDefinition.GetOutputStructure(arithmeticExpr);

                Assert.True(expectedStructure.EqualsObj(dataStructure));
            }
        }

        [Fact]
        public void GetOutputStructure_WrongArgsExpr_ThrowsException()
        {
            TestExprType[][] correctCombs = new TestExprType[][] {
                new TestExprType[] { TestExprType.Integer, TestExprType.Integer },
                new TestExprType[] { TestExprType.Integer, TestExprType.Number },
                new TestExprType[] { TestExprType.Integer, TestExprType.None },
                new TestExprType[] { TestExprType.Integer, TestExprType.IntsDataset },
                new TestExprType[] { TestExprType.Integer, TestExprType.NumbersDataset },
                new TestExprType[] { TestExprType.Integer, TestExprType.MixedIntNumDataset },
                new TestExprType[] { TestExprType.Integer, TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.Integer, TestExprType.MixedNoneIntDataset },
                new TestExprType[] { TestExprType.Integer, TestExprType.MixedNoneNumDataset },
                new TestExprType[] { TestExprType.Number, TestExprType.Integer },
                new TestExprType[] { TestExprType.Number, TestExprType.Number },
                new TestExprType[] { TestExprType.Number, TestExprType.None },
                new TestExprType[] { TestExprType.Number, TestExprType.IntsDataset },
                new TestExprType[] { TestExprType.Number, TestExprType.NumbersDataset },
                new TestExprType[] { TestExprType.Number, TestExprType.MixedIntNumDataset },
                new TestExprType[] { TestExprType.Number, TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.Number, TestExprType.MixedNoneIntDataset },
                new TestExprType[] { TestExprType.Number, TestExprType.MixedNoneNumDataset },
                new TestExprType[] { TestExprType.None, TestExprType.Integer },
                new TestExprType[] { TestExprType.None, TestExprType.Number },
                new TestExprType[] { TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.None, TestExprType.IntsDataset },
                new TestExprType[] { TestExprType.None, TestExprType.NumbersDataset },
                new TestExprType[] { TestExprType.None, TestExprType.MixedIntNumDataset },
                new TestExprType[] { TestExprType.None, TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.None, TestExprType.MixedNoneIntDataset },
                new TestExprType[] { TestExprType.None, TestExprType.MixedNoneNumDataset },
                new TestExprType[] { TestExprType.IntsDataset, TestExprType.Integer },
                new TestExprType[] { TestExprType.IntsDataset, TestExprType.Number },
                new TestExprType[] { TestExprType.IntsDataset, TestExprType.None },
                new TestExprType[] { TestExprType.IntsDataset, TestExprType.IntsDataset },
                new TestExprType[] { TestExprType.IntsDataset, TestExprType.NumbersDataset },
                new TestExprType[] { TestExprType.IntsDataset, TestExprType.MixedIntNumDataset },
                new TestExprType[] { TestExprType.IntsDataset, TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.IntsDataset, TestExprType.MixedNoneIntDataset },
                new TestExprType[] { TestExprType.IntsDataset, TestExprType.MixedNoneNumDataset },
                new TestExprType[] { TestExprType.NumbersDataset, TestExprType.Integer },
                new TestExprType[] { TestExprType.NumbersDataset, TestExprType.Number },
                new TestExprType[] { TestExprType.NumbersDataset, TestExprType.None },
                new TestExprType[] { TestExprType.NumbersDataset, TestExprType.IntsDataset },
                new TestExprType[] { TestExprType.NumbersDataset, TestExprType.NumbersDataset },
                new TestExprType[] { TestExprType.NumbersDataset, TestExprType.MixedIntNumDataset },
                new TestExprType[] { TestExprType.NumbersDataset, TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.NumbersDataset, TestExprType.MixedNoneIntDataset },
                new TestExprType[] { TestExprType.NumbersDataset, TestExprType.MixedNoneNumDataset },
                new TestExprType[] { TestExprType.MixedIntNumDataset, TestExprType.Integer },
                new TestExprType[] { TestExprType.MixedIntNumDataset, TestExprType.Number },
                new TestExprType[] { TestExprType.MixedIntNumDataset, TestExprType.None },
                new TestExprType[] { TestExprType.MixedIntNumDataset, TestExprType.IntsDataset },
                new TestExprType[] { TestExprType.MixedIntNumDataset, TestExprType.NumbersDataset },
                new TestExprType[] { TestExprType.MixedIntNumDataset, TestExprType.MixedIntNumDataset },
                new TestExprType[] { TestExprType.MixedIntNumDataset, TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.MixedIntNumDataset, TestExprType.MixedNoneIntDataset },
                new TestExprType[] { TestExprType.MixedIntNumDataset, TestExprType.MixedNoneNumDataset },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.Integer },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.Number },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.None },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.IntsDataset },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.NumbersDataset },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.MixedIntNumDataset },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.MixedNoneIntDataset },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.MixedNoneNumDataset },
                new TestExprType[] { TestExprType.MixedNoneIntDataset, TestExprType.Integer },
                new TestExprType[] { TestExprType.MixedNoneIntDataset, TestExprType.Number },
                new TestExprType[] { TestExprType.MixedNoneIntDataset, TestExprType.None },
                new TestExprType[] { TestExprType.MixedNoneIntDataset, TestExprType.IntsDataset },
                new TestExprType[] { TestExprType.MixedNoneIntDataset, TestExprType.NumbersDataset },
                new TestExprType[] { TestExprType.MixedNoneIntDataset, TestExprType.MixedIntNumDataset },
                new TestExprType[] { TestExprType.MixedNoneIntDataset, TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.MixedNoneIntDataset, TestExprType.MixedNoneIntDataset },
                new TestExprType[] { TestExprType.MixedNoneIntDataset, TestExprType.MixedNoneNumDataset },
                new TestExprType[] { TestExprType.MixedNoneNumDataset, TestExprType.Integer },
                new TestExprType[] { TestExprType.MixedNoneNumDataset, TestExprType.Number },
                new TestExprType[] { TestExprType.MixedNoneNumDataset, TestExprType.None },
                new TestExprType[] { TestExprType.MixedNoneNumDataset, TestExprType.IntsDataset },
                new TestExprType[] { TestExprType.MixedNoneNumDataset, TestExprType.NumbersDataset },
                new TestExprType[] { TestExprType.MixedNoneNumDataset, TestExprType.MixedIntNumDataset },
                new TestExprType[] { TestExprType.MixedNoneNumDataset, TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.MixedNoneNumDataset, TestExprType.MixedNoneIntDataset },
                new TestExprType[] { TestExprType.MixedNoneNumDataset, TestExprType.MixedNoneNumDataset }
            };

            TestExprType[][] combinations = Enum.GetValues(typeof(TestExprType)).Cast<TestExprType>().GetCombinations(2);
            TestExprType[][] wrongCombs = combinations.Without(correctCombs);

            foreach (string opSymbol in this.operators)
            {
                foreach (TestExprType[] wrongComb in wrongCombs)
                {
                    IExpression arithmeticExpr = TestExprFactory.GetExpression(wrongComb);
                    arithmeticExpr.OperatorDefinition = this.opResolver(opSymbol);

                    // Debug condition example: wrongComb[0] == TestExprType.Integer && wrongComb[1] == TestExprType.Integer
                    Assert.ThrowsAny<VtlOperatorError>(() => { arithmeticExpr.OperatorDefinition.GetOutputStructure(arithmeticExpr); });
                }
            }
        }
    }
}
