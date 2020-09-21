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
    using StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public partial class NumericOperatorTests
    {
        private readonly OperatorResolver opResolver;
        public NumericOperatorTests()
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

            opResolverMock.Setup(o => o("mod")).Returns(() => { return new NumericOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver, "mod"); });
            opResolverMock.Setup(o => o("round")).Returns(() => { return new NumericOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver, "round"); });
            opResolverMock.Setup(o => o("trunc")).Returns(() => { return new NumericOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver, "trunc"); });
            opResolverMock.Setup(o => o("ceil")).Returns(() => { return new NumericOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver, "ceil"); });
            opResolverMock.Setup(o => o("floor")).Returns(() => { return new NumericOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver, "floor"); });
            opResolverMock.Setup(o => o("abs")).Returns(() => { return new NumericOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver, "abs"); });
            opResolverMock.Setup(o => o("exp")).Returns(() => { return new NumericOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver, "exp"); });
            opResolverMock.Setup(o => o("ln")).Returns(() => { return new NumericOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver, "ln"); });
            opResolverMock.Setup(o => o("power")).Returns(() => { return new NumericOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver, "power"); });
            opResolverMock.Setup(o => o("log")).Returns(() => { return new NumericOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver, "log"); });
            opResolverMock.Setup(o => o("sqrt")).Returns(() => { return new NumericOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver, "sqrt"); });

            this.opResolver = opResolverMock.Object;
        }

        [Theory]
        [InlineData("ceil", TestExprType.Integer)]
        [InlineData("ceil", TestExprType.Number)]
        [InlineData("floor", TestExprType.Integer)]
        [InlineData("floor", TestExprType.Number)]
        public void GetOutputStructure_CorrectScalarsExpr_IntScalarStructure(string opSymbol, params TestExprType[] types)
        {
            IExpression numericExpr = TestExprFactory.GetExpression(types);
            numericExpr.OperatorDefinition = this.opResolver(opSymbol);

            IDataStructure dataStructure = numericExpr.OperatorDefinition.GetOutputStructure(numericExpr);

            Assert.True(TestExprFactory.GetExpression(TestExprType.Integer).Structure.EqualsObj(dataStructure));
        }

        [Theory]
        // OneArg
        [InlineData("abs", TestExprType.Integer)]
        [InlineData("abs", TestExprType.Number)]
        [InlineData("abs", TestExprType.None)]
        [InlineData("exp", TestExprType.Integer)]
        [InlineData("exp", TestExprType.Number)]
        [InlineData("exp", TestExprType.None)]
        [InlineData("ln", TestExprType.Integer)]
        [InlineData("ln", TestExprType.Number)]
        [InlineData("ln", TestExprType.None)]
        [InlineData("sqrt", TestExprType.Integer)]
        [InlineData("sqrt", TestExprType.Number)]
        [InlineData("sqrt", TestExprType.None)]
        [InlineData("round", TestExprType.Integer)]
        [InlineData("round", TestExprType.Number)]
        [InlineData("round", TestExprType.None)]
        [InlineData("trunc", TestExprType.Integer)]
        [InlineData("trunc", TestExprType.Number)]
        [InlineData("trunc", TestExprType.None)]
        // TwoArgs
        [InlineData("mod", TestExprType.Integer, TestExprType.Integer)]
        [InlineData("mod", TestExprType.Integer, TestExprType.Number)]
        [InlineData("mod", TestExprType.Integer, TestExprType.None)]
        [InlineData("mod", TestExprType.Number, TestExprType.Integer)]
        [InlineData("mod", TestExprType.Number, TestExprType.Number)]
        [InlineData("mod", TestExprType.Number, TestExprType.None)]
        [InlineData("mod", TestExprType.None, TestExprType.Integer)]
        [InlineData("mod", TestExprType.None, TestExprType.Number)]
        [InlineData("mod", TestExprType.None, TestExprType.None)]
        [InlineData("power", TestExprType.Integer, TestExprType.Integer)]
        [InlineData("power", TestExprType.Integer, TestExprType.Number)]
        [InlineData("power", TestExprType.Integer, TestExprType.None)]
        [InlineData("power", TestExprType.Number, TestExprType.Integer)]
        [InlineData("power", TestExprType.Number, TestExprType.Number)]
        [InlineData("power", TestExprType.Number, TestExprType.None)]
        [InlineData("power", TestExprType.None, TestExprType.Integer)]
        [InlineData("power", TestExprType.None, TestExprType.Number)]
        [InlineData("power", TestExprType.None, TestExprType.None)]
        [InlineData("round", TestExprType.Integer, TestExprType.Integer)]
        [InlineData("round", TestExprType.Integer, TestExprType.None)]
        [InlineData("round", TestExprType.Number, TestExprType.Integer)]
        [InlineData("round", TestExprType.Number, TestExprType.None)]
        [InlineData("round", TestExprType.None, TestExprType.Integer)]
        [InlineData("round", TestExprType.None, TestExprType.None)]
        [InlineData("trunc", TestExprType.Integer, TestExprType.Integer)]
        [InlineData("trunc", TestExprType.Integer, TestExprType.None)]
        [InlineData("trunc", TestExprType.Number, TestExprType.Integer)]
        [InlineData("trunc", TestExprType.Number, TestExprType.None)]
        [InlineData("trunc", TestExprType.None, TestExprType.Integer)]
        [InlineData("trunc", TestExprType.None, TestExprType.None)]
        [InlineData("log", TestExprType.Integer, TestExprType.Integer)]
        [InlineData("log", TestExprType.Integer, TestExprType.None)]
        [InlineData("log", TestExprType.Number, TestExprType.Integer)]
        [InlineData("log", TestExprType.Number, TestExprType.None)]
        [InlineData("log", TestExprType.None, TestExprType.Integer)]
        [InlineData("log", TestExprType.None, TestExprType.None)]
        public void GetOutputStructure_CorrectScalarsExpr_NumberScalarStructure(string opSymbol, params TestExprType[] types)
        {
            IExpression numericExpr = TestExprFactory.GetExpression(types);
            numericExpr.OperatorDefinition = this.opResolver(opSymbol);

            IDataStructure dataStructure = numericExpr.OperatorDefinition.GetOutputStructure(numericExpr);

            Assert.True(TestExprFactory.GetExpression(TestExprType.Number).Structure.EqualsObj(dataStructure));
        }

        [Theory]
        // OneArg
        [InlineData("round", TestExprType.IntsDataset)]
        [InlineData("round", TestExprType.NumbersDataset)]
        [InlineData("round", TestExprType.MixedIntNumDataset)]
        [InlineData("round", TestExprType.NonesDataset)]
        [InlineData("round", TestExprType.MixedNoneIntDataset)]
        [InlineData("round", TestExprType.MixedNoneNumDataset)]
        [InlineData("trunc", TestExprType.IntsDataset)]
        [InlineData("trunc", TestExprType.NumbersDataset)]
        [InlineData("trunc", TestExprType.MixedIntNumDataset)]
        [InlineData("trunc", TestExprType.NonesDataset)]
        [InlineData("trunc", TestExprType.MixedNoneIntDataset)]
        [InlineData("trunc", TestExprType.MixedNoneNumDataset)]
        [InlineData("abs", TestExprType.IntsDataset)]
        [InlineData("abs", TestExprType.NumbersDataset)]
        [InlineData("abs", TestExprType.MixedIntNumDataset)]
        [InlineData("abs", TestExprType.NonesDataset)]
        [InlineData("abs", TestExprType.MixedNoneIntDataset)]
        [InlineData("abs", TestExprType.MixedNoneNumDataset)]
        [InlineData("exp", TestExprType.IntsDataset)]
        [InlineData("exp", TestExprType.NumbersDataset)]
        [InlineData("exp", TestExprType.MixedIntNumDataset)]
        [InlineData("exp", TestExprType.NonesDataset)]
        [InlineData("exp", TestExprType.MixedNoneIntDataset)]
        [InlineData("exp", TestExprType.MixedNoneNumDataset)]
        [InlineData("ln", TestExprType.IntsDataset)]
        [InlineData("ln", TestExprType.NumbersDataset)]
        [InlineData("ln", TestExprType.MixedIntNumDataset)]
        [InlineData("ln", TestExprType.NonesDataset)]
        [InlineData("ln", TestExprType.MixedNoneIntDataset)]
        [InlineData("ln", TestExprType.MixedNoneNumDataset)]
        [InlineData("sqrt", TestExprType.IntsDataset)]
        [InlineData("sqrt", TestExprType.NumbersDataset)]
        [InlineData("sqrt", TestExprType.MixedIntNumDataset)]
        [InlineData("sqrt", TestExprType.NonesDataset)]
        [InlineData("sqrt", TestExprType.MixedNoneIntDataset)]
        [InlineData("sqrt", TestExprType.MixedNoneNumDataset)]
        [InlineData("ceil", TestExprType.IntsDataset)]
        [InlineData("ceil", TestExprType.NumbersDataset)]
        [InlineData("ceil", TestExprType.MixedIntNumDataset)]
        [InlineData("ceil", TestExprType.NonesDataset)]
        [InlineData("ceil", TestExprType.MixedNoneIntDataset)]
        [InlineData("ceil", TestExprType.MixedNoneNumDataset)]
        [InlineData("floor", TestExprType.IntsDataset)]
        [InlineData("floor", TestExprType.NumbersDataset)]
        [InlineData("floor", TestExprType.MixedIntNumDataset)]
        [InlineData("floor", TestExprType.NonesDataset)]
        [InlineData("floor", TestExprType.MixedNoneIntDataset)]
        [InlineData("floor", TestExprType.MixedNoneNumDataset)]
        // TwoArgs
        [InlineData("mod", TestExprType.IntsDataset, TestExprType.IntsDataset)]
        [InlineData("mod", TestExprType.IntsDataset, TestExprType.NumbersDataset)]
        [InlineData("mod", TestExprType.IntsDataset, TestExprType.MixedIntNumDataset)]
        [InlineData("mod", TestExprType.IntsDataset, TestExprType.NonesDataset)]
        [InlineData("mod", TestExprType.IntsDataset, TestExprType.MixedNoneIntDataset)]
        [InlineData("mod", TestExprType.IntsDataset, TestExprType.MixedNoneNumDataset)]
        [InlineData("mod", TestExprType.NumbersDataset, TestExprType.IntsDataset)]
        [InlineData("mod", TestExprType.NumbersDataset, TestExprType.NumbersDataset)]
        [InlineData("mod", TestExprType.NumbersDataset, TestExprType.MixedIntNumDataset)]
        [InlineData("mod", TestExprType.NumbersDataset, TestExprType.NonesDataset)]
        [InlineData("mod", TestExprType.NumbersDataset, TestExprType.MixedNoneIntDataset)]
        [InlineData("mod", TestExprType.NumbersDataset, TestExprType.MixedNoneNumDataset)]
        [InlineData("mod", TestExprType.MixedIntNumDataset, TestExprType.IntsDataset)]
        [InlineData("mod", TestExprType.MixedIntNumDataset, TestExprType.NumbersDataset)]
        [InlineData("mod", TestExprType.MixedIntNumDataset, TestExprType.MixedIntNumDataset)]
        [InlineData("mod", TestExprType.MixedIntNumDataset, TestExprType.NonesDataset)]
        [InlineData("mod", TestExprType.MixedIntNumDataset, TestExprType.MixedNoneIntDataset)]
        [InlineData("mod", TestExprType.MixedIntNumDataset, TestExprType.MixedNoneNumDataset)]
        public void GetOutputStructure_CorrectDatasetsExpr_CorrectDatasetStructure(string opSymbol, params TestExprType[] types)
        {
            IExpression numericExpr = TestExprFactory.GetExpression(types);
            numericExpr.OperatorDefinition = this.opResolver(opSymbol);

            IDataStructure dataStructure = numericExpr.OperatorDefinition.GetOutputStructure(numericExpr);
            
            if (opSymbol.In("ceil", "floor"))
                Assert.True(TestExprFactory.GetExpression(TestExprType.IntsDataset).Structure.EqualsObj(dataStructure));
            else
                Assert.True(TestExprFactory.GetExpression(TestExprType.NumbersDataset).Structure.EqualsObj(dataStructure));
        }

        [Theory]
        [InlineData("mod", TestExprType.IntsDataset, TestExprType.Integer)]
        [InlineData("mod", TestExprType.IntsDataset, TestExprType.Number)]
        [InlineData("mod", TestExprType.IntsDataset, TestExprType.None)]
        [InlineData("mod", TestExprType.NumbersDataset, TestExprType.Integer)]
        [InlineData("mod", TestExprType.NumbersDataset, TestExprType.Number)]
        [InlineData("mod", TestExprType.NumbersDataset, TestExprType.None)]
        [InlineData("mod", TestExprType.MixedIntNumDataset, TestExprType.Integer)]
        [InlineData("mod", TestExprType.MixedIntNumDataset, TestExprType.Number)]
        [InlineData("mod", TestExprType.MixedIntNumDataset, TestExprType.None)]
        [InlineData("mod", TestExprType.NonesDataset, TestExprType.Integer)]
        [InlineData("mod", TestExprType.NonesDataset, TestExprType.Number)]
        [InlineData("mod", TestExprType.NonesDataset, TestExprType.None)]
        [InlineData("mod", TestExprType.MixedNoneIntDataset, TestExprType.Integer)]
        [InlineData("mod", TestExprType.MixedNoneIntDataset, TestExprType.Number)]
        [InlineData("mod", TestExprType.MixedNoneIntDataset, TestExprType.None)]
        [InlineData("mod", TestExprType.MixedNoneNumDataset, TestExprType.Integer)]
        [InlineData("mod", TestExprType.MixedNoneNumDataset, TestExprType.Number)]
        [InlineData("mod", TestExprType.MixedNoneNumDataset, TestExprType.None)]
        [InlineData("power", TestExprType.IntsDataset, TestExprType.Integer)]
        [InlineData("power", TestExprType.IntsDataset, TestExprType.Number)]
        [InlineData("power", TestExprType.IntsDataset, TestExprType.None)]
        [InlineData("power", TestExprType.NumbersDataset, TestExprType.Integer)]
        [InlineData("power", TestExprType.NumbersDataset, TestExprType.Number)]
        [InlineData("power", TestExprType.NumbersDataset, TestExprType.None)]
        [InlineData("power", TestExprType.MixedIntNumDataset, TestExprType.Integer)]
        [InlineData("power", TestExprType.MixedIntNumDataset, TestExprType.Number)]
        [InlineData("power", TestExprType.MixedIntNumDataset, TestExprType.None)]
        [InlineData("power", TestExprType.NonesDataset, TestExprType.Integer)]
        [InlineData("power", TestExprType.NonesDataset, TestExprType.Number)]
        [InlineData("power", TestExprType.NonesDataset, TestExprType.None)]
        [InlineData("power", TestExprType.MixedNoneIntDataset, TestExprType.Integer)]
        [InlineData("power", TestExprType.MixedNoneIntDataset, TestExprType.Number)]
        [InlineData("power", TestExprType.MixedNoneIntDataset, TestExprType.None)]
        [InlineData("power", TestExprType.MixedNoneNumDataset, TestExprType.Integer)]
        [InlineData("power", TestExprType.MixedNoneNumDataset, TestExprType.Number)]
        [InlineData("power", TestExprType.MixedNoneNumDataset, TestExprType.None)]
        [InlineData("round", TestExprType.IntsDataset, TestExprType.Integer)]
        [InlineData("round", TestExprType.IntsDataset, TestExprType.None)]
        [InlineData("round", TestExprType.NumbersDataset, TestExprType.Integer)]
        [InlineData("round", TestExprType.NumbersDataset, TestExprType.None)]
        [InlineData("round", TestExprType.MixedIntNumDataset, TestExprType.Integer)]
        [InlineData("round", TestExprType.MixedIntNumDataset, TestExprType.None)]
        [InlineData("round", TestExprType.NonesDataset, TestExprType.Integer)]
        [InlineData("round", TestExprType.NonesDataset, TestExprType.None)]
        [InlineData("round", TestExprType.MixedNoneIntDataset, TestExprType.Integer)]
        [InlineData("round", TestExprType.MixedNoneIntDataset, TestExprType.None)]
        [InlineData("round", TestExprType.MixedNoneNumDataset, TestExprType.Integer)]
        [InlineData("round", TestExprType.MixedNoneNumDataset, TestExprType.None)]
        [InlineData("trunc", TestExprType.IntsDataset, TestExprType.Integer)]
        [InlineData("trunc", TestExprType.IntsDataset, TestExprType.None)]
        [InlineData("trunc", TestExprType.NumbersDataset, TestExprType.Integer)]
        [InlineData("trunc", TestExprType.NumbersDataset, TestExprType.None)]
        [InlineData("trunc", TestExprType.MixedIntNumDataset, TestExprType.Integer)]
        [InlineData("trunc", TestExprType.MixedIntNumDataset, TestExprType.None)]
        [InlineData("trunc", TestExprType.NonesDataset, TestExprType.Integer)]
        [InlineData("trunc", TestExprType.NonesDataset, TestExprType.None)]
        [InlineData("trunc", TestExprType.MixedNoneIntDataset, TestExprType.Integer)]
        [InlineData("trunc", TestExprType.MixedNoneIntDataset, TestExprType.None)]
        [InlineData("trunc", TestExprType.MixedNoneNumDataset, TestExprType.Integer)]
        [InlineData("trunc", TestExprType.MixedNoneNumDataset, TestExprType.None)]
        [InlineData("log", TestExprType.IntsDataset, TestExprType.Integer)]
        [InlineData("log", TestExprType.IntsDataset, TestExprType.None)]
        [InlineData("log", TestExprType.NumbersDataset, TestExprType.Integer)]
        [InlineData("log", TestExprType.NumbersDataset, TestExprType.None)]
        [InlineData("log", TestExprType.MixedIntNumDataset, TestExprType.Integer)]
        [InlineData("log", TestExprType.MixedIntNumDataset, TestExprType.None)]
        [InlineData("log", TestExprType.NonesDataset, TestExprType.Integer)]
        [InlineData("log", TestExprType.NonesDataset, TestExprType.None)]
        [InlineData("log", TestExprType.MixedNoneIntDataset, TestExprType.Integer)]
        [InlineData("log", TestExprType.MixedNoneIntDataset, TestExprType.None)]
        [InlineData("log", TestExprType.MixedNoneNumDataset, TestExprType.Integer)]
        [InlineData("log", TestExprType.MixedNoneNumDataset, TestExprType.None)]
        public void GetOutputStructure_CorrectDatasetScalarExpr_NumbersDatasetStructure(string opSymbol, params TestExprType[] types)
        {
            IExpression numericExpr = TestExprFactory.GetExpression(types);
            numericExpr.OperatorDefinition = this.opResolver(opSymbol);

            IDataStructure dataStructure = numericExpr.OperatorDefinition.GetOutputStructure(numericExpr);

            Assert.True(TestExprFactory.GetExpression(TestExprType.NumbersDataset).Structure.EqualsObj(dataStructure));
        }

        [Theory]
        [InlineData("mod", TestExprType.Integer, TestExprType.IntsDataset)]
        [InlineData("mod", TestExprType.Integer, TestExprType.NumbersDataset)]
        [InlineData("mod", TestExprType.Integer, TestExprType.MixedIntNumDataset)]
        [InlineData("mod", TestExprType.Integer, TestExprType.NonesDataset)]
        [InlineData("mod", TestExprType.Integer, TestExprType.MixedNoneIntDataset)]
        [InlineData("mod", TestExprType.Integer, TestExprType.MixedNoneNumDataset)]
        [InlineData("mod", TestExprType.Number, TestExprType.IntsDataset)]
        [InlineData("mod", TestExprType.Number, TestExprType.NumbersDataset)]
        [InlineData("mod", TestExprType.Number, TestExprType.MixedIntNumDataset)]
        [InlineData("mod", TestExprType.Number, TestExprType.NonesDataset)]
        [InlineData("mod", TestExprType.Number, TestExprType.MixedNoneIntDataset)]
        [InlineData("mod", TestExprType.Number, TestExprType.MixedNoneNumDataset)]
        [InlineData("mod", TestExprType.None, TestExprType.IntsDataset)]
        [InlineData("mod", TestExprType.None, TestExprType.NumbersDataset)]
        [InlineData("mod", TestExprType.None, TestExprType.MixedIntNumDataset)]
        [InlineData("mod", TestExprType.None, TestExprType.NonesDataset)]
        [InlineData("mod", TestExprType.None, TestExprType.MixedNoneIntDataset)]
        [InlineData("mod", TestExprType.None, TestExprType.MixedNoneNumDataset)]
        public void GetOutputStructure_CorrectScalarDatasetExpr_NumbersDatasetStructure(string opSymbol, params TestExprType[] types)
        {
            IExpression numericExpr = TestExprFactory.GetExpression(types);
            numericExpr.OperatorDefinition = this.opResolver(opSymbol);

            IDataStructure dataStructure = numericExpr.OperatorDefinition.GetOutputStructure(numericExpr);

            Assert.True(TestExprFactory.GetExpression(TestExprType.NumbersDataset).Structure.EqualsObj(dataStructure));
        }

        [Theory]
        [InlineData("mod")]
        [InlineData("round")]
        [InlineData("trunc")]
        [InlineData("ceil")]
        [InlineData("floor")]
        [InlineData("abs")]
        [InlineData("exp")]
        [InlineData("ln")]
        [InlineData("power")]
        [InlineData("log")]
        [InlineData("sqrt")]
        public void GetOutputStructure_WrongArgsExpr_ThrowsException(string opSymbol)
        {
            int combinationsNumber = 1;
            List<TestExprType[]> correctCombs = new List<TestExprType[]>();

            if (opSymbol.In("ceil", "floor", "abs", "exp", "ln", "sqrt"))
            {
                // OneArg
                correctCombs.AddRange(new TestExprType[][] {
                    new TestExprType[] { TestExprType.Integer },
                    new TestExprType[] { TestExprType.Number },
                    new TestExprType[] { TestExprType.None },
                    new TestExprType[] { TestExprType.IntsDataset },
                    new TestExprType[] { TestExprType.NumbersDataset },
                    new TestExprType[] { TestExprType.MixedIntNumDataset },
                    new TestExprType[] { TestExprType.NonesDataset },
                    new TestExprType[] { TestExprType.MixedNoneIntDataset },
                    new TestExprType[] { TestExprType.MixedNoneNumDataset }
                });
            }
            else
            {
                // TwoArgs
                combinationsNumber = 2;
                correctCombs.AddRange(new TestExprType[][] {
                    new TestExprType[] { TestExprType.Integer, TestExprType.Integer },
                    new TestExprType[] { TestExprType.Integer, TestExprType.None },
                    new TestExprType[] { TestExprType.Number, TestExprType.Integer },
                    new TestExprType[] { TestExprType.Number, TestExprType.None },
                    new TestExprType[] { TestExprType.None, TestExprType.Integer },
                    new TestExprType[] { TestExprType.None, TestExprType.None },
                    new TestExprType[] { TestExprType.IntsDataset, TestExprType.Integer },
                    new TestExprType[] { TestExprType.IntsDataset, TestExprType.None },
                    new TestExprType[] { TestExprType.NumbersDataset, TestExprType.Integer },
                    new TestExprType[] { TestExprType.NumbersDataset, TestExprType.None },
                    new TestExprType[] { TestExprType.MixedIntNumDataset, TestExprType.Integer },
                    new TestExprType[] { TestExprType.MixedIntNumDataset, TestExprType.None },
                    new TestExprType[] { TestExprType.NonesDataset, TestExprType.Integer },
                    new TestExprType[] { TestExprType.NonesDataset, TestExprType.None },
                    new TestExprType[] { TestExprType.MixedNoneIntDataset, TestExprType.Integer },
                    new TestExprType[] { TestExprType.MixedNoneIntDataset, TestExprType.None },
                    new TestExprType[] { TestExprType.MixedNoneNumDataset, TestExprType.Integer },
                    new TestExprType[] { TestExprType.MixedNoneNumDataset, TestExprType.None }
                });

                if (!opSymbol.In("round", "trunc", "log"))
                {
                    correctCombs.AddRange(new TestExprType[][] {
                        new TestExprType[] { TestExprType.Integer, TestExprType.Number },
                        new TestExprType[] { TestExprType.Integer, TestExprType.None },
                        new TestExprType[] { TestExprType.Number, TestExprType.Number },
                        new TestExprType[] { TestExprType.Number, TestExprType.None },
                        new TestExprType[] { TestExprType.None, TestExprType.Number },
                        new TestExprType[] { TestExprType.None, TestExprType.None },
                        new TestExprType[] { TestExprType.IntsDataset, TestExprType.Number },
                        new TestExprType[] { TestExprType.IntsDataset, TestExprType.None },
                        new TestExprType[] { TestExprType.NumbersDataset, TestExprType.Number },
                        new TestExprType[] { TestExprType.NumbersDataset, TestExprType.None },
                        new TestExprType[] { TestExprType.MixedIntNumDataset, TestExprType.Number },
                        new TestExprType[] { TestExprType.MixedIntNumDataset, TestExprType.None },
                        new TestExprType[] { TestExprType.NonesDataset, TestExprType.Number },
                        new TestExprType[] { TestExprType.NonesDataset, TestExprType.None },
                        new TestExprType[] { TestExprType.MixedNoneIntDataset, TestExprType.Number },
                        new TestExprType[] { TestExprType.MixedNoneIntDataset, TestExprType.None },
                        new TestExprType[] { TestExprType.MixedNoneNumDataset, TestExprType.Number },
                        new TestExprType[] { TestExprType.MixedNoneNumDataset, TestExprType.None }
                    });
                }

                if (!opSymbol.In("log", "round", "trunc", "power"))
                {
                    correctCombs.AddRange(new TestExprType[][] {
                        new TestExprType[] { TestExprType.Integer, TestExprType.IntsDataset },
                        new TestExprType[] { TestExprType.Integer, TestExprType.NumbersDataset },
                        new TestExprType[] { TestExprType.Integer, TestExprType.MixedIntNumDataset },
                        new TestExprType[] { TestExprType.Integer, TestExprType.NonesDataset },
                        new TestExprType[] { TestExprType.Integer, TestExprType.MixedNoneIntDataset },
                        new TestExprType[] { TestExprType.Integer, TestExprType.MixedNoneNumDataset },
                        new TestExprType[] { TestExprType.Number, TestExprType.IntsDataset },
                        new TestExprType[] { TestExprType.Number, TestExprType.NumbersDataset },
                        new TestExprType[] { TestExprType.Number, TestExprType.MixedIntNumDataset },
                        new TestExprType[] { TestExprType.Number, TestExprType.NonesDataset },
                        new TestExprType[] { TestExprType.Number, TestExprType.MixedNoneIntDataset },
                        new TestExprType[] { TestExprType.Number, TestExprType.MixedNoneNumDataset },
                        new TestExprType[] { TestExprType.None, TestExprType.IntsDataset },
                        new TestExprType[] { TestExprType.None, TestExprType.NumbersDataset },
                        new TestExprType[] { TestExprType.None, TestExprType.MixedIntNumDataset },
                        new TestExprType[] { TestExprType.None, TestExprType.NonesDataset },
                        new TestExprType[] { TestExprType.None, TestExprType.MixedNoneIntDataset },
                        new TestExprType[] { TestExprType.None, TestExprType.MixedNoneNumDataset },
                        new TestExprType[] { TestExprType.IntsDataset, TestExprType.IntsDataset },
                        new TestExprType[] { TestExprType.IntsDataset, TestExprType.NumbersDataset },
                        new TestExprType[] { TestExprType.IntsDataset, TestExprType.MixedIntNumDataset },
                        new TestExprType[] { TestExprType.IntsDataset, TestExprType.NonesDataset },
                        new TestExprType[] { TestExprType.IntsDataset, TestExprType.MixedNoneIntDataset },
                        new TestExprType[] { TestExprType.IntsDataset, TestExprType.MixedNoneNumDataset },
                        new TestExprType[] { TestExprType.NumbersDataset, TestExprType.IntsDataset },
                        new TestExprType[] { TestExprType.NumbersDataset, TestExprType.NumbersDataset },
                        new TestExprType[] { TestExprType.NumbersDataset, TestExprType.MixedIntNumDataset },
                        new TestExprType[] { TestExprType.NumbersDataset, TestExprType.NonesDataset },
                        new TestExprType[] { TestExprType.NumbersDataset, TestExprType.MixedNoneIntDataset },
                        new TestExprType[] { TestExprType.NumbersDataset, TestExprType.MixedNoneNumDataset },
                        new TestExprType[] { TestExprType.NonesDataset, TestExprType.IntsDataset },
                        new TestExprType[] { TestExprType.NonesDataset, TestExprType.NumbersDataset },
                        new TestExprType[] { TestExprType.NonesDataset, TestExprType.MixedIntNumDataset },
                        new TestExprType[] { TestExprType.NonesDataset, TestExprType.NonesDataset },
                        new TestExprType[] { TestExprType.NonesDataset, TestExprType.MixedNoneIntDataset },
                        new TestExprType[] { TestExprType.NonesDataset, TestExprType.MixedNoneNumDataset },
                        new TestExprType[] { TestExprType.MixedIntNumDataset, TestExprType.IntsDataset },
                        new TestExprType[] { TestExprType.MixedIntNumDataset, TestExprType.NumbersDataset },
                        new TestExprType[] { TestExprType.MixedIntNumDataset, TestExprType.MixedIntNumDataset },
                        new TestExprType[] { TestExprType.MixedIntNumDataset, TestExprType.NonesDataset },
                        new TestExprType[] { TestExprType.MixedIntNumDataset, TestExprType.MixedNoneIntDataset },
                        new TestExprType[] { TestExprType.MixedIntNumDataset, TestExprType.MixedNoneNumDataset },
                        new TestExprType[] { TestExprType.MixedNoneIntDataset, TestExprType.IntsDataset },
                        new TestExprType[] { TestExprType.MixedNoneIntDataset, TestExprType.NumbersDataset },
                        new TestExprType[] { TestExprType.MixedNoneIntDataset, TestExprType.MixedIntNumDataset },
                        new TestExprType[] { TestExprType.MixedNoneIntDataset, TestExprType.NonesDataset },
                        new TestExprType[] { TestExprType.MixedNoneIntDataset, TestExprType.MixedNoneIntDataset },
                        new TestExprType[] { TestExprType.MixedNoneIntDataset, TestExprType.MixedNoneNumDataset },
                        new TestExprType[] { TestExprType.MixedNoneNumDataset, TestExprType.IntsDataset },
                        new TestExprType[] { TestExprType.MixedNoneNumDataset, TestExprType.NumbersDataset },
                        new TestExprType[] { TestExprType.MixedNoneNumDataset, TestExprType.MixedIntNumDataset },
                        new TestExprType[] { TestExprType.MixedNoneNumDataset, TestExprType.NonesDataset },
                        new TestExprType[] { TestExprType.MixedNoneNumDataset, TestExprType.MixedNoneIntDataset },
                        new TestExprType[] { TestExprType.MixedNoneNumDataset, TestExprType.MixedNoneNumDataset },
                    });
                }
            }

            TestExprType[][] combinations = Enum.GetValues(typeof(TestExprType)).Cast<TestExprType>().GetCombinations(combinationsNumber);
            TestExprType[][] wrongCombs = combinations.Without(correctCombs.ToArray());

            foreach (TestExprType[] wrongComb in wrongCombs)
            {
                IExpression numericExpr = null;

                if (opSymbol.In("ceil", "floor", "abs", "exp", "ln", "sqrt")) 
                    numericExpr = TestExprFactory.GetExpression(new TestExprType[] { wrongComb[0] });
                else
                    numericExpr = TestExprFactory.GetExpression(wrongComb[0], wrongComb[1]);

                numericExpr.OperatorDefinition = this.opResolver(opSymbol);

                // Debug condition example: wrongComb[0] == TestExprType.Integer && wrongComb[1] == TestExprType.Integer
                Assert.ThrowsAny<VtlOperatorError>(() => { numericExpr.OperatorDefinition.GetOutputStructure(numericExpr); });
            }
        }
    }
}
